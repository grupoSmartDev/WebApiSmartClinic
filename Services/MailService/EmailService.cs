using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using WebApiSmartClinic.Helpers;

namespace WebApiSmartClinic.Services.MailService;

public class EmailService : IEmailService
{
    private readonly EmailSettings emailSettings;

    public EmailService(IOptions<EmailSettings> options)
    {
        this.emailSettings = options.Value;
    }

    public async Task SendEmailAsync(MailRequest mailRequest)
    {
        try
        {
            Console.WriteLine($"\n📧 ===== ENVIANDO EMAIL =====");
            Console.WriteLine($"De: {emailSettings.Email}");
            Console.WriteLine($"Para: {mailRequest.ToEmail}");
            Console.WriteLine($"Assunto: {mailRequest.Subject}");

            var email = new MimeMessage();

            // Usa o email configurado como remetente
            var senderAddress = new MailboxAddress(emailSettings.Displayname, emailSettings.Email);
            email.From.Add(senderAddress);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();

            // ⚠️ MUDANÇA AQUI: Porta 465 com SSL (não StartTls)
            Console.WriteLine($"🔌 Conectando ao {emailSettings.Host}:{emailSettings.Port}...");
            await smtp.ConnectAsync(
                emailSettings.Host,
                emailSettings.Port,
                MailKit.Security.SecureSocketOptions.SslOnConnect  // <-- SSL direto!
            );

            Console.WriteLine($"🔐 Autenticando...");
            await smtp.AuthenticateAsync(emailSettings.Email, emailSettings.Password);

            Console.WriteLine($"📤 Enviando...");
            await smtp.SendAsync(email);

            await smtp.DisconnectAsync(true);

            Console.WriteLine($"✅ Email enviado com sucesso!\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Erro ao enviar email: {ex.Message}");
            Console.WriteLine($"❌ Detalhes: {ex.InnerException?.Message}");
            throw new Exception($"Falha no envio de email: {ex.Message}", ex);
        }
    }
}
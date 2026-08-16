using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using WebApiSmartClinic.Helpers;

namespace WebApiSmartClinic.Services.MailService;

public class EmailService : IEmailService
{
    private readonly EmailSettings configuracaoEmail;

    public EmailService(IOptions<EmailSettings> options)
    {
        configuracaoEmail = options.Value;
    }

    public async Task SendEmailAsync(MailRequest mailRequest)
    {
        try
        {
            Console.WriteLine($"\n📧 ===== ENVIANDO EMAIL =====");
            Console.WriteLine($"De: {configuracaoEmail.Email}");
            Console.WriteLine($"Para: {mailRequest.ToEmail}");
            Console.WriteLine($"Assunto: {mailRequest.Subject}");

            var email = new MimeMessage();

            // Usa o email configurado como remetente
            var senderAddress = new MailboxAddress(configuracaoEmail.Displayname, configuracaoEmail.Email);
            email.From.Add(senderAddress);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();

            // O Zoho usa SSL direto na porta 465.
            Console.WriteLine($"🔌 Conectando ao {configuracaoEmail.Host}:{configuracaoEmail.Port}...");
            await smtp.ConnectAsync(
                configuracaoEmail.Host,
                configuracaoEmail.Port,
                MailKit.Security.SecureSocketOptions.SslOnConnect
            );

            Console.WriteLine($"🔐 Autenticando...");
            await smtp.AuthenticateAsync(configuracaoEmail.Email, configuracaoEmail.Password);

            Console.WriteLine($"📤 Enviando...");
            await smtp.SendAsync(email);

            await smtp.DisconnectAsync(true);

            Console.WriteLine($"✅ Email enviado com sucesso!\n");
        }
        catch (SmtpCommandException ex) when (
            ex.Message.Contains("Relaying disallowed", StringComparison.OrdinalIgnoreCase) ||
            ex.Message.Contains("Invalid Domain", StringComparison.OrdinalIgnoreCase))
        {
            const string mensagem = "O Zoho recusou o domínio do remetente. Verifique se o domínio está ativo no painel do Zoho e se o servidor SMTP corresponde ao plano da conta.";

            Console.WriteLine($"❌ {mensagem}");
            throw new InvalidOperationException(mensagem, ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Erro ao enviar email: {ex.Message}");
            Console.WriteLine($"❌ Detalhes: {ex.InnerException?.Message}");
            throw new Exception($"Falha no envio de email: {ex.Message}", ex);
        }
    }
}

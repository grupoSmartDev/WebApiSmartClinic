using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Helpers;
using WebApiSmartClinic.Services.MailService;

namespace WebApiSmartClinic.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MailController : ControllerBase
{
    
    private readonly IEmailService emailService;

    public MailController(IEmailService service)
    {
        this.emailService = service;
    }


    [AllowAnonymous]
    [HttpPost("SendMail")]
    public async Task<IActionResult> SendMail()
    {
        try
        {
            MailRequest mailRequest = new MailRequest();
            mailRequest.ToEmail = "joaog.faelis@gmail.com";
            mailRequest.Subject = "João o Gaybriel";
            mailRequest.Body = GetHtmlContent();

            await emailService.SendEmailAsync(mailRequest);
            return Ok();
        }
        catch (Exception)
        {

            throw;
        }

    }


    private string GetHtmlContent()
    {
        string mensagem = @"Olá. Seja bem vindo ao Clinc Smart!

        Obrigado, sua senha padrão é Admin@123 e sua chave de acesso é o CPF informado para cadastro. 

        Após o primeiro Login, recomendamos que você altere sua senha. 

        Qualquer dúvida, entre em contato com o suporte.

        Estamos felizes em ter você na Família Smart.";

                string htmlContent = $@"
                <!DOCTYPE html>
                <html lang=""pt-BR"">
                <head>
                    <meta charset=""UTF-8"">
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            line-height: 1.6;
                            color: #333;
                            max-width: 600px;
                            margin: 0 auto;
                            padding: 20px;
                        }}
                        .welcome-message {{
                            background-color: #f4f4f4;
                            border-left: 4px solid #007bff;
                            padding: 15px;
                        }}
                    </style>
                </head>
                <body>
                    <div class=""welcome-message"">
                        <p>{mensagem.Replace("\n", "<br>")}</p>
                    </div>
                </body>
                </html>";

                return htmlContent;
            }

}

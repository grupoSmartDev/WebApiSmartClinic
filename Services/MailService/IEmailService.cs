using WebApiSmartClinic.Helpers;

namespace WebApiSmartClinic.Services.MailService;

public interface IEmailService
{
    Task SendEmailAsync(MailRequest mailRequest);
}

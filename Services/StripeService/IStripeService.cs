using static WebApiSmartClinic.Models.stripe.StripeModels;

namespace WebApiSmartClinic.Services.StripeService;

public interface IStripeService
{
    Task<string> CreateCustomerAsync(CreateCustomerRequest request);
    Task<CreatePaymentIntentResponse> CreatePaymentIntentAsync(CreatePaymentIntentRequest request);
    Task<bool> ProcessWebhookAsync(string json, string stripeSignature);
}

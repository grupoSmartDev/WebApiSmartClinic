using static WebApiSmartClinic.Models.stripe.StripeModels;
using Stripe;

namespace WebApiSmartClinic.Services.StripeService;

public class StripeService : IStripeService
{
    private readonly IConfiguration _configuration;

    public StripeService(IConfiguration configuration)
    {
        _configuration = configuration;
        StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
    }

    public async Task<string> CreateCustomerAsync(CreateCustomerRequest request)
    {
        var options = new CustomerCreateOptions
        {
            Email = request.Email,
            Name = request.Name,
        };

        var service = new CustomerService();
        var customer = await service.CreateAsync(options);

        return customer.Id;
    }

    public async Task<CreatePaymentIntentResponse> CreatePaymentIntentAsync(CreatePaymentIntentRequest request)
    {
        var options = new PaymentIntentCreateOptions
        {
            Amount = request.Amount,
            Currency = request.Currency,
            Customer = request.CustomerId,
            AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
            {
                Enabled = true,
            }
        };

        var service = new PaymentIntentService();
        var paymentIntent = await service.CreateAsync(options);

        return new CreatePaymentIntentResponse
        {
            ClientSecret = paymentIntent.ClientSecret,
            PaymentIntentId = paymentIntent.Id
        };
    }

    public async Task<bool> ProcessWebhookAsync(string json, string stripeSignature)
    {
        try
        {
            var webhookSecret = _configuration["Stripe:WebhookSecret"];
            var stripeEvent = EventUtility.ConstructEvent(json, stripeSignature, webhookSecret);

            switch (stripeEvent.Type)
            {
                case "payment_intent.succeeded":
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    // Aqui você atualiza o status do pagamento no seu banco
                    Console.WriteLine($"Pagamento bem-sucedido: {paymentIntent.Id}");
                    break;

                case "payment_intent.payment_failed":
                    var failedPayment = stripeEvent.Data.Object as PaymentIntent;
                    // Aqui você trata o pagamento falhou
                    Console.WriteLine($"Pagamento falhou: {failedPayment.Id}");
                    break;

                default:
                    Console.WriteLine($"Evento não tratado: {stripeEvent.Type}");
                    break;
            }

            return true;
        }
        catch (StripeException)
        {
            return false;
        }
    }
}

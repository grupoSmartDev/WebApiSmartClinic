namespace WebApiSmartClinic.Models.stripe;

public class StripeModels
{
    public class CreatePaymentIntentRequest
    {
        public long Amount { get; set; } // Em centavos
        public string Currency { get; set; } = "brl";
        public string CustomerId { get; set; }
    }

    public class CreatePaymentIntentResponse
    {
        public string ClientSecret { get; set; }
        public string PaymentIntentId { get; set; }
    }

    public class CreateCustomerRequest
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }

    public class WebhookEvent
    {
        public string Type { get; set; }
        public object Data { get; set; }
    }
}

namespace WebApiSmartClinic.Models.Asaas;

public class AsaasSubscriptionRequest
{
    public string customer { get; set; }
    public string billingType { get; set; } // BOLETO, CREDIT_CARD, PIX
    public decimal value { get; set; }
    public string nextDueDate { get; set; } // YYYY-MM-DD
    public string cycle { get; set; } // MONTHLY, SEMIANNUALLY
    public string description { get; set; }
    public string externalReference { get; set; }
    public AsaasCreditCard creditCard { get; set; }
    public AsaasCreditCardHolderInfo creditCardHolderInfo { get; set; }
}

public class AsaasCreditCard
{
    public string holderName { get; set; }
    public string number { get; set; }
    public string expiryMonth { get; set; }
    public string expiryYear { get; set; }
    public string ccv { get; set; }
}

public class AsaasCreditCardHolderInfo
{
    public string name { get; set; }
    public string email { get; set; }
    public string cpfCnpj { get; set; }
    public string postalCode { get; set; }
    public string addressNumber { get; set; }
    public string phone { get; set; }
}

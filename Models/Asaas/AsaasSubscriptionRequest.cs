namespace WebApiSmartClinic.Models.Asaas;

public class AsaasSubscriptionRequest
{
    public string customer { get; set; }
    public string billingType { get; set; } // BOLETO, CREDIT_CARD, PIX
    public decimal value { get; set; } //valor total 
    public string nextDueDate { get; set; } // YYYY-MM-DD
    public string cycle { get; set; } // MONTHLY, SEMIANNUALLY
    public string description { get; set; }
    public string externalReference { get; set; }
    public string? installmentCount { get; set; } // Numero maximo de parcelas
    public decimal totalValue { get; set; } //valor total 
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


// Request para criar pagamento com cartão
public class AsaasPaymentRequest
{
    public string customer { get; set; }
    public string billingType { get; set; } = "CREDIT_CARD";
    public decimal value { get; set; }
    public DateTime dueDate { get; set; }
    public string description { get; set; }
    public int? installmentCount { get; set; } // Número de parcelas
    public decimal? installmentValue { get; set; } // Valor de cada parcela
    public AsaasCreditCardRequest creditCard { get; set; }
    public AsaasCreditCardHolderInfoRequest creditCardHolderInfo { get; set; }
    public string remoteIp { get; set; } // IP do cliente (recomendado)
    public string? externalReference { get; set; } 

}

// Dados do cartão de crédito
public class AsaasCreditCardRequest
{
    public string holderName { get; set; }
    public string number { get; set; }
    public string expiryMonth { get; set; }
    public string expiryYear { get; set; }
    public string ccv { get; set; }
}

// Informações do titular do cartão
public class AsaasCreditCardHolderInfoRequest
{
    public string name { get; set; }
    public string email { get; set; }
    public string cpfCnpj { get; set; }
    public string postalCode { get; set; }
    public string addressNumber { get; set; }
    public string addressComplement { get; set; }
    public string phone { get; set; }
    public string mobilePhone { get; set; }
}

// Response do pagamento
public class AsaasPaymentResponse
{
    public string id { get; set; }
    public DateTime dateCreated { get; set; }
    public string customer { get; set; }
    public string subscription { get; set; }
    public string installment { get; set; }
    public DateTime dueDate { get; set; }
    public decimal value { get; set; }
    public decimal netValue { get; set; }
    public string billingType { get; set; }
    public string status { get; set; } // PENDING, CONFIRMED, RECEIVED, etc.
    public string description { get; set; }
    public string invoiceUrl { get; set; }
    public string invoiceNumber { get; set; }
    public string transactionReceiptUrl { get; set; }
    public bool canBePaidAfterDueDate { get; set; }
    public AsaasCreditCardInfo creditCard { get; set; }
}

// Informações do cartão na resposta (dados mascarados)
public class AsaasCreditCardInfo
{
    public string creditCardNumber { get; set; }
    public string creditCardBrand { get; set; }
    public string creditCardToken { get; set; }
}


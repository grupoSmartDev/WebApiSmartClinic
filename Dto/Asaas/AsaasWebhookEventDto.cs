namespace WebApiSmartClinic.Dto.Asaas;

// Payload de evento enviado pelo Asaas para o endpoint POST /api/Asaas/Webhook.
// Docs: https://docs.asaas.com/docs/webhook-para-cobrancas
// A desserialização usa os JSON web defaults do ASP.NET Core (camelCase, case-insensitive),
// então "event"/"payment"/"externalReference" caem nas propriedades PascalCase abaixo.
public class AsaasWebhookEventDto
{
    public string Event { get; set; }
    public AsaasWebhookPaymentDto? Payment { get; set; }
    public AsaasWebhookSubscriptionDto? Subscription { get; set; }
}

public class AsaasWebhookPaymentDto
{
    public string Id { get; set; }
    public string Status { get; set; }
    public string? ExternalReference { get; set; } // "clinicsmart_cpf_{cpfKey}"
    public decimal Value { get; set; }
    public string? Description { get; set; }
    public string? Subscription { get; set; } // ID da subscription se a cobrança vier de uma
    public string? Customer { get; set; }      // ID do customer no Asaas
}

public class AsaasWebhookSubscriptionDto
{
    public string Id { get; set; }
    public string Status { get; set; }
    public string? ExternalReference { get; set; } // "clinicsmart_cpf_{cpfKey}"
}

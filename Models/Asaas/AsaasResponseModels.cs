namespace WebApiSmartClinic.Models.Asaas;

public class AsaasCustomerResponse
{
    public string id { get; set; }
    public string name { get; set; }
    public string email { get; set; }
    public string cpfCnpj { get; set; }
    public DateTime dateCreated { get; set; }
}

public class AsaasSubscriptionResponse
{
    public string id { get; set; }
    public string customer { get; set; }
    public string status { get; set; }
    public decimal value { get; set; }
    public string nextDueDate { get; set; }
    public string cycle { get; set; }
    public string description { get; set; }
    public string externalReference { get; set; }
    public DateTime dateCreated { get; set; }

    // Não vem na resposta de criação da subscription — é preenchido pelo AsaasService
    // com o invoiceUrl da 1ª cobrança (GET /subscriptions/{id}/payments).
    public string? invoiceUrl { get; set; }
}

// Envelope de listagem padrão do Asaas: { object, hasMore, totalCount, limit, offset, data: [...] }
public class AsaasListResponse<T>
{
    public bool hasMore { get; set; }
    public int totalCount { get; set; }
    public int limit { get; set; }
    public int offset { get; set; }
    public List<T> data { get; set; } = new();
}

public class AsaasErrorResponse
{
    public List<AsaasError> errors { get; set; }
}

public class AsaasError
{
    public string code { get; set; }
    public string description { get; set; }
}

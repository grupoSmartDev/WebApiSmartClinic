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

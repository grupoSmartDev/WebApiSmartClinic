namespace WebApiSmartClinic.Models.Asaas;

public class AsaasCustomerRequest
{
    public string name { get; set; }
    public string email { get; set; }
    public string phone { get; set; }
    public string mobilePhone { get; set; }
    public string cpfCnpj { get; set; }
    public string postalCode { get; set; }
    public string address { get; set; }
    public string addressNumber { get; set; }
    public string complement { get; set; }
    public string province { get; set; }
    public string city { get; set; }
    public string state { get; set; }
    public string country { get; set; } = "Brasil";
    public string observations { get; set; }
}



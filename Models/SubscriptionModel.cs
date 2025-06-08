namespace WebApiSmartClinic.Models;

public class SubscriptionModel
{
    public int Id { get; set; }
    public int EmpresaId { get; set; }
    public string StripeSubscriptionId { get; set; }
    public string StripePriceId { get; set; }
    public string Status { get; set; } // active, canceled, past_due, etc.
    public DateTime CurrentPeriodStart { get; set; }
    public DateTime CurrentPeriodEnd { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "brl";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public virtual EmpresaModel Empresa { get; set; }
}

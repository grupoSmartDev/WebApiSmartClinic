namespace WebApiSmartClinic.Models;

public class PaymentModel
{
    public int Id { get; set; }
    public int EmpresaId { get; set; }
    public int? SubscriptionId { get; set; }
    public string StripePaymentIntentId { get; set; }
    public string StripeInvoiceId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "brl";
    public string Status { get; set; } // succeeded, failed, pending
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public virtual EmpresaModel Empresa { get; set; }
    public virtual SubscriptionModel Subscription { get; set; }
}

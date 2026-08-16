namespace WebApiSmartClinic.Dto.Financ_Pagar;

public sealed class AgruparParcelasDto
{
    public List<int> ParcelasFilhasIds { get; set; } = new();
    public decimal ValorPago { get; set; }
}

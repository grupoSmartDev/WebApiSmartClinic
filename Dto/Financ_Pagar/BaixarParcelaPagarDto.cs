namespace WebApiSmartClinic.Dto.Financ_Pagar;

public sealed class BaixarParcelaPagarDto
{
    public decimal ValorPago { get; set; }
    public DateTime? DataPagamento { get; set; }
    public int? FormaPagamentoId { get; set; }
    public int? TipoPagamentoId { get; set; }
}

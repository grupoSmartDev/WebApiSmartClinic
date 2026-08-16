namespace WebApiSmartClinic.Dto.Financ_Pagar;

public sealed class Financ_PagarEdicaoDto
{
    public int Id { get; set; }
    public int? IdOrigem { get; set; }
    public int? NrDocto { get; set; }
    public DateTime? DataEmissao { get; set; }
    public decimal? ValorOriginal { get; set; }
    public decimal? Valor { get; set; }
    public string? Status { get; set; }
    public string? NotaFiscal { get; set; }
    public string? Descricao { get; set; }
    public string? Classificacao { get; set; }
    public string? Observacao { get; set; }
    public int? FornecedorId { get; set; }
    public int? CentroCustoId { get; set; }
    public int? TipoPagamentoId { get; set; }
    public int? BancoId { get; set; }
    public int? PlanoContaId { get; set; }
    public int? DespesaFixaId { get; set; }
    public int? Parcela { get; set; }
    public List<Financ_PagarSubEdicaoDto>? subFinancPagar { get; set; }
}

public sealed class Financ_PagarSubEdicaoDto
{
    public int? Id { get; set; }
    public int? FinancPagarId { get; set; }
    public int? Parcela { get; set; }
    public decimal? Valor { get; set; }
    public DateTime? DataVencimento { get; set; }
    public DateTime? DataPagamento { get; set; }
    public decimal? ValorPago { get; set; }
    public string? Observacao { get; set; }
    public decimal? Desconto { get; set; }
    public decimal? Juros { get; set; }
    public decimal? Multa { get; set; }
    public int? FormaPagamentoId { get; set; }
    public int? TipoPagamentoId { get; set; }
}

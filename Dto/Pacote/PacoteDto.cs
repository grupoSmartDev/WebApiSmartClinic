namespace WebApiSmartClinic.Dto.Pacote
{
    // ========== CREATE ==========
    public class PacoteCreateDto
    {
        public string Descricao { get; set; } = string.Empty;
        public int ProcedimentoId { get; set; }
        public int QuantidadeSessoes { get; set; }
        public decimal Valor { get; set; }
        public int? CentroCustoId { get; set; }
        public string? Observacao { get; set; }
    }

    // ========== EDIÇÃO ==========
    public class PacoteEdicaoDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public int ProcedimentoId { get; set; }
        public int QuantidadeSessoes { get; set; }
        public decimal Valor { get; set; }
        public int? CentroCustoId { get; set; }
        public string? Observacao { get; set; }
        public bool Ativo { get; set; } = true;
    }

    // ========== VENDA ==========
    public class PacoteVendaDto
    {
        public int PacoteId { get; set; }
        public int PacienteId { get; set; }
        public string? Observacao { get; set; }
        public FinanceiroPacoteDto? Financeiro { get; set; }
    }

    public class FinanceiroPacoteDto
    {
        public decimal Valor { get; set; }
        public int TipoPagamentoId { get; set; }
        public int Parcela { get; set; }
        public int? CentroCustoId { get; set; }
        public string? Observacao { get; set; }
        public List<FinanceiroPacoteSubDto> subFinancReceber { get; set; } = new();
    }

    public class FinanceiroPacoteSubDto
    {
        public int Parcela { get; set; }
        public decimal Valor { get; set; }
        public int? TipoPagamentoId { get; set; }
        public int? FormaPagamentoId { get; set; }
        public DateTime? DataPagamento { get; set; }
        public decimal Desconto { get; set; }
        public decimal Juros { get; set; }
        public decimal Multa { get; set; }
        public DateTime DataVencimento { get; set; }
        public string? Observacao { get; set; }
    }

    // ========== USO ==========
    public class PacoteUsoDto
    {
        public int PacotePacienteId { get; set; }
        public int AgendaId { get; set; }
        public int PacienteUtilizadoId { get; set; }
        public string? Observacao { get; set; }
    }
}
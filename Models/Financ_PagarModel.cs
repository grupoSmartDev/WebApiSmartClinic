
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApiSmartClinic.Models;

public class Financ_PagarModel
{
    public int Id { get; set; }
    public int? IdOrigem { get; set; }
    public int? NrDocto { get; set; }
    private DateTime? _DataEmissao;
    public DateTime? DataEmissao
    {
        get => _DataEmissao?.ToLocalTime();
        set => _DataEmissao = value.HasValue
            ? DateTime.SpecifyKind(value.Value.ToUniversalTime(), DateTimeKind.Utc)
            : null;
    }
    public decimal? ValorOriginal { get; set; }
    public decimal? ValorPago { get; set; }
    public decimal? Valor { get; set; }
    public int? Parcela { get; set; }
    public string? Status { get; set; } // Pendente, Pago, Atrasado, Cancelado
    public string? NotaFiscal { get; set; }
    public string? Descricao { get; set; }
    public string? Classificacao { get; set; }
    public string? Observacao { get; set; }
    public int? PacienteId { get; set; } = null;

    public PacienteModel? Paciente { get; set; }
    public int? FornecedorId { get; set; } = null; // Relacionamento com a tabela de Fornecedor

    public FornecedorModel? Fornecedor { get; set; }
    public int? CentroCustoId { get; set; } = null; // Relacionamento com a tabela de CentroCusto    
    public CentroCustoModel? CentroCusto { get; set; }
    public BancoModel? Banco { get; set; }
    public int? BancoId { get; set; } = 0; // Relacionamento com a tabela de Banco
    public int? TipoPagamentoId { get; set; }
    public TipoPagamentoModel? TipoPagamento { get; set; }

    public ICollection<Financ_PagarSubModel>? subFinancPagar { get; set; } = new List<Financ_PagarSubModel>();


    public int? PlanoContaId { get; internal set; }
    public PlanoContaModel? PlanoConta { get; set; }

    public int? DespesaFixaId { get; set; }
    [JsonIgnore]
    public DespesaFixaModel? DespesaFixa{ get; set; }


}

public class Financ_PagarSubModel
{
    public int? Id { get; set; }
    public int? financPagarId { get; set; }
    public Financ_PagarModel? FinancPagar { get; set; }
    public int? Parcela { get; set; }
    public decimal Valor { get; set; }
    public decimal? ValorPago { get; set; }
    public decimal? Desconto { get; set; }
    public decimal? Juros { get; set; }
    public decimal? Multa { get; set; }

    public int? TipoPagamentoId { get; set; } = null; // Relacionamento com a tabela de TipoPagamento
    [JsonIgnore]
    public TipoPagamentoModel? TipoPagamento { get; set; }
    public int? FormaPagamentoId { get; set; } = null; // Relacionamento com a tabela de FormaPagamento
    [JsonIgnore]
    public FormaPagamentoModel? FormaPagamento { get; set; }
    private DateTime? _DataPagamento;
    public DateTime? DataPagamento
    {
        get => _DataPagamento?.ToLocalTime();
        set => _DataPagamento = value.HasValue
            ? DateTime.SpecifyKind(value.Value.ToUniversalTime(), DateTimeKind.Utc)
            : null;
    }
    private DateTime? _DataVencimento;
    public DateTime? DataVencimento
    {
        get => _DataVencimento?.ToLocalTime();
        set => _DataVencimento = value.HasValue
            ? DateTime.SpecifyKind(value.Value.ToUniversalTime(), DateTimeKind.Utc)
            : null;
    }
    public string? Observacao { get; set; }
}
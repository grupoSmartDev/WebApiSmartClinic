
using System.Text.Json.Serialization;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Dto.Financ_Pagar;

public class Financ_PagarCreateDto
{
    public int? IdOrigem { get; set; }
    public int? NrDocto { get; set; }
    public DateTime DataEmissao { get; set; }
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
    [JsonIgnore]
    public PacienteModel? Paciente { get; set; }
    public int? FornecedorId { get; set; } = null; // Relacionamento com a tabela de Fornecedor
    [JsonIgnore]
    public FornecedorModel? Fornecedor { get; set; }
    public int? CentroCustoId { get; set; } = null; // Relacionamento com a tabela de CentroCusto
    [JsonIgnore]
    public CentroCustoModel? CentroCusto { get; set; }

    [JsonIgnore]
    public BancoModel? Banco { get; set; }
    public int? BancoId { get; set; } = 0; // Relacionamento com a tabela de Banco
    public int? TipoPagamentoId { get; set; }
    public TipoPagamentoModel? TipoPagamento { get; set; }

    public ICollection<Financ_PagarSubModel>? subFinancPagar { get; set; } = new List<Financ_PagarSubModel>();
}

public class Financ_PagarSubCreateDto
{
    
    public int? financPagarId { get; set; }
    public Financ_PagarModel? FinancPagar { get; set; }
    public int? Parcela { get; set; }
    public decimal Valor { get; set; }
    public decimal? Desconto { get; set; }
    public decimal? Juros { get; set; }
    public decimal? Multa { get; set; }

    public int? TipoPagamentoId { get; set; } = null; // Relacionamento com a tabela de TipoPagamento
    [JsonIgnore]
    public TipoPagamentoModel? TipoPagamento { get; set; }
    public int? FormaPagamentoId { get; set; } = null; // Relacionamento com a tabela de FormaPagamento
    [JsonIgnore]
    public FormaPagamentoModel? FormaPagamento { get; set; }
    public DateTime? DataPagamento { get; set; }
    public DateTime? DataVencimento { get; set; }
    public string? Observacao { get; set; }
}
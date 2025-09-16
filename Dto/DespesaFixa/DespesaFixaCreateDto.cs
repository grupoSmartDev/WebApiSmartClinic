using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Dto.CentroCusto;
using WebApiSmartClinic.Dto.PlanoConta;
using WebApiSmartClinic.Dto.Fornecedor;
using WebApiSmartClinic.Dto.Financ_Pagar;

namespace WebApiSmartClinic.Dto.DespesaFixa;

public class DespesaFixaCreateDto
{
    [Required]
    [StringLength(150)]
    public string Descricao { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Valor { get; set; }

    [Required]
    public int DiaVencimento { get; set; }

    [Required]
    private DateTime? _DataInicio;
    public DateTime? DataInicio
    {
        get => _DataInicio?.ToLocalTime();
        set => _DataInicio = value.HasValue
            ? DateTime.SpecifyKind(value.Value.ToUniversalTime(), DateTimeKind.Utc)
            : null;
    }

    private DateTime? _DataFim;
    public DateTime? DataFim
    {
        get => _DataFim?.ToLocalTime();
        set => _DataFim = value.HasValue
            ? DateTime.SpecifyKind(value.Value.ToUniversalTime(), DateTimeKind.Utc)
            : null;
    }


    public bool Ativo { get; set; } = true;

 
    [Required]
    public int Frequencia { get; set; } = 1;

    // Propriedades de Relacionamento
    public List<Financ_PagarCreateDto> FinancPagar { get; set; } = new List<Financ_PagarCreateDto>();

    // Fornecedor (opcional)
    public int? FornecedorId { get; set; }
    public FornecedorCreateDto? Fornecedor { get; set; }

    public int? PlanoContaId { get; set; }
    public PlanoContaCreateDto? PlanoConta { get; set; }

    public int? CentroCustoId { get; set; }

    public CentroCustoCreateDto? CentroCusto { get; set; }
    public int? TipoPagamentoId { get; set; }

    public TipoPagamentoModel? TipoPagamento { get; set; }

    public int? FormaPagamentoId { get; set; }

    public FormaPagamentoModel? FormaPagamento { get; set; }

}
//public enum TipoFrequencia
//{
//    Mensal = 1,
//    Bimestral = 2,
//    Trimestral = 3,
//    Semestral = 6,
//    Anual = 12
//}
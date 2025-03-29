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
    public DateTime DataInicio { get; set; }

    public DateTime? DataFim { get; set; }


    public bool? Ativo { get; set; } = true;

    [Required]
    [StringLength(50)]
    public string Categoria { get; set; }

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
}
//public enum TipoFrequencia
//{
//    Mensal = 1,
//    Bimestral = 2,
//    Trimestral = 3,
//    Semestral = 6,
//    Anual = 12
//}
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApiSmartClinic.Models;

public class DespesaFixaModel
{

    [Key]
    public int Id { get; set; }

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
    public List<Financ_PagarSubModel>? FinancPagar { get; set; } = new List<Financ_PagarSubModel>();

    // Fornecedor (opcional)
    public int? FornecedorId { get; set; }
    public FornecedorModel? Fornecedor { get; set; }

    public int? PlanoContaId { get; set; }
    public PlanoContaModel? PlanoConta { get; set;}

    public int? CentroCustoId { get; set; }

    public CentroCustoModel? CentroCusto { get; set; }
}

//public enum TipoFrequencia
//{
//    Mensal = 1,
//    Bimestral = 2,
//    Trimestral = 3,
//    Semestral = 6,
//    Anual = 12
//}
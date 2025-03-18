
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Dto.Plano;

public class PlanoCreateDto
{

    [Required(ErrorMessage = "A descrição é obrigatória.")]
    [StringLength(255, ErrorMessage = "A descrição deve ter no máximo 255 caracteres.")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "O tempo em minutos é obrigatório.")]
    [Range(1, int.MaxValue, ErrorMessage = "O tempo em minutos deve ser maior que zero.")]
    public int TempoMinutos { get; set; }

    [Required(ErrorMessage = "Os dias da semana são obrigatórios.")]
    [Range(1, 7, ErrorMessage = "Os dias da semana devem estar entre 1 e 7.")]
    public int DiasSemana { get; set; }

    public int? CentroCustoId { get; set; } // Relacionamento opcional com centro de custo

    [Range(0, double.MaxValue, ErrorMessage = "O valor deve ser maior ou igual a zero.")]
    public decimal? ValorBimestral { get; set; }
    public decimal? ValorTrimestral { get; set; }
    public decimal? ValorQuadrimestral { get; set; }
    public decimal? ValorSemestral { get; set; }
    public decimal? ValorAnual { get; set; }
    public decimal? ValorMensal { get; set; }

    [DataType(DataType.Date)]
    public DateTime? DataInicio { get; set; }

    [DataType(DataType.Date)]
    public DateTime? DataFim { get; set; }

    public bool Ativo { get; set; } = true;

    public int? PacienteId { get; set; } // Relacionamento opcional com paciente
    [JsonIgnore]
    public PacienteModel? Paciente { get; set; }

    public int? FinanceiroId { get; set; } // Relacionamento opcional com financeiro
    [JsonIgnore]
    public Financ_ReceberModel? Financeiro { get; set; }

    [Required(ErrorMessage = "O tipo de mês é obrigatório.")]
    [StringLength(1, ErrorMessage = "O tipo de mês deve ter apenas um caractere.")]
    public string TipoMes { get; set; } = string.Empty; // Usado como enum no front-end
}
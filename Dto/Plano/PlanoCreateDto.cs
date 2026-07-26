
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebApiSmartClinic.Dto.Agenda;
using WebApiSmartClinic.Dto.Financ_Receber;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Dto.Plano;

public sealed class PlanoCreateDto
{

    [Required(ErrorMessage = "A descri��o � obrigat�ria.")]
    [StringLength(255, ErrorMessage = "A descri��o deve ter no m�ximo 255 caracteres.")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "O tempo em minutos � obrigat�rio.")]
    [Range(1, int.MaxValue, ErrorMessage = "O tempo em minutos deve ser maior que zero.")]
    public int TempoMinutos { get; set; }

    [Required(ErrorMessage = "Os dias da semana s�o obrigat�rios.")]
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
    
    public PacienteModel? Paciente { get; set; }

    public int? FinanceiroId { get; set; } // Relacionamento opcional com financeiro
    
    public Financ_ReceberCreateDto? Financeiro { get; set; }

    [Required(ErrorMessage = "O tipo de m�s � obrigat�rio.")]
    [StringLength(1, ErrorMessage = "O tipo de m�s deve ter apenas um caractere.")]
    public string TipoMes { get; set; } = string.Empty; // Usado como enum no front-end
    
    public AgendaCreateDto? Agendamento { get; set; }
}


public class PlanoRenovacaoDto
{
    public int PlanoId { get; set; }
    // Id do plano-template (PlanoModel sem PacienteId) escolhido para a renovação. O backend
    // usa os valores atuais desse template (TempoMinutos/DiasSemana/Valor*), não os do plano
    // que está sendo renovado - permite trocar de plano na renovação, não só a periodicidade.
    public int PlanoModeloId { get; set; }
    public string Descricao { get; set; }
    public string TipoMes { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public bool GerarFinanceiro { get; set; }
    public bool GerarAgendamento { get; set; }
    public Financ_ReceberCreateDto Financeiro { get; set; }
    public AgendaCreateDto Agendamento { get; set; }
}
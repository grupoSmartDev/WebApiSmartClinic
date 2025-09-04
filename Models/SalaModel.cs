using WebApiSmartClinic.Dto.SalaHorario;

namespace WebApiSmartClinic.Models;

public class SalaModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int Capacidade { get; set; }
    public string? Tipo { get; set; }
    public string? local { get; set; }
    public bool Status { get; set; } = true;
    public string? HorarioFincionamento { get; set; }
    public string? Observacao { get; set; }
    private DateTime? _DataAlteracao;
    public DateTime? DataAlteracao
    {
        get => _DataAlteracao;
        set => _DataAlteracao = value.HasValue ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc) : null;
    }
    public bool IsSystemDefault { get; internal set; }
    public virtual ICollection<SalaHorarioModel> HorariosFuncionamento { get; set; } = new List<SalaHorarioModel>();
    public virtual ICollection<AgendaModel> Agendamentos { get; set; } = new List<AgendaModel>();
}

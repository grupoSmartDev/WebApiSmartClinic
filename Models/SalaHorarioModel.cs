using WebApiSmartClinic.Models;

public class SalaHorarioModel
{
    public int Id { get; set; }
    public int SalaId { get; set; }
    public DayOfWeek DiaSemana { get; set; }
    public TimeSpan HoraInicio { get; set; }
    public TimeSpan HoraFim { get; set; }
    public bool Ativo { get; set; } = true;
    public SalaModel Sala { get; set; } = null!;
}

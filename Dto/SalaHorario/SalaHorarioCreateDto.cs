using System.Text.Json.Serialization;

public sealed class SalaHorarioCreateDto
{
    public int SalaId { get; set; }
    public DayOfWeek DiaSemana { get; set; }
    public string HoraInicio { get; set; }
    public string HoraFim { get; set; }
    public bool Ativo { get; set; } = true;
}

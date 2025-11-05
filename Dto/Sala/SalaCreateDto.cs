using System.Text.Json.Serialization;
using WebApiSmartClinic.Dto.SalaHorario;

namespace WebApiSmartClinic.Dto.Sala;

public sealed class SalaCreateDto
{
    public string Nome { get; set; }
    public int Capacidade { get; set; }
    public string? Tipo { get; set; }
    public string? Local { get; set; }
    public bool Status { get; set; } = true;
    public string? Observacao { get; set; }
    public List<SalaHorarioCreateDto>? HorariosFuncionamento { get; set; }
}
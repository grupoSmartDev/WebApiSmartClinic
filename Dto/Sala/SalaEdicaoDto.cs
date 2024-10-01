namespace WebApiSmartClinic.Dto.Sala;

public class SalaEdicaoDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int Capacidade { get; set; }
    public string Tipo { get; set; }
    public string local { get; set; }
    public string Status { get; set; }
    public string HorarioFincionamento { get; set; }
    public string Observacao { get; set; }
}

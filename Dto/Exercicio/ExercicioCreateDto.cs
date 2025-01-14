
namespace WebApiSmartClinic.Dto.Exercicio;

public class ExercicioCreateDto
{
    
    public int? EvolucaoId { get; set; }
    // Propriedades que podem ser alteradas ao vincular
    public int? Peso { get; set; }
    public int? Tempo { get; set; }
    public int? Repeticoes { get; set; }
    public int? Series { get; set; }
    public string? Obs { get; set; }
    public string? Descricao { get; set; }
}
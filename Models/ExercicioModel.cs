
using System.Text.Json.Serialization;

namespace WebApiSmartClinic.Models;

public class ExercicioModel
{
    public int Id { get; set; }
    public string Descricao { get; set; }
    public string? Obs { get; set; }
    public int? Peso { get; set; }
    public int? Tempo { get; set; }
    public int? Repeticoes { get; set; }
    public int? Series { get; set; }
    public int? EvolucaoId { get; set; } // Opcional, quando vinculado a uma evolu��o
    
    [JsonIgnore]
    public EvolucaoModel? Evolucao { get; set; }
}
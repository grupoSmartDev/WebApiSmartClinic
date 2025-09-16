
using System.Text.Json.Serialization;

namespace WebApiSmartClinic.Models;

public class AtividadeModel
{
    public int Id { get; set; }
    public string? Titulo { get; set; }
    public string Descricao { get; set; }
    public string? Tempo { get; set; }
    public int? EvolucaoId { get; set; }
    
    [JsonIgnore]
    public virtual EvolucaoModel? Evolucao { get; set; }
}
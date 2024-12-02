using System.Text.Json.Serialization;

namespace WebApiSmartClinic.Models;

public class Evolucao
{
    public int Id { get; set; }
    public string Observacao{ get; set; }
    public DateTime DataEvolucao { get; set; }
    public int? PacienteId { get; set; }
    [JsonIgnore]
    public PacienteModel paciente { get; set; }
    [JsonIgnore]
    public List<ExercicioModel>? exercicios{ get; set; }
    [JsonIgnore]
    public List<AtividadeModel>? atividades{ get; set; }

}
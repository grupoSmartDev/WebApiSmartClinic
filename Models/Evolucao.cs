using System.Text.Json.Serialization;

namespace WebApiSmartClinic.Models;

public class Evolucao
{
    public int Id { get; set; }
    public string Observacao { get; set; }
    public DateTime DataEvolucao { get; set; }
    public int PacienteId { get; set; }
    public virtual PacienteModel Paciente { get; set; }
    public virtual List<ExercicioModel>? Exercicios { get; set; }
    public virtual List<AtividadeModel>? Atividades { get; set; }

}
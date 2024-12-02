
using Newtonsoft.Json;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Dto.Evolucao
{
    public class EvolucaoCreateDto
    {
        public string Observacao { get; set; }
        public DateTime DataEvolucao { get; set; }
        public int? PacienteId { get; set; }
        [JsonIgnore]
        public PacienteModel Paciente { get; set; }
        [JsonIgnore]
        public List<ExercicioModel>? Exercicios { get; set; }
        [JsonIgnore]
        public List<AtividadeModel>? Atividades { get; set; }
    }
}
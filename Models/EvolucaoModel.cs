
using Newtonsoft.Json;
using WebApiSmartClinic.Dto.Financ_Receber;

namespace WebApiSmartClinic.Models
{
    public class EvolucaoModel
    {
        public int Id { get; set; }
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
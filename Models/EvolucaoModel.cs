
using Newtonsoft.Json;
using WebApiSmartClinic.Dto.Financ_Receber;

namespace WebApiSmartClinic.Models
{
    public class EvolucaoModel
    {

        public int Id { get; set; }
        public string Observacao { get; set; }
        private DateTime? _DataEvolucao;
        public DateTime? DataEvolucao 
        {
            get => _DataEvolucao;
            set => _DataEvolucao = value.HasValue ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc) : null;
        }
        public int? PacienteId { get; set; }

        [JsonIgnore]
        public virtual PacienteModel Paciente { get; set; }
        public int? ProfissionalId { get; set; }
        public ICollection<AtividadeModel>? Atividades { get; set; } = new List<AtividadeModel>();
        public ICollection<ExercicioModel>? Exercicios { get; set; } = new List<ExercicioModel>();
    }
}
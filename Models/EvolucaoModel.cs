
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
        public virtual PacienteModel Paciente { get; set; }
        public ICollection<ExercicioModel>? Exercicios { get; set; }
        public virtual ICollection<AtividadeModel> Atividades { get; set; }
    }
}
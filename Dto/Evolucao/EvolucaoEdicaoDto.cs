
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Dto.Evolucao
{
    public class EvolucaoEdicaoDto
    {
        public int Id { get; set; }
        public string Obs { get; set; }
        public DateTime DataEvolucao { get; set; }
        public int PacienteId { get; set; }
        public List<ExercicioModel>? Exercicios { get; set; }
        public List<AtividadeModel>? Atividades { get; set; }
    }
}
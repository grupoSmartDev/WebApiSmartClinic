
using Newtonsoft.Json;
using WebApiSmartClinic.Dto.Atividade;
using WebApiSmartClinic.Dto.Exercicio;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Dto.Evolucao;

public class EvolucaoEdicaoDto
{
    public int Id { get; set; }
    public string Observacao { get; set; }
    public DateTime DataEvolucao { get; set; }
    public int? PacienteId { get; set; }
    public int? ProfissionalId { get; set; }
    public List<ExercicioEdicaoDto>? Exercicios { get; set; }
    public List<AtividadeEdicaoDto>? Atividades { get; set; }
}
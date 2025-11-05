
using Newtonsoft.Json;
using WebApiSmartClinic.Dto.Atividade;
using WebApiSmartClinic.Dto.Exercicio;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Dto.Evolucao;

public sealed class EvolucaoCreateDto
{
    public string Observacao { get; set; }
    public DateTime DataEvolucao { get; set; }
    public int? PacienteId { get; set; }
    public int? ProfissionalId { get; set; }
    public List<ExercicioCreateDto>? Exercicios { get; set; }
    public List<AtividadeCreateDto>? Atividades { get; set; }
}
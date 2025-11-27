
using Newtonsoft.Json;
using WebApiSmartClinic.Dto.Atividade;
using WebApiSmartClinic.Dto.Exercicio;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Dto.Evolucao;

public sealed class EvolucaoEdicaoDto : EvolucaoModel
{
    public new List<ExercicioEdicaoDto>? Exercicios { get; set; }
    public new List<AtividadeEdicaoDto>? Atividades { get; set; }
}
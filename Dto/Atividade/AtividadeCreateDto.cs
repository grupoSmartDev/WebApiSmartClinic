
using System.Text.Json.Serialization;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Dto.Atividade
{
    public class AtividadeCreateDto
    {
        public string? Titulo { get; set; }
        public string Descricao { get; set; }
        public string? Tempo { get; set; }
        //public int? EvolucaoId { get; set; }
    }
}
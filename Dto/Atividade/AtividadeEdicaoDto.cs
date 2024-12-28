
namespace WebApiSmartClinic.Dto.Atividade
{
    public class AtividadeEdicaoDto
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public string? Tempo { get; set; }
        public int? EvolucaoId { get; set; }
    }
}
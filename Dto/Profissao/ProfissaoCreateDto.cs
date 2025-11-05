
namespace WebApiSmartClinic.Dto.Profissao
{
    public sealed class ProfissaoCreateDto
    {
        public string Descricao { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
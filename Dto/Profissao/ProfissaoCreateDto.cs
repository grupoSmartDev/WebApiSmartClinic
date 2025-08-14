
namespace WebApiSmartClinic.Dto.Profissao
{
    public class ProfissaoCreateDto
    {
        public string Descricao { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
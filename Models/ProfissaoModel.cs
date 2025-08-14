
namespace WebApiSmartClinic.Models
{
    public class ProfissaoModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool IsSystemDefault { get; internal set; }
        public bool Ativo { get; set; } = true;
    }
}
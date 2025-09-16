
namespace WebApiSmartClinic.Models
{
    public class ConselhoModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public bool IsSystemDefault { get; internal set; }
    }
}
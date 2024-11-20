
namespace WebApiSmartClinic.Models
{
    public class LogUsuarioModel
    {
        public int Id { get; set; }
        public int IdMovimentacao { get; set; }
        public string Descricao { get; set; }
        public string Rotina { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataMovimentacao { get; set; } = DateTime.Now;
    }
}
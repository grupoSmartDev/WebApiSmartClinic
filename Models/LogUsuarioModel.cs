
namespace WebApiSmartClinic.Models
{
    public class LogUsuarioModel
    {
        public int Id { get; set; }
        public int IdMovimentacao { get; set; }
        public string Descricao { get; set; }
        public string Rotina { get; set; }
        public int UsuarioId { get; set; }
        private DateTime? _DataMovimentacao;
        public DateTime? DataMovimentacao 
        {
            get => _DataMovimentacao;
            set => _DataMovimentacao = value.HasValue ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc) : DateTime.UtcNow;
        }
    }
}
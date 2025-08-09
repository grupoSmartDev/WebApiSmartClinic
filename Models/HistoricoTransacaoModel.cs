
using System.Text.Json.Serialization;

namespace WebApiSmartClinic.Models
{
    public class HistoricoTransacaoModel
    {
        public int Id { get; set; }
        public int BancoId { get; set; }  // Relacionamento com a conta bancária associada
        public BancoModel Banco { get; set; }  // Propriedade de navegação para a conta bancária
        private DateTime? _DataTransacao;
        public DateTime? DataTransacao
        {
            get => _DataTransacao;
            set => _DataTransacao = value.HasValue ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc) : null;
        }  // Data e hora da transação
        public string TipoTransacao { get; set; }  // Tipo da transação: "Débito", "Crédito", "Estorno", etc.
        public decimal Valor { get; set; }  // Valor da transação (positiva ou negativa, dependendo do tipo)
        public string Descricao { get; set; }  // Descrição ou observação sobre a transação
        public string Referencia { get; set; }  // Informação adicional, como número de documento ou ID de pagamento
        //public int? UsuarioId { get; set; }  // ID do usuário que realizou a transação (opcional, útil para auditoria)
        public string UsuarioId { get; set; }  // ID do usuário que realizou a transação (opcional, útil para auditoria)

        [JsonIgnore]
        public UsuarioModel Usuario { get; set; }  // Propriedade de navegação para o usuário que fez a transação
    }
}

using System.Text.Json.Serialization;
using WebApiSmartClinic.Models.Abstractions;

namespace WebApiSmartClinic.Models
{
    public class HistoricoTransacaoModel : IEntidadeEmpresa, IEntidadeAuditavel
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public string? UsuarioCriacaoId { get; set; }
        private DateTime _DataCriacao = DateTime.UtcNow;
        public DateTime DataCriacao
        {
            get => _DataCriacao.ToLocalTime();
            set => _DataCriacao = DateTime.SpecifyKind(value.ToUniversalTime(), DateTimeKind.Utc);
        }
        public string? UsuarioAlteracaoId { get; set; }
        private DateTime? _DataAlteracao;
        public DateTime? DataAlteracao
        {
            get => _DataAlteracao?.ToLocalTime();
            set => _DataAlteracao = value.HasValue ? DateTime.SpecifyKind(value.Value.ToUniversalTime(), DateTimeKind.Utc) : null;
        }
        public bool Ativo { get; set; } = true;        
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
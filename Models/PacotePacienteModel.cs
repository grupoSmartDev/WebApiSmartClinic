using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebApiSmartClinic.Models.Abstractions;

namespace WebApiSmartClinic.Models
{
    /// <summary>
    /// Representa um pacote vendido/vinculado a um paciente específico
    /// Controla quantidade total, usada e disponível
    /// </summary>
    public class PacotePacienteModel : IEntidadeEmpresa, IEntidadeAuditavel
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

        [Required(ErrorMessage = "O pacote é obrigatório")]
        public int PacoteId { get; set; }

        [JsonIgnore]
        public PacoteModel? Pacote { get; set; }

        [Required(ErrorMessage = "O paciente é obrigatório")]
        public int PacienteId { get; set; }

        [JsonIgnore]
        public PacienteModel? Paciente { get; set; }

        /// <summary>
        /// Referência ao financeiro de pagamento do pacote
        /// </summary>
        public int? FinanceiroId { get; set; }

        [JsonIgnore]
        public Financ_ReceberModel? Financeiro { get; set; }

        [Required]
        [Range(1, 999)]
        public int QuantidadeTotal { get; set; }

        [Range(0, 999)]
        public int QuantidadeUsada { get; set; } = 0;

        /// <summary>
        /// Quantidade disponível (calculada: Total - Usada)
        /// </summary>
        public int QuantidadeDisponivel => QuantidadeTotal - QuantidadeUsada;

        private DateTime _DataCompra = DateTime.UtcNow;
        public DateTime DataCompra
        {
            get => _DataCompra.ToLocalTime();
            set => _DataCompra = DateTime.SpecifyKind(value.ToUniversalTime(), DateTimeKind.Utc);
        }

        /// <summary>
        /// Status: Ativo, Esgotado, Cancelado
        /// </summary>
        [MaxLength(20)]
        public string Status { get; set; } = "Ativo";

        [MaxLength(500)]
        public string? Observacao { get; set; }

        // Relacionamento: Histórico de usos deste pacote
        [JsonIgnore]
        public virtual ICollection<PacoteUsoModel> Usos { get; set; } = new List<PacoteUsoModel>();
    }
}
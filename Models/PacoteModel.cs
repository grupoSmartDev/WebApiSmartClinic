using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebApiSmartClinic.Models.Abstractions;

namespace WebApiSmartClinic.Models
{
    /// <summary>
    /// Representa o template/cadastro de um pacote de sessões
    /// Exemplo: "Pacote 10 Massagens - R$ 150,00"
    /// </summary>
    public class PacoteModel : IEntidadeEmpresa, IEntidadeAuditavel
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

        [Required(ErrorMessage = "A descrição é obrigatória")]
        [MaxLength(200)]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "O procedimento é obrigatório")]
        public int ProcedimentoId { get; set; }

        [JsonIgnore]
        public ProcedimentoModel? Procedimento { get; set; }

        [Required(ErrorMessage = "A quantidade de sessões é obrigatória")]
        [Range(1, 999, ErrorMessage = "A quantidade deve ser entre 1 e 999")]
        public int QuantidadeSessoes { get; set; }

        [Required(ErrorMessage = "O valor é obrigatório")]
        [Range(0.01, 999999.99, ErrorMessage = "O valor deve ser maior que zero")]
        public decimal Valor { get; set; }

        public int? CentroCustoId { get; set; }

        [JsonIgnore]
        public CentroCustoModel? CentroCusto { get; set; }

        [MaxLength(500)]
        public string? Observacao { get; set; }

        // Relacionamento: Um pacote pode ser vendido para vários pacientes
        [JsonIgnore]
        public virtual ICollection<PacotePacienteModel> PacotesVendidos { get; set; } = new List<PacotePacienteModel>();
    }
}
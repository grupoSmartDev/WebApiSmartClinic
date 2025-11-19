using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebApiSmartClinic.Models.Abstractions;

namespace WebApiSmartClinic.Models
{
    /// <summary>
    /// Representa o histórico de uso/consumo de uma sessão do pacote
    /// Registra quando, onde e por quem foi utilizado
    /// </summary>
    public class PacoteUsoModel : IEntidadeEmpresa, IEntidadeAuditavel
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

        [Required(ErrorMessage = "O pacote do paciente é obrigatório")]
        public int PacotePacienteId { get; set; }

        [JsonIgnore]
        public PacotePacienteModel? PacotePaciente { get; set; }

        [Required(ErrorMessage = "O agendamento é obrigatório")]
        public int AgendaId { get; set; }

        [JsonIgnore]
        public AgendaModel? Agenda { get; set; }

        /// <summary>
        /// Paciente que efetivamente utilizou a sessão
        /// Pode ser diferente do dono do pacote (ex: Cleiton usa no filho)
        /// </summary>
        [Required(ErrorMessage = "O paciente que utilizou é obrigatório")]
        public int PacienteUtilizadoId { get; set; }

        [JsonIgnore]
        public PacienteModel? PacienteUtilizado { get; set; }

        private DateTime _DataUso = DateTime.UtcNow;
        public DateTime DataUso
        {
            get => _DataUso.ToLocalTime();
            set => _DataUso = DateTime.SpecifyKind(value.ToUniversalTime(), DateTimeKind.Utc);
        }

        [MaxLength(500)]
        public string? Observacao { get; set; }
    }
}

using Newtonsoft.Json;
using WebApiSmartClinic.Dto.Financ_Receber;
using WebApiSmartClinic.Models.Abstractions;

namespace WebApiSmartClinic.Models
{
    public class EvolucaoModel : IEntidadeEmpresa, IEntidadeAuditavel, IEntidadeDoProfissional
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
        public bool Ativo { get; set; }
        public string Observacao { get; set; }
        private DateTime? _DataEvolucao;
        public DateTime? DataEvolucao 
        {
            get => _DataEvolucao;
            set => _DataEvolucao = value.HasValue ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc) : null;
        }
        public int? PacienteId { get; set; }

        [JsonIgnore]
        public virtual PacienteModel? Paciente { get; set; }
        public int? ProfissionalId { get; set; }
        public ICollection<AtividadeModel>? Atividades { get; set; } = new List<AtividadeModel>();
        public ICollection<ExercicioModel>? Exercicios { get; set; } = new List<ExercicioModel>();
    }
}
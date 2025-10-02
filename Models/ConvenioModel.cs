
using System.Text.Json.Serialization;
using WebApiSmartClinic.Models.Abstractions;

namespace WebApiSmartClinic.Models
{
    public class ConvenioModel : IEntidadeEmpresa, IEntidadeAuditavel
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
        public string Nome { get; set; }
        public string RegistroAvs { get; set; }
        public string PeriodoCarencia { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        //public bool Ativo { get; set; } = true;
        public bool IsSystemDefault { get; internal set; }
    }
}
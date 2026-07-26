using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WebApiSmartClinic.Models.Abstractions;

namespace WebApiSmartClinic.Models
{

    public class PlanoModel : IEntidadeEmpresa, IEntidadeAuditavel
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

        [Required(ErrorMessage = "A descri��o � obrigat�ria.")]
        [StringLength(255, ErrorMessage = "A descri��o deve ter no m�ximo 255 caracteres.")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "O tempo em minutos � obrigat�rio.")]
        [Range(1, int.MaxValue, ErrorMessage = "O tempo em minutos deve ser maior que zero.")]
        public int TempoMinutos { get; set; }

        [Required(ErrorMessage = "Os dias da semana s�o obrigat�rios.")]
        [Range(1, 7, ErrorMessage = "Os dias da semana devem estar entre 1 e 7.")]
        public int DiasSemana { get; set; }

        public int? CentroCustoId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "O valor deve ser maior ou igual a zero.")]
        public decimal? ValorBimestral { get; set; }
        public decimal? ValorTrimestral { get; set; }
        public decimal? ValorQuadrimestral { get; set; }
        public decimal? ValorSemestral { get; set; }
        public decimal? ValorAnual { get; set; }
        public decimal? ValorMensal { get; set; }

        private DateTime? _dataInicio;
        private DateTime? _dataFim;

        [DataType(DataType.Date)]
        public DateTime? DataInicio
        {
            get => _dataInicio;
            set => _dataInicio = value.HasValue ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc) : null;
        }

        [DataType(DataType.Date)]
        public DateTime? DataFim
        {
            get => _dataFim;
            set => _dataFim = value.HasValue ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc) : null;
        }

        //public bool Ativo { get; set; } = true;

        // Calculado dinamicamente a cada leitura - nunca persistido. "Ativo" (flag de soft-delete/
        // inativação manual) só reflete se o plano foi explicitamente inativado/renovado; um plano
        // com DataFim no passado continua Ativo=true até alguém mexer nele, então o status exibido
        // ao usuário precisa considerar a data, não só a flag.
        [NotMapped]
        public string Status
        {
            get
            {
                if (!Ativo) return "Inativo";
                if (DataFim.HasValue && DataFim.Value < DateTime.UtcNow) return "Vencido";
                return "Ativo";
            }
        }

        public int? PacienteId { get; set; }
        [JsonIgnore]
        public PacienteModel? Paciente { get; set; }
        public virtual ICollection<PacientePlanoHistoricoModel> HistoricoPacientes { get; set; } = new List<PacientePlanoHistoricoModel>(); // Hist�rico de uso

        public int? FinanceiroId { get; set; }
        [JsonIgnore]
        public Financ_ReceberModel? Financeiro { get; set; }

        [Required(ErrorMessage = "O tipo de m�s � obrigat�rio.")]
        [StringLength(1, ErrorMessage = "O tipo de m�s deve ter apenas um caractere.")]
        public string TipoMes { get; set; } = string.Empty;
    }
}

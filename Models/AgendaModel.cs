
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Globalization;

namespace WebApiSmartClinic.Models
{
    public class AgendaModel
    {
        public int Id { get; set; }

        public string Titulo { get; set; }


        //private DateTime? _Data;
        //public DateTime? Data
        //{
        //    get => _Data?.ToLocalTime();
        //    set => _Data = value.HasValue
        //        ? DateTime.SpecifyKind(value.Value.ToUniversalTime(), DateTimeKind.Utc)
        //        : null;
        //}
        private DateTime? _Data;
        public DateTime? Data
        {
            get => _Data;
            set => _Data = value.HasValue
                ? DateTime.SpecifyKind(value.Value, DateTimeKind.Unspecified)
                : null;
        }

        private DateTime? _DataFimRecorrencia;
        public DateTime? DataFimRecorrencia
        {
            get => _DataFimRecorrencia?.ToLocalTime();
            set => _DataFimRecorrencia = value.HasValue
                ? DateTime.SpecifyKind(value.Value.ToUniversalTime(), DateTimeKind.Utc)
                : null;
        }

        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }
        public int? PacienteId { get; set; }
        public PacienteModel? Paciente { get; set; }
        public int? ProfissionalId { get; set; }
        public ProfissionalModel? Profissional { get; set; }
        public bool Convenio { get; set; } = false;
        public decimal? Valor { get; set; }
        public string? FormaPagamento { get; set; }
        public bool Pago { get; set; }
        public int? FinancReceberId { get; set; }
        public Financ_ReceberModel? FinancReceber { get; set; }
        public int? SalaId { get; set; }
        public virtual SalaModel? Sala { get; set; }
        public int? PacoteId { get; set; }
        public PacienteModel? Pacote {get; set;}
        public bool LembreteSms { get; set; } = false;
        public bool LembreteWhatsapp { get; set; } = false;
        public bool LembreteEmail { get; set; } = false;
        public int? StatusId { get; set; }
        public StatusModel? Status { get; set; }
        public bool IntegracaoGmail { get; set; } = false;
        public bool Avulso { get; set; } = false;
        public bool StatusFinal { get; set; } = false;
        public List<DayOfWeek>? DiasRecorrencia { get; set; }

        public virtual ComissaoCalculadaModel? ComissaoCalculada { get; set; }

        public bool Ativo { get; set; } = true;
        private DateTime? _DataAlteracao;
        public DateTime? DataAlteracao
        {
            get => _DataAlteracao;
            set => _DataAlteracao = value.HasValue ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc) : null;
        }

        private DateTime _DataCriacao;
        public DateTime DataCriacao
        {
            get => _DataCriacao;
            set => _DataCriacao = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        } 
    }
}
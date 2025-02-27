
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Globalization;

namespace WebApiSmartClinic.Models
{
    public class AgendaModel
    {
        public int Id { get; set; }

        public string Titulo { get; set; }

        private DateTime? _date;

        [DataType(DataType.Date)]
        public DateTime? Data
        {
            get => _date;
            set => _date = value.HasValue ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc) : null;
        }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }
        public int? PacienteId { get; set; }
        public PacienteModel? Paciente { get; set; }
        public int? ProfissionalId { get; set; }
        public ProfissionalModel? Profissional { get; set; }
        public bool Convenio { get; set; } = false;
        public decimal? Valor { get; set; }
        public string FormaPagamento { get; set; }
        public bool Pago { get; set; }
        public int? FinancReceberId { get; set; }
        public Financ_ReceberModel? FinancReceber { get; set; }
        public int? SalaId { get; set; }
        public int? PacoteId { get; set; }
        public PacienteModel Pacote {get; set;}
        public bool LembreteSms { get; set; } = false;
        public bool LembreteWhatsapp { get; set; } = false;
        public bool LembreteEmail { get; set; } = false;
        public int? StatusId { get; set; }
        public StatusModel? Status { get; set; }
        public bool IntegracaoGmail { get; set; } = false;
        public bool StatusFinal { get; set; } = false;
    }
}

using System.ComponentModel.DataAnnotations;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Dto.Agenda
{
    public class AgendaCreateDto
    {
        public string Titulo { get; set; }
        public DateTime? Data { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFim { get; set; }
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
        public int? PacoteId { get; set; }
        public PlanoModel? Pacote { get; set; }
        public bool LembreteSms { get; set; } = false;
        public bool LembreteWhatsapp { get; set; } = false;
        public bool LembreteEmail { get; set; } = false;
        public int? StatusId { get; set; }
        public StatusModel? Status { get; set; }
        public bool IntegracaoGmail { get; set; } = false;
        public bool StatusFinal { get; set; } = false;
        public DateTime? DataFimRecorrencia { get; set; }
        public List<DayOfWeek>? DiasRecorrencia { get; set; }
    }
}
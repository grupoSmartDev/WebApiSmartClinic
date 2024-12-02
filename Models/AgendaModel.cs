
using System.Drawing;
using System.Globalization;

namespace WebApiSmartClinic.Models
{
    public class AgendaModel
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }
        public int PacienteId { get; set; }
        public int ProfissionalId { get; set; }
        public bool Convenio { get; set; }
        public decimal? Valor { get; set; }
        public string FormaPagamento { get; set; }
        public bool Pago { get; set; }
        public int? FinanceiroId { get; set; }
        public int SalaId { get; set; }
        public int? PacoteId { get; set; }
        public bool LembreteSms { get; set; }
        public bool LembreteWhatsapp { get; set; }
        public bool LembreteEmail { get; set; }
        public string Status { get; set; }
        public string CorStatus { get; set; }
        public bool IntegracaoGmail { get; set; }
    }
}
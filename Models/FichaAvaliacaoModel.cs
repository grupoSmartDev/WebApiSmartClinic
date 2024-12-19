
namespace WebApiSmartClinic.Models
{
    public class FichaAvaliacaoModel
    {
        public int Id { get; set; }
        public string QueixaPrincipal {get; set; }
        public string HistoriaPregressa {get; set; }
        public string HistoriaAtual {get; set; }
        public string TipoDor {get; set; }
        public string? SinaisVitais {get; set; }
        public string? Medicamentos {get; set; }
        public string? DoencasCronicas {get; set; }
        public string? Cirurgia {get; set; }
        public string? DoencaNeurodegenerativa {get; set; }
        public string? TratamentosRealizados {get; set; }
        public string? AlergiaMedicamentos {get; set; }
        public string FrequenciaConsumoAlcool {get; set; }
        public string? ObjetivoTratamento { get; set; }
        public bool PraticaAtividade { get; set; } = false;
        public bool Tabagista {get; set; } = false;
    }
}
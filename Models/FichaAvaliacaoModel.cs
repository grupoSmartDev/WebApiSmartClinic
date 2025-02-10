
namespace WebApiSmartClinic.Models
{
    public class FichaAvaliacaoModel
    {
        public int Id { get; set; }
        public int? PacienteId { get; set; }
        public PacienteModel? Paciente { get; set; }
        private DateTime? _DataAvaliacao;
        public DateTime? DataAvaliacao
        {
            get => _DataAvaliacao?.ToLocalTime();
            set => _DataAvaliacao = value.HasValue
                ? DateTime.SpecifyKind(value.Value.ToUniversalTime(), DateTimeKind.Utc)
                : null;
        }
        public string? Especialidade { get; set; }
        public int? Idade { get; set; }
        public double? Altura { get; set; }
        public double? Peso { get; set; }
        public string? Sexo { get; set; }
        public string? ObservacoesGerais { get; set; }
        public bool HistoricoDoencas { get; set; }
        public string? DoencasPreExistentes { get; set; }
        public bool MedicacaoUsoContinuo { get; set; }
        public string? Medicacao { get; set; }
        public bool CirurgiasPrevias { get; set; }
        public string? DetalheCirurgias { get; set; }
        public string? Alergias { get; set; }
        public string? QueixaPrincipal { get; set; }
        public string? ObjetivosDoTratamento { get; set; }
        public double? IMC { get; set; }
        public string? AvaliacaoPostural { get; set; }
        public string? AmplitudeMovimento { get; set; }
        public string? AssinaturaProfissional { get; set; }
        public string? AssinaturaCliente { get; set; }
        public string? HistoriaPregressa { get; set; }
        public string? HistoriaAtual { get; set; }
        public string? TipoDor { get; set; }
        public string? SinaisVitais { get; set; }
        public string? DoencasCronicas { get; set; }
        public string? Cirurgia { get; set; }
        public string? DoencaNeurodegenerativa { get; set; }
        public string? TratamentosRealizados { get; set; }
        public string? AlergiaMedicamentos { get; set; }
        public string? FrequenciaConsumoAlcool { get; set; }
        public bool PraticaAtividade { get; set; } = false;
        public bool Tabagista { get; set; } = false;
        public int? ProfissionalId { get; set; }
        public ProfissionalModel? Profissional { get; set; }
    }
}
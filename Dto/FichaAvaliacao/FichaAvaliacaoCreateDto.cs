
using System.Text.Json.Serialization;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Dto.FichaAvaliacao;

public class FichaAvaliacaoCreateDto
{
    public int PacienteId { get; set; }
    [JsonIgnore]
    public PacienteModel? Paciente { get; set; }
    public DateTime DataAvaliacao { get; set; }
    public int ProfissionalId { get; set; }
    [JsonIgnore]
    public ProfissionalModel? Profissional { get; set; }

    public string? Especialidade { get; set; }
    public int Idade { get; set; }
    public decimal Altura { get; set; }
    public decimal Peso { get; set; }
    public string Sexo { get; set; }
    public string? ObservacoesGerais { get; set; }
    public bool HistoricoDoencas { get; set; } = false;
    public string? DoencasPreExistentes { get; set; }
    public bool MedicacaoUsoContinuo { get; set; } = false;
    public string? Medicacao { get; set; }
    public bool CirurgiasPrevias { get; set; } = false;
    public string? DetalheCirurgias { get; set; }
    public string? Alergias { get; set; }
    public string? QueixaPrincipal { get; set; }
    public string? ObjetivosDoTratamento { get; set; }
    public decimal? Imc { get; set; }
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
}
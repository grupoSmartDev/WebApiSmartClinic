
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.Intrinsics.X86;
using System.Text.Json.Serialization;

namespace WebApiSmartClinic.Models;

public class EmpresaModel
{
    public int Id { get; set; }
    private DateTime? _DataInicio;
    public DateTime? DataInicio
    {
        get => _DataInicio?.ToLocalTime();
        set => _DataInicio = value.HasValue
            ? DateTime.SpecifyKind(value.Value.ToUniversalTime(), DateTimeKind.Utc)
            : null;
    }
    private DateTime? _DataFim;
    public DateTime? DataFim
    {
        get => _DataFim?.ToLocalTime();
        set => _DataFim = value.HasValue
            ? DateTime.SpecifyKind(value.Value.ToUniversalTime(), DateTimeKind.Utc)
            : null;
    }
    private DateTime? _DataInicioTeste;
    public DateTime? DataInicioTeste
    {
        get => _DataInicioTeste?.ToLocalTime();
        set => _DataInicioTeste = value.HasValue
            ? DateTime.SpecifyKind(value.Value.ToUniversalTime(), DateTimeKind.Utc)
            : null;
    }
    private DateTime? _DataNascimentoTitular;
    public DateTime? DataNascimentoTitular
    {
        get => _DataNascimentoTitular?.ToLocalTime();
        set => _DataNascimentoTitular = value.HasValue
            ? DateTime.SpecifyKind(value.Value.ToUniversalTime(), DateTimeKind.Utc)
            : null;
    }
    // Data de Criação
    private DateTime _DataCriacao = DateTime.UtcNow;
    public DateTime DataCriacao
    {
        get => _DataCriacao.ToLocalTime();
        set => _DataCriacao = DateTime.SpecifyKind(value.ToUniversalTime(), DateTimeKind.Utc);
    }
    public string Nome { get; set; }
    public string TitularCPF { get; set; }
    public string CNPJEmpresaMatriz { get; set; }
    public string Email { get; set; }
    public string Celular { get; set; }
    public string Sobrenome { get; set; }
    public string Especialidade { get; set; } // fisioterapia, psicologia, etc...) 
    public string PlanoEscolhido { get; set; }
    public string? TelefoneFixo { get; set; }
    public bool Ativo { get; set; } = true;
    public bool PeriodoTeste { get; set; }
    public bool CelularComWhatsApp { get; set; }
    public bool ReceberNotificacoes { get; set; } = true;
    public int QtdeLicencaEmpresaPermitida { get; set; }
    public int QtdeLicencaUsuarioPermitida { get; set; }
    public int QtdeLicencaEmpresaUtilizada { get; set; }
    public int QtdeLicencaUsuarioUtilizada { get; set; }
    public int QtdeLicencaFilialPermitida { get; set; }
    public int QtdeLicencaFilialUtilizada { get; set; }
    public string? StripeCustomerId { get; set; }
    public int TipoPagamentoId { get; set; }
    [JsonIgnore]
    public TipoPagamentoModel? TipoPagamento { get; set; }
    [JsonIgnore]
    public ICollection<UsuarioModel> Usuarios { get; set; } = new List<UsuarioModel>();
    [JsonIgnore]
    public ICollection<FilialModel>? Filiais { get; set; } = new List<FilialModel>();
    [JsonIgnore]
    public ICollection<SubscriptionModel> Subscription { get; set; } = new List<SubscriptionModel>();
    public string DatabaseConnectionString { get; internal set; }

    public string AsaasCustomerId { get; set; } // ID do customer no Asaas
    public string AsaasSubscriptionId { get; set; } // ID da subscription no Asaas
    public string PeriodoCobranca { get; set; } // "monthly" ou "semiannual"
    public decimal PrecoSelecionado { get; set; }

}

using System.Text.Json.Serialization;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Dto.CadastroCliente
{
    public sealed class CadastroClienteCreateDto
    {
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

        public string? Nome { get; set; }
        public string? TitularCPF { get; set; }
        public string? CNPJEmpresaMatriz { get; set; }
        public string? Email { get; set; }
        public string? Celular { get; set; }
        public string? Sobrenome { get; set; }
        public string? Especialidade { get; set; } // fisioterapia, psicologia, etc...) 
        public string? PlanoEscolhido { get; set; }
        public string? TelefoneFixo { get; set; }
        public bool Ativo { get; set; } = true;
        public bool PeriodoTeste { get; set; } = false;
        public bool CelularComWhatsApp { get; set; }
        public bool ReceberNotificacoes { get; set; } = true;
        public int? TipoPagamentoId { get; set; }
        public int? QtdeLicencaEmpresaPermitida { get; set; }
        public int? QtdeLicencaUsuarioPermitida { get; set; }
        public int? QtdeLicencaEmpresaUtilizada { get; set; }
        public int? QtdeLicencaUsuarioUtilizada { get; set; }

        [JsonIgnore]
        public TipoPagamentoModel? TipoPagamento { get; set; }

        public string PeriodoCobranca { get; set; } // "monthly" ou "semiannual"
        public decimal PrecoSelecionado { get; set; }

    }


}
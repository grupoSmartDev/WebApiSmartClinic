
using System.Text.Json.Serialization;
using WebApiSmartClinic.Models.Abstractions;

namespace WebApiSmartClinic.Models
{
    public class Financ_ReceberModel : IEntidadeEmpresa, IEntidadeAuditavel
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
        public int? IdOrigem { get; set; } = 0;
        public int? NrDocto { get; set; }
        private DateTime? _DataEmissao;
        public DateTime? DataEmissao
        {
            get => _DataEmissao;
            set => _DataEmissao = value.HasValue
                ? DateTime.SpecifyKind(value.Value.ToUniversalTime(), DateTimeKind.Utc)
                : null;
        }
        public decimal? ValorOriginal { get; set; }
        public decimal? ValorPago { get; set; }
        public decimal? Valor { get; set; }
        public int? Parcela { get; set; }
        public string? Status { get; set; } // Pendente, Pago, Atrasado, Cancelado
        public string? NotaFiscal { get; set; }
        public string? Descricao { get; set; }
        public string? Classificacao { get; set; }
        public string? Observacao { get; set; }
        public int? PacienteId { get; set; } = null; // Relacionamento com a tabela de Fornecedor

        
        public PacienteModel? Paciente { get; set; }
        public int? FornecedorId { get; set; } = null; // Relacionamento com a tabela de Fornecedor

        
        public FornecedorModel? Fornecedor { get; set; }
        public int? CentroCustoId { get; set; } = null; // Relacionamento com a tabela de CentroCusto

        [JsonIgnore]
        public CentroCustoModel? CentroCusto { get; set; }

        [JsonIgnore]
        public BancoModel? Banco { get; set; }
        public int? BancoId { get; set; } = 0; // Relacionamento com a tabela de Banco
        public int TipoPagamentoId { get; set; }
        public TipoPagamentoModel? TipoPagamento { get; set; }

        public ICollection<Financ_ReceberSubModel>? subFinancReceber { get; set; } = new List<Financ_ReceberSubModel>();
    }

    public class Financ_ReceberSubModel
    {
        public int? Id { get; set; }
        public int? financReceberId { get; set; }

        //JsonIgnore]
        public Financ_ReceberModel? FinancReceber { get; set; }
        public int? Parcela { get; set; }
        public decimal Valor { get; set; }
        public decimal ValorPago { get; set; }
        public decimal? Desconto { get; set; }
        public decimal? Juros { get; set; }
        public decimal? Multa { get; set; }

        public int? TipoPagamentoId { get; set; } = null; // Relacionamento com a tabela de TipoPagamento

        [JsonIgnore]
        public TipoPagamentoModel? TipoPagamento { get; set; }
        public int? FormaPagamentoId { get; set; } = null; // Relacionamento com a tabela de FormaPagamento

        [JsonIgnore]
        public FormaPagamentoModel? FormaPagamento { get; set; }

        private DateTime? _DataPagamento;
        public DateTime? DataPagamento
        {
            get => _DataPagamento;
            set => _DataPagamento = value.HasValue
                ? DateTime.SpecifyKind(value.Value.ToUniversalTime(), DateTimeKind.Utc)
                : null;
        }
        private DateTime? _DataVencimento;
        public DateTime? DataVencimento
        {
            get => _DataVencimento;
            set => _DataVencimento = value.HasValue
                ? DateTime.SpecifyKind(value.Value.ToUniversalTime(), DateTimeKind.Utc)
                : null;
        }
        public string? Observacao { get; set; }
    }
}
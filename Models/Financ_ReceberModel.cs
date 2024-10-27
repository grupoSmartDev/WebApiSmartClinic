
using System.Text.Json.Serialization;

namespace WebApiSmartClinic.Models
{
    public class Financ_ReceberModel
    {
        public int Id { get; set; }
        public int IdOrigem { get; set; }
        public int NrDocto { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public decimal ValorOriginal { get; set; }
        public decimal? ValorPago { get; set; }
        public string Status { get; set; } // Pendente, Pago, Atrasado, Cancelado
        public string NotaFiscal { get; set; }
        public string Descricao { get; set; }
        public string Parcela { get; set; }
        public string Classificacao { get; set; }
        public decimal? Desconto { get; set; }
        public decimal? Juros { get; set; }
        public decimal? Multa { get; set; }
        public string Observacao { get; set; }

        [JsonIgnore]
        public FornecedorModel Fornecedor { get; set; }
        public int? FornecedorId { get; set; } = 0; // Relacionamento com a tabela de Fornecedor

        [JsonIgnore]
        public CentroCustoModel CentroCusto { get; set; }
        public int? CentroCustoId { get; set; } = 0; // Relacionamento com a tabela de CentroCusto

        [JsonIgnore]
        public TipoPagamentoModel TipoPagamento { get; set; }
        public int? TipoPagamentoId { get; set; } = 0; // Relacionamento com a tabela de TipoPagamento

        [JsonIgnore]
        public FormaPagamentoModel FormaPagamento { get; set; }
        public int? FormaPagamentoId { get; set; } = 0; // Relacionamento com a tabela de FormaPagamento

        [JsonIgnore]
        public BancoModel Banco { get; set; }
        public int? BancoId { get; set; } = 0; // Relacionamento com a tabela de Banco
    }
}

using System.Text.Json.Serialization;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Dto.Financ_Receber
{
    public class Financ_ReceberCreateDto
    {
        public int? IdOrigem { get; set; }
        public int? NrDocto { get; set; }
        public DateTime DataEmissao { get; set; }
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
        public int? FornecedorId { get; set; } = null; // Relacionamento com a tabela de Fornecedor
        public int? CentroCustoId { get; set; } = null; // Relacionamento com a tabela de CentroCusto
        public int? BancoId { get; set; } = 0; // Relacionamento com a tabela de Banco
        public ICollection<Financ_ReceberSubCreateDto>? subFinancReceber { get; set; }

    }

    public class Financ_ReceberSubCreateDto
    {
        public int? financReceberId { get; set; }
        public int? Parcela { get; set; }
        public decimal Valor { get; set; }
        public decimal ValorPago { get; set; }
        public decimal? Desconto { get; set; }
        public decimal? Juros { get; set; }
        public decimal? Multa { get; set; }
        public int? TipoPagamentoId { get; set; } = null; // Relacionamento com a tabela de TipoPagamento
        public int? FormaPagamentoId { get; set; } = null; // Relacionamento com a tabela de FormaPagamento
        public DateTime? DataPagamento { get; set; }
        public DateTime? DataVencimento { get; set; }
        public string? Observacao { get; set; }
    }
}
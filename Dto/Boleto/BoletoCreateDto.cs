
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Dto.Boleto
{
    public class BoletoCreateDto
    {
        public string NossoNumero { get; set; } // Número único do boleto
        public string NumeroDocumento { get; set; } // Número do documento
        public decimal Valor { get; set; } // Valor do boleto
        public DateTime DataVencimento { get; set; } // Data de vencimento do boleto
        public decimal Juros { get; set; } // Valor de juros (se aplicável)
        public decimal Multa { get; set; } // Valor de multa (se aplicável)

        // Dados do Sacado (quem vai pagar o boleto)
        public string NomeSacado { get; set; }
        public string DocumentoSacado { get; set; } // CPF ou CNPJ

        // Relacionamento com o banco emissor do boleto
        public int BancoId { get; set; }
        public BancoModel? Banco { get; set; }

        // Informações de status do boleto
        public bool Pago { get; set; }
        public DateTime? DataPagamento { get; set; }

        // Propriedades adicionais para homologação
        public string CodigoDeBarras { get; set; } // Código de barras gerado pelo banco
        public string LinhaDigitavel { get; set; } // Linha digitável gerada pelo banco
    }
}
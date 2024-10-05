
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Dto.Boleto
{
    public class BoletoCreateDto
    {
        public string NossoNumero { get; set; } // N�mero �nico do boleto
        public string NumeroDocumento { get; set; } // N�mero do documento
        public decimal Valor { get; set; } // Valor do boleto
        public DateTime DataVencimento { get; set; } // Data de vencimento do boleto
        public decimal Juros { get; set; } // Valor de juros (se aplic�vel)
        public decimal Multa { get; set; } // Valor de multa (se aplic�vel)

        // Dados do Sacado (quem vai pagar o boleto)
        public string NomeSacado { get; set; }
        public string DocumentoSacado { get; set; } // CPF ou CNPJ

        // Relacionamento com o banco emissor do boleto
        public int BancoId { get; set; }
        public BancoModel? Banco { get; set; }

        // Informa��es de status do boleto
        public bool Pago { get; set; }
        public DateTime? DataPagamento { get; set; }

        // Propriedades adicionais para homologa��o
        public string CodigoDeBarras { get; set; } // C�digo de barras gerado pelo banco
        public string LinhaDigitavel { get; set; } // Linha digit�vel gerada pelo banco
    }
}
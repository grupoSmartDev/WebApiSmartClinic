
namespace WebApiSmartClinic.Dto.Banco
{
    public class BancoCreateDto
    {
        public string NomeBanco { get; set; } 
        public string Codigo { get; set; }
        public string Agencia { get; set; }
        public string NumeroConta { get; set; }
        public string TipoConta { get; set; }
        public string NomeTitular { get; set; }
        public string DocumentoTitular { get; set; } 
        public decimal SaldoInicial { get; set; } 
        public bool Ativo { get; set; } = true;

        // Dados para homologação e emissão de boletos
        public string CodigoConvenio { get; set; } 
        public string Carteira { get; set; }
        public string VariacaoCarteira { get; set; }
        public string CodigoBeneficiario { get; set; } 
        public string NumeroContrato { get; set; }
        public string CodigoTransmissao { get; set; }
    }
}
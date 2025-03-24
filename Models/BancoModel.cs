
namespace WebApiSmartClinic.Models
{
    public class BancoModel
    {
        public int Id { get; set; }
        public string NomeBanco { get; set; } // Nome do Banco
        public string Codigo { get; set; } // C�digo do Banco (geralmente usado em transa��es)
        public string Agencia { get; set; } // Ag�ncia Banc�ria
        public string NumeroConta { get; set; } // N�mero da Conta
        public string TipoConta { get; set; } // Tipo de Conta (Corrente, Poupan�a, etc.)
        public string NomeTitular { get; set; } // Nome do Titular da Conta
        public string DocumentoTitular { get; set; } // Documento (CPF/CNPJ) do Titular da Conta
        public decimal SaldoInicial { get; set; } // Saldo inicial ao cadastrar a conta
        public bool Ativo { get; set; } = true; // Indica se a conta banc�ria est� ativa ou desativada

        // Dados para homologa��o e emiss�o de boletos
        public string CodigoConvenio { get; set; } // C�digo do Conv�nio fornecido pelo banco
        public string Carteira { get; set; } // Carteira de Cobran�a
        public string VariacaoCarteira { get; set; } // Varia��o da Carteira (se aplic�vel)
        public string CodigoBeneficiario { get; set; } // C�digo fornecido pelo banco para o benefici�rio
        public string NumeroContrato { get; set; } // N�mero do contrato com o banco para emiss�o de boletos
        public string CodigoTransmissao { get; set; } // C�digo de transmiss�o para arquivos de remessa
    }
}
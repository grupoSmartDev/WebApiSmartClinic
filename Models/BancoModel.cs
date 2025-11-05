using WebApiSmartClinic.Models.Abstractions;

namespace WebApiSmartClinic.Models;

public class BancoModel : IEntidadeEmpresa, IEntidadeAuditavel
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
    public string NomeBanco { get; set; } // Nome do Banco
    public string Codigo { get; set; } // Código do Banco (geralmente usado em transações)
    public string Agencia { get; set; } // Agência Bancária
    public string NumeroConta { get; set; } // Número da Conta
    public string TipoConta { get; set; } // Tipo de Conta (Corrente, Poupança, etc.)
    public string NomeTitular { get; set; } // Nome do Titular da Conta
    public string DocumentoTitular { get; set; } // Documento (CPF/CNPJ) do Titular da Conta
    public decimal SaldoInicial { get; set; } // Saldo inicial ao cadastrar a conta
    //public bool Ativo { get; set; } = true; // Indica se a conta bancária está ativa ou desativada

    // Dados para homologação e emissão de boletos
    public string? CodigoConvenio { get; set; } // Código do Convênio fornecido pelo banco
    public string? Carteira { get; set; } // Carteira de Cobrança
    public string? VariacaoCarteira { get; set; } // Variação da Carteira (se aplicável)
    public string? CodigoBeneficiario { get; set; } // Código fornecido pelo banco para o beneficiário
    public string? NumeroContrato { get; set; } // Número do contrato com o banco para emissão de boletos
    public string? CodigoTransmissao { get; set; } // Código de transmissão para arquivos de remessa
}
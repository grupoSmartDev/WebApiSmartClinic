namespace WebApiSmartClinic.Models;

public class ComissaoCalculadaModel
{
    public int Id { get; set; }

    // Foreign Key para Profissional
    public int ProfissionalId { get; set; }
    public virtual ProfissionalModel Profissional { get; set; }

    // Foreign Key para Agendamento (sessão realizada)
    public int AgendamentoId { get; set; }
    public virtual AgendaModel Agendamento { get; set; }

    // Dados da comissão
    public DateTime DataAgendamento { get; set; } // Data da sessão realizada
    public string? TipoComissaoUtilizado { get; set; } //no tipo de comissao é P para porcentagem e VF para valor fixo. 
    public decimal PercentualOuValor { get; set; } // % ou valor fixo usado
    public decimal ValorBase { get; set; } // Valor do procedimento/plano
    public decimal ValorComissao { get; set; } // Valor calculado da comissão

    // Informações do paciente/plano (para relatórios)
    public string? NomePaciente { get; set; }
    public string? NomePlano { get; set; }
    public string? Observacoes { get; set; }

    // Controle de pagamento
    public StatusComissao Status { get; set; } = StatusComissao.Pendente;
    public DateTime? DataPagamento { get; set; }
    public string? UsuarioPagamento { get; set; }

    // Auditoria
    public DateTime DataCalculo { get; set; } = DateTime.UtcNow;
    public bool Ativo { get; set; } = true;
}

public enum StatusComissao
{
    Pendente = 1,
    Pago = 2,
    Cancelado = 3
}
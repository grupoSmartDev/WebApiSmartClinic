namespace WebApiSmartClinic.Models;

public class PacientePlanoHistoricoModel
{
    public int Id { get; set; }

    // Foreign Keys
    public int PacienteId { get; set; }
    public int PlanoId { get; set; }

    // Navegação
    public virtual PacienteModel Paciente { get; set; }
    public virtual PlanoModel Plano { get; set; }

    // Dados do período do plano
    public DateTime DataInicio { get; set; }
    public DateTime? DataFim { get; set; } // Null se ainda estiver ativo
    public int AulasContratadas { get; set; } // Quantas aulas tinha o plano
    public int AulasUtilizadas { get; set; } = 0; // Quantas já foram usadas
    public decimal ValorPago { get; set; } // Valor pago pelo plano
    public string? Observacoes { get; set; }

    // Status
    public StatusPlano Status { get; set; } = StatusPlano.Ativo;
    public string? MotivoFinalizacao { get; set; } // Se foi cancelado, finalizado, etc.

    // Auditoria
    public bool Ativo { get; set; } = true;
    private DateTime _DataCriacao;
    public DateTime DataCriacao
    {
        get => _DataCriacao;
        set => _DataCriacao = DateTime.SpecifyKind(value, DateTimeKind.Utc);
    }
}

public enum StatusPlano
{
    Ativo = 1,
    Finalizado = 2,
    Cancelado = 3,
    Pausado = 4
}
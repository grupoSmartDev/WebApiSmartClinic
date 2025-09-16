using WebApiSmartClinic.Models.Abstractions;

namespace WebApiSmartClinic.Models;

public class PacientePlanoHistoricoModel : IEntidadeEmpresa, IEntidadeAuditavel
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
    public bool Ativo { get; set; }
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
}

public enum StatusPlano
{
    Ativo = 1,
    Finalizado = 2,
    Cancelado = 3,
    Pausado = 4
}
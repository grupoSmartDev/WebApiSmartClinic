using WebApiSmartClinic.Models;
using WebApiSmartClinic.Models.Abstractions;

public class SalaHorarioModel : IEntidadeEmpresa, IEntidadeAuditavel
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
    public int SalaId { get; set; }
    public DayOfWeek DiaSemana { get; set; }
    public TimeSpan HoraInicio { get; set; }
    public TimeSpan HoraFim { get; set; }
    //public bool Ativo { get; set; } = true;
    public SalaModel Sala { get; set; } = null!;
}

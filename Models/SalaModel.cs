using WebApiSmartClinic.Dto.SalaHorario;
using WebApiSmartClinic.Models.Abstractions;

namespace WebApiSmartClinic.Models;

public class SalaModel : IEntidadeEmpresa, IEntidadeAuditavel
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
    public string Nome { get; set; }
    public int Capacidade { get; set; }
    public string? Tipo { get; set; }
    public string? local { get; set; }
    public bool Status { get; set; } = true;
    public string? HorarioFincionamento { get; set; }
    public string? Observacao { get; set; }
    public bool IsSystemDefault { get; internal set; }
    public virtual ICollection<SalaHorarioModel> HorariosFuncionamento { get; set; } = new List<SalaHorarioModel>();
    public virtual ICollection<AgendaModel> Agendamentos { get; set; } = new List<AgendaModel>();
}

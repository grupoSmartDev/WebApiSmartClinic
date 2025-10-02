
using System.Text.Json.Serialization;
using WebApiSmartClinic.Models.Abstractions;

namespace WebApiSmartClinic.Models;

public class ExercicioModel : IEntidadeEmpresa, IEntidadeAuditavel
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
    public string Descricao { get; set; }
    public string? Obs { get; set; }
    public int? Peso { get; set; }
    public int? Tempo { get; set; }
    public int? Repeticoes { get; set; }
    public int? Series { get; set; }
    public int? EvolucaoId { get; set; } // Opcional, quando vinculado a uma evolução
    
    [JsonIgnore]
    public EvolucaoModel? Evolucao { get; set; }
}
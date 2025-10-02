using System.Text.Json.Serialization;
using WebApiSmartClinic.Models.Abstractions;
using WebApiSmartClinic.Services.EmpresaPermissao;

namespace WebApiSmartClinic.Models;

public class SubCentroCustoModel : IEntidadeEmpresa, IEntidadeAuditavel
{
    public int Id { get; set; }
    public string Nome { get; set; }
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
    // Relacionamento com o Centro de Custo Pai
    public int CentroCustoId { get; set; }

    [JsonIgnore]
    public CentroCustoModel CentroCusto { get; set; }
}



using System.Text.Json.Serialization;
using WebApiSmartClinic.Models.Abstractions;

namespace WebApiSmartClinic.Models;

public class CentroCustoModel : IEntidadeEmpresa, IEntidadeAuditavel
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
    public string Tipo { get; set; }
    public string Descricao { get; set; }

    //public List<SubCentroCustoModel> SubCentrosCusto { get; set; } = new List<SubCentroCustoModel>();

    public List<SubCentroCustoModel>? SubCentrosCusto { get; set; } = new List<SubCentroCustoModel>();
    public bool IsSystemDefault { get; internal set; }
}
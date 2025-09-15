namespace WebApiSmartClinic.Models.Abstractions;

/// <summary>
/// Propriedades padrão de auditoria.
/// </summary>
public interface IEntidadeAuditavel
{
    string? UsuarioCriacaoId { get; set; }
    DateTime DataCriacao { get; set; }
    string? UsuarioAlteracaoId { get; set; }
    DateTime? DataAlteracao { get; set; }
    bool Ativo { get; set; }
}
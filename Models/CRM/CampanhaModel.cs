using System.ComponentModel.DataAnnotations;
using WebApiSmartClinic.Models.Abstractions;

namespace WebApiSmartClinic.Models;

public class CampanhaModel : IEntidadeEmpresa, IEntidadeAuditavel
{
    public int Id { get; set; }
    public int EmpresaId { get; set; }

    [Required]
    [MaxLength(150)]
    public string Nome { get; set; } = string.Empty;

    [MaxLength(50)]
    public string? Plataforma { get; set; }  // Facebook, Instagram, Google, Outro

    [MaxLength(100)]
    public string? IdExterno { get; set; }   // ID da campanha no Meta Ads
    public bool Ativo { get; set; } = true;

    // Auditoria
    public string? UsuarioCriacaoId { get; set; }
    public DateTime DataCriacao { get; set; }
    public string? UsuarioAlteracaoId { get; set; }
    public DateTime? DataAlteracao { get; set; }
}

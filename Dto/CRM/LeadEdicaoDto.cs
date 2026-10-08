using System.ComponentModel.DataAnnotations;

namespace WebApiSmartClinic.Dto.CRM;

public sealed class LeadEdicaoDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O telefone é obrigatório.")]
    public string Telefone { get; set; } = string.Empty;

    public string? Email { get; set; }
    public string? CanalOrigem { get; set; }
    public int? CampanhaId { get; set; }
    public int EtapaFunilId { get; set; }
    public int? ProfissionalId { get; set; }
    public string? Observacao { get; set; }
    public string? MotivoPerdaId { get; set; }
}

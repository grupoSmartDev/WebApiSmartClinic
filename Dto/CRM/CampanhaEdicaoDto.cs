using System.ComponentModel.DataAnnotations;

namespace WebApiSmartClinic.Dto.CRM;

public sealed class CampanhaEdicaoDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome da campanha é obrigatório.")]
    public string Nome { get; set; } = string.Empty;
    public string? Plataforma { get; set; }
    public string? IdExterno { get; set; }
}

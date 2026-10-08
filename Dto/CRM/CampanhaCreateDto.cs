using System.ComponentModel.DataAnnotations;

namespace WebApiSmartClinic.Dto.CRM;

public sealed class CampanhaCreateDto
{
    [Required(ErrorMessage = "O nome da campanha é obrigatório.")]
    public string Nome { get; set; } = string.Empty;
    public string? Plataforma { get; set; }
    public string? IdExterno { get; set; }
}

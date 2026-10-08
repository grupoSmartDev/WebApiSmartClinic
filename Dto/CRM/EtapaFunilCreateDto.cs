using System.ComponentModel.DataAnnotations;

namespace WebApiSmartClinic.Dto.CRM;

public sealed class EtapaFunilCreateDto
{
    [Required(ErrorMessage = "O nome da etapa é obrigatório.")]
    public string Nome { get; set; } = string.Empty;
    public string? Cor { get; set; }
    public string? ScriptSugerido { get; set; }
    public int? Ordem { get; set; } // null/0 → última ordem + 1
}

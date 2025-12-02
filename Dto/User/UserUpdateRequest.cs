using System.ComponentModel.DataAnnotations;

namespace WebApiSmartClinic.Dto.User;

public class UserUpdateRequest
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public required string FirstName { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public required string LastName { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [EmailAddress(ErrorMessage = "O campo {0} está num formato inválido")]
    public required string Email { get; set; }

    [StringLength(255, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres"), MinLength(8)]
    public string? Password { get; set; }

    [StringLength(255, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres"), MinLength(8)]
    public string? NewPassword { get; set; }

    [Compare("NewPassword", ErrorMessage = "As senhas não conferem")]
    public string? ConfirmNewPassword { get; set; }

    /// <summary>
    /// Perfil/role selecionado no front (Admin, Support ou User). Opcional para não quebrar fluxos existentes.
    /// </summary>
    public string? Role { get; set; }
}

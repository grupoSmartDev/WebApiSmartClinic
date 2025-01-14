using System.ComponentModel.DataAnnotations;

namespace WebApiSmartClinic.Dto.User;

public class UserLoginRequest
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [EmailAddress(ErrorMessage = "O campo {0} está num formato inválido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres"), MinLength(8)]
    public string Password { get; set; }

    public bool RememberMe { get; set; }
}

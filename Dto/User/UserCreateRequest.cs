using System.ComponentModel.DataAnnotations;

namespace WebApiSmartClinic.Dto.User;

public class UserCreateRequest
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [EmailAddress(ErrorMessage = "O campo {0} está num formato inválido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres"), MinLength(8)]
    public string Password { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres"), MinLength(8)]
    [Compare("Password", ErrorMessage = "As senhas não conferem")]
    public string ConfirmPassword { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public bool AcceptTerms { get; set; }

    public bool UserMaster { get; set; } = false;
}

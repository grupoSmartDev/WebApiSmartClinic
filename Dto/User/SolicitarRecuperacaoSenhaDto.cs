using System.ComponentModel.DataAnnotations;

namespace WebApiSmartClinic.Dto.User;

public sealed class SolicitarRecuperacaoSenhaDto
{
    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
    [StringLength(256, ErrorMessage = "O e-mail deve ter no máximo 256 caracteres.")]
    public required string Email { get; init; }
}

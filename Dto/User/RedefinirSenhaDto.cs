using System.ComponentModel.DataAnnotations;

namespace WebApiSmartClinic.Dto.User;

public sealed class RedefinirSenhaDto : IValidatableObject
{
    [Required(ErrorMessage = "O identificador do usuário é obrigatório.")]
    [StringLength(450)]
    public required string UsuarioId { get; init; }

    [Required(ErrorMessage = "O token é obrigatório.")]
    [StringLength(4096)]
    public required string Token { get; init; }

    [Required(ErrorMessage = "A nova senha é obrigatória.")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "A nova senha deve ter entre 8 e 100 caracteres.")]
    [DataType(DataType.Password)]
    public required string NovaSenha { get; init; }

    [Required(ErrorMessage = "A confirmação da senha é obrigatória.")]
    [DataType(DataType.Password)]
    public required string ConfirmacaoSenha { get; init; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!string.Equals(NovaSenha, ConfirmacaoSenha, StringComparison.Ordinal))
        {
            yield return new ValidationResult(
                "A nova senha e a confirmação não conferem.",
                [nameof(ConfirmacaoSenha)]);
        }
    }
}

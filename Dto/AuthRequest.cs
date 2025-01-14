using System.ComponentModel.DataAnnotations;

namespace WebApiSmartClinic.Dto;

public class AuthRequest
{
    [EmailAddress]
    public string? Email { get; set; }

    [DataType(DataType.Password)]
    public string? Password { get; set; }

    public string? Token { get; set; }

    public bool RememberMe { get; set; }
}

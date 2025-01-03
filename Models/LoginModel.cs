namespace WebApiSmartClinic.Models;

public class LoginModel
{
    public string Email { get; set; }
    public string Senha { get; set; }
    public string Identificador { get; set; }
}

public class LoginResponse
{
    public string Token { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Identificador { get; set; }
}
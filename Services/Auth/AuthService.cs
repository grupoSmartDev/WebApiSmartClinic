using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Auth;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginModel request);
}

public class AuthService : IAuthService
{
    private readonly MasterDbContext _masterContext;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        MasterDbContext masterContext,
        IConfiguration configuration,
        ILogger<AuthService> logger)
    {
        _masterContext = masterContext;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<LoginResponse> LoginAsync(LoginModel request)
    {
        var tenant = await _masterContext.Empresas
            .FirstOrDefaultAsync(t => t.Identificador == request.Identificador && t.Ativo);

        if (tenant == null)
            throw new Exception("Empresa não encontrada");

        // TODO: Implemente sua lógica de autenticação aqui
        // Exemplo: verificar usuário no banco do tenant

        var token = GerarToken(request.Email, tenant.Identificador);

        return new LoginResponse
        {
            Token = token,
            Nome = "Nome do Usuário", // Substituir pelo nome real
            Email = request.Email,
            Identificador = tenant.Identificador
        };
    }

    private string GerarToken(string email, string identificadorEmpresa)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Email, email),
            new Claim("empresa", identificadorEmpresa)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
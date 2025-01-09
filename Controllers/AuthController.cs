using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiSmartClinic.Helpers;
using WebApiSmartClinic;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.ConnectionsService;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.User;
using SixLabors.ImageSharp;
[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly AppDbContext _context;
    private readonly AppSettings _appSettings;
    private const long TamanhoMaximoFotoPerfilEmBytes = 10L * 1024 * 1024; // 10 MB
    private readonly IConnectionsService _connectionsService;
    private readonly IConnectionStringProvider _connectionStringProvider;

    public AuthController(SignInManager<User> signInManager, UserManager<User> userManager, AppDbContext identityContext, IOptions<AppSettings> appSettings, IConnectionsService connectionsService, IConnectionStringProvider connectionStringProvider)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _context = identityContext;
        _appSettings = appSettings.Value;
        _connectionsService = connectionsService;
        _connectionStringProvider = connectionStringProvider;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequest model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));
        // Recupera o cabeçalho 'UserKey' da solicitação HTTP
        var userKey = HttpContext.Request.Headers["UserKey"].FirstOrDefault();
        model.RememberMe = true;
        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, true);

        if (result.Succeeded)
        {
            return Ok(await GenerateJwtAsync(model.Email, userKey!));
        }

        return BadRequest("E-mail, senha ou chave estão incorretos.");
    }


    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserCreateRequest model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

        // Recupera o cabeçalho 'UserKey' da solicitação HTTP
        var userKey = HttpContext.Request.Headers["UserKey"].FirstOrDefault();

        if (!model.AcceptTerms) return BadRequest("É necessário aceitar os termos");

        var user = new User
        {
            UserName = model.Email,
            Email = model.Email,
            EmailConfirmed = true /* futuramente adicionar função para validar o código enviado pelo e-mail*/,
            //extend props
            FirstName = model.FirstName,
            LastName = model.LastName,
            UserKey = userKey!
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        // Se o usuário foi criado com sucesso, adicioná-lo à role padrão "User"
        if (result.Succeeded)
        {
            result = await _userManager.AddToRoleAsync(user, "USER"); // Use o nome da role que você deseja atribuir
            if (!result.Succeeded)
            {
                // Se não foi possível adicionar a role, delete o usuário e retorne o erro
                await _userManager.DeleteAsync(user);
                return BadRequest(result.Errors);
            }

            // Claim pré-definida
            var claims = new List<Claim>
            {
                new Claim("Usuário", "Visualizar,Alterar"), // Exemplo de tipo de usuário
                new Claim("Documentos Usuário", "Enviar,Visualizar,Alterar,Excluir,Download") // Exemplo de nível de acesso
            };

            result = await _userManager.AddClaimsAsync(user, claims);
            if (!result.Succeeded)
            {
                // Se não foi possível adicionar a role, delete o usuário e retorne o erro
                await _userManager.DeleteAsync(user);
                return BadRequest(result.Errors);
            }

            await _signInManager.SignInAsync(user, false);
            return Ok(await GenerateJwtAsync(model.Email, userKey!));
        }

        return BadRequest(result.Errors);
    }

    [Authorize(Roles = "Admin,Support")]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 100, [FromQuery] string? filter = null)
    {
        var usersQuery = _userManager.Users
            .Where(u => filter == null || u.Email!.Contains(filter))
            .OrderBy(u => u.Email)
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

        var users = await usersQuery.ToListAsync();

        var userResponses = new List<UserResponseRequest>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? string.Empty; // Assume que cada usuário tem apenas uma role

            var userResponse = new UserResponseRequest
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
                Role = role,
                ProfilePictureBase64 = ConvertBlobToBase64(user.ProfilePicture),
                UserKey = user.UserKey // Supondo que UserKey é uma propriedade que você quer retornar
            };

            userResponses.Add(userResponse);
        }

        var totalUsers = await _userManager.Users.CountAsync(u => filter == null || u.Email!.Contains(filter));

        return Ok(new
        {
            data = userResponses,
            total = totalUsers
        });
    }


    [ClaimsAuthorize("Usuário", "Visualizar")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        // Extrair o identificador do usuário do ClaimsPrincipal (HttpContext.User)
        string tokenUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

        var roles = await _userManager.GetRolesAsync(user);
        var role = roles.FirstOrDefault() ?? string.Empty;

        // Verificar se o usuário atual é "Support" ou "Admin"
        bool isSupportOrAdmin = User.IsInRole("Support") || User.IsInRole("Admin");

        if (tokenUserId != id && !isSupportOrAdmin)
        {
            return StatusCode(StatusCodes.Status403Forbidden, "Você não tem permissão para consultar dados de outro usuário");
        }

        var userResponse = new UserResponseRequest
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
            Role = role,
            ProfilePictureBase64 = ConvertBlobToBase64(user.ProfilePicture),
        };

        return Ok(userResponse);
    }

    [ClaimsAuthorize("Usuário", "Alterar")]
    [HttpPut("{id}"), DisableRequestSizeLimit]
    public async Task<IActionResult> Update(string id, [FromForm] UserUpdateRequest model, IFormFile? profilePicture)
    {
        string tokenUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

        // Verificar se o usuário atual é "Support" ou "Admin"
        bool isSupportOrAdmin = User.IsInRole("Support") || User.IsInRole("Admin");

        if (tokenUserId != id && !isSupportOrAdmin)
        {
            return StatusCode(StatusCodes.Status403Forbidden, "Você não tem permissão para alterar outros usuários.");
        }

        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        user.Email = model.Email;
        user.UserName = model.Email;

        // Processamento e armazenamento da imagem de perfil
        if (profilePicture != null)
        {
            long sizeLimitBytes = Convert.ToInt64(_appSettings.UserProfileImageSizeMb!) * 1024L * 1024L;
            if (profilePicture.Length > sizeLimitBytes)
            {
                return BadRequest($"A imagem de perfil não pode ser maior que {_appSettings.UserProfileImageSizeMb}MB.");
            }

            string fileExtension = Path.GetExtension(profilePicture.FileName).ToLowerInvariant();
            if (fileExtension != ".png" && fileExtension != ".jpeg" && fileExtension != ".jpg" && fileExtension != ".bmp")
            {
                return BadRequest("Somente arquivos PNG, JPEG e BMP são permitidos.");
            }

            // Lê o arquivo enviado e converte para byte array
            byte[] profileImageBytes;
            using (var memoryStream = new MemoryStream())
            {
                await profilePicture.CopyToAsync(memoryStream);
                profileImageBytes = memoryStream.ToArray();
            }

            // Armazena a imagem no usuário
            user.ProfilePicture = profileImageBytes;
        }

        // Verifica e altera a senha, se necessário
        if (!string.IsNullOrWhiteSpace(model.Password))
        {
            var passwordCheck = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordCheck)
            {
                return BadRequest("Senha atual incorreta.");
            }

            if (!string.IsNullOrWhiteSpace(model.NewPassword))
            {
                if (!model.NewPassword.Equals(model.ConfirmNewPassword))
                {
                    return BadRequest("Nova senha e confirmação de senha não conferem.");
                }

                var passwordChangeResult = await _userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);
                if (!passwordChangeResult.Succeeded)
                {
                    return BadRequest(passwordChangeResult.Errors);
                }
            }
        }


        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded) return BadRequest(result.Errors);

        // Recuperar a role atual do usuário
        var userRoles = await _userManager.GetRolesAsync(user);
        var userRole = userRoles.FirstOrDefault() ?? string.Empty; // Supondo que cada usuário tenha apenas uma role

        // Construir o objeto UserResponseRequest para retorno
        var userResponse = new UserResponseRequest
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Role = userRole, // Incluindo a role no retorno
            ProfilePictureBase64 = ConvertBlobToBase64(user.ProfilePicture), // Assumindo que essa função converte byte[] para Base64
            UserKey = user.UserKey // Se UserKey é uma propriedade que você deseja retornar
        };

        return Ok(new { message = "Usuário atualizado com sucesso.", user = userResponse });
    }

    [Authorize(Roles = "Admin,Support")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

        // Primeiro, exclua os documentos do usuário do banco de dados
        //var documents = await _context.UserDocuments
        //                              .Where(doc => doc.UserId == id)
        //                              .ToListAsync();

        //_context.UserDocuments.RemoveRange(documents);
        await _context.SaveChangesAsync();

        // Agora, exclua os documentos do sistema de arquivos
        var documentDirectory = Path.Combine(_appSettings.UserDocumentsPath!, id);
        if (Directory.Exists(documentDirectory))
        {
            Directory.Delete(documentDirectory, true);
        }

        // Finalmente, exclua o usuário
        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded) return BadRequest(result.Errors);

        return Ok(new { message = "Usuário e documentos relacionados deletados com sucesso" });
    }

    private async Task<object> GenerateJwtAsync(string email, string userKey)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var roles = await _userManager.GetRolesAsync(user!);
        var claims = await _userManager.GetClaimsAsync(user!);

        var identityClaims = new ClaimsIdentity();
        identityClaims.AddClaims(claims);
        identityClaims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user!.Id));
        identityClaims.AddClaim(new Claim(ClaimTypes.Name, user!.UserName!));
        identityClaims.AddClaim(new Claim("UserKey", userKey));

        if (roles.Count > 0)
        {
            foreach (var role in roles)
            {
                identityClaims.AddClaim(new Claim(ClaimTypes.Role, role));
            }
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.ASCII.GetBytes(_appSettings.JwtSecretKey!);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = identityClaims,
            Expires = DateTime.UtcNow.AddHours(_appSettings.JwtExpiresHours),
            Issuer = _appSettings.JwtIssuer,
            Audience = _appSettings.JwtAudience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var encodedToken = tokenHandler.WriteToken(token);

        // Construir o objeto de retorno
        return new
        {
            success = true,
            data = new
            {
                key = user.UserKey,
                accessToken = encodedToken,
                expiresIn = _appSettings.JwtExpiresHours * 3600, // Tempo em segundos
                userToken = new
                {
                    id = user.Id,
                    email = user.Email,
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    profilePicture = ConvertBlobToBase64(user.ProfilePicture),
                    claims = claims.Select(c => new { value = c.Value, type = c.Type }).ToList(),
                    role = roles.FirstOrDefault() ?? string.Empty // Assume que cada usuário tem um role
                }
            }
        };
    }

    private string ConvertBlobToBase64(byte[]? blob)
    {
        if (blob == null || blob.Length == 0)
        {
            return string.Empty;
        }

        // Obter a extensão do arquivo
        string extension = GetImageExtension(blob);

        // Converter para base64 e adicionar a extensão do arquivo
        return $"data:image/{extension};base64,{Convert.ToBase64String(blob)}";
    }

    private string GetImageExtension(byte[] data)
    {
        if (data == null || data.Length == 0)
        {
            return string.Empty;
        }

        // Detecting the image format based on the header bytes
        IImageFormat imageFormat = Image.DetectFormat(data);

        // Determining the file extension based on the detected format
        string extension = imageFormat switch
        {
            JpegFormat _ => "jpg",
            PngFormat _ => "png",
            BmpFormat _ => "bmp",
            _ => string.Empty, // Unknown format
        };

        return extension;
    }
}

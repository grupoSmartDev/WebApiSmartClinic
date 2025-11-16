using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.User;
using WebApiSmartClinic.Helpers;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.Auth;
using WebApiSmartClinic.Services.ConnectionsService;


namespace WebApiSmartClinic.Services.Auth
{
    public class AuthService : IAuthInterface
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _context;
        private readonly AppSettings _appSettings;
        private readonly IConnectionsService _connectionsService;
        private readonly IConnectionStringProvider _connectionStringProvider;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly DataConnectionContext _contextDataConnection;
        private const long TamanhoMaximoFotoPerfilEmBytes = 10L * 1024 * 1024; // 10 MB

        public AuthService(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            AppDbContext identityContext,
            IOptions<AppSettings> appSettings,
            IConnectionsService connectionsService,
            IConnectionStringProvider connectionStringProvider, 
            DataConnectionContext contextDataConnection,
            IServiceScopeFactory scopeFactory)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = identityContext;
            _appSettings = appSettings.Value;
            _connectionsService = connectionsService;
            _connectionStringProvider = connectionStringProvider;
            _contextDataConnection = contextDataConnection;
            _scopeFactory = scopeFactory;
        }

        public async Task<object> LoginAsync(UserLoginRequest model, string? userKey)
        {
            // Validações
            var conn = await _contextDataConnection.DataConnection
                .Where(c => c.Key == userKey)
                .Select(c => c.StringConnection)
                .FirstOrDefaultAsync();

            if (string.IsNullOrWhiteSpace(conn))
                return new { success = false, errors = "Banco não encontrado" };

            // Define a conexão ANTES de chamar o SignInManager
            _connectionStringProvider.SetConnectionString(conn);
            
            
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            //await dbContext.Database.MigrateAsync();

            var migrationPendente = await dbContext.Database.GetPendingMigrationsAsync();
            
            if (migrationPendente.Any())
                await dbContext.Database.MigrateAsync();

            model.RememberMe = true;

            var result = await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                model.RememberMe,
                lockoutOnFailure: true);

            bool existe = await _contextDataConnection.DataConnection.AnyAsync(c => c.Key == userKey);
            if (!existe)
            {
                return new { success = false, errors = "Database não encontrado" };
            }

            if (!result.Succeeded)
            {
                // Retorne algo indicando falha (aqui, a título de exemplo):
                return new { success = false, error = "E-mail, senha ou chave estão incorretos." };
            }

            // Gera o JWT e retorna
            return await GenerateJwtAsync(model.Email, userKey!);
        }

        public async Task<object> RegisterAsync(UserCreateRequest model, string? userKey)
        {
            // Se preferir, valide aqui a aceitação dos termos, etc.
            if (!model.AcceptTerms)
                return new { success = false, error = "É necessário aceitar os termos." };

            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true, // futuramente validar e-mail
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserKey = userKey!
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            // 1. Verifica se já existe
            bool existe = await _contextDataConnection.DataConnection.AnyAsync(c => c.Key == userKey);
            if (!existe)
            {
                return new { success = false, errors = "Database não encontrado" };
            }

            // Se o usuário foi criado com sucesso, adiciona role, claims etc.
            if (!result.Succeeded)
            {
                return new { success = false, errors = result.Errors };
            }

            // Adiciona role
            result = await _userManager.AddToRoleAsync(user, "USER");
            if (!result.Succeeded)
            {
                // Se não foi possível adicionar a role, deleta o usuário
                await _userManager.DeleteAsync(user);
                return new { success = false, errors = result.Errors };
            }

            // Adiciona claims
            var claims = new List<Claim>
            {
                new Claim("Usuário", "Visualizar,Alterar"),
                new Claim("Documentos Usuário", "Enviar,Visualizar,Alterar,Excluir,Download")
            };
            result = await _userManager.AddClaimsAsync(user, claims);
            if (!result.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                return new { success = false, errors = result.Errors };
            }

            // Faz login automático
            await _signInManager.SignInAsync(user, isPersistent: false);
            return await GenerateJwtAsync(model.Email, userKey!);
        }

        public async Task<object> GetAllUsersAsync(int page, int pageSize, string? filter)
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
                var role = roles.FirstOrDefault() ?? string.Empty;

                var userResponse = new UserResponseRequest
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email!,
                    Role = role,
                    ProfilePictureBase64 = ConvertBlobToBase64(user.ProfilePicture),
                    UserKey = user.UserKey
                };
                userResponses.Add(userResponse);
            }

            var totalUsers = await _userManager.Users.CountAsync(u => filter == null || u.Email!.Contains(filter));

            return new
            {
                data = userResponses,
                total = totalUsers
            };
        }

        public async Task<object> GetUserByIdAsync(string id, ClaimsPrincipal currentUser)
        {
            // Extrair id do usuário logado, ver se tem permissão
            string tokenUserId = currentUser.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return new { success = false, error = "Usuário não encontrado." };

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? string.Empty;

            bool isSupportOrAdmin = currentUser.IsInRole("Support") || currentUser.IsInRole("Admin");

            // Se o usuário não é o dono do id e não é Support/Admin, não tem acesso
            if (tokenUserId != id && !isSupportOrAdmin)
            {
                return new { success = false, error = "Sem permissão para consultar dados de outro usuário." };
            }

            // Retorna objeto
            var userResponse = new UserResponseRequest
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
                Role = role,
                ProfilePictureBase64 = ConvertBlobToBase64(user.ProfilePicture),
            };

            return userResponse;
        }

        public async Task<object> Editar(string id, UserUpdateRequest model)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return new { success = false, error = "Usuário não encontrado." };

            // Atualiza dados
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.UserName = model.Email;

            // Se enviou a senha atual, checa e se for correto, altera
            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, model.Password);
                if (!passwordCheck)
                {
                    return new { success = false, error = "Senha atual incorreta." };
                }

                if (!string.IsNullOrWhiteSpace(model.NewPassword))
                {
                    if (!model.NewPassword.Equals(model.ConfirmNewPassword))
                    {
                        return new { success = false, error = "Nova senha e confirmação de senha não conferem." };
                    }

                    var passwordChangeResult = await _userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);
                    if (!passwordChangeResult.Succeeded)
                    {
                        return new { success = false, errors = passwordChangeResult.Errors };
                    }
                }
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return new { success = false, errors = result.Errors };

            // Recupera a role atual
            var userRoles = await _userManager.GetRolesAsync(user);
            var userRole = userRoles.FirstOrDefault() ?? string.Empty;

            // Monta objeto de retorno
            var userResponse = new UserResponseRequest
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = userRole,
                UserKey = user.UserKey
            };

            return new
            {
                success = true,
                message = "Usuário atualizado com sucesso.",
                user = userResponse
            };
        }


        public async Task<object> UpdateUserAsync(string id, UserUpdateRequest model, IFormFile? profilePicture, ClaimsPrincipal currentUser)
        {
            string tokenUserId = currentUser.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return new { success = false, error = "Usuário não encontrado." };

            bool isSupportOrAdmin = currentUser.IsInRole("Support") || currentUser.IsInRole("Admin");

            if (tokenUserId != id && !isSupportOrAdmin)
            {
                return new { success = false, error = "Você não tem permissão para alterar outros usuários." };
            }

            // Atualiza dados
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.UserName = model.Email;

            // Se recebeu imagem de perfil
            if (profilePicture != null)
            {
                long sizeLimitBytes = Convert.ToInt64(_appSettings.UserProfileImageSizeMb) * 1024L * 1024L;
                if (profilePicture.Length > sizeLimitBytes)
                {
                    return new { success = false, error = $"A imagem não pode ser maior que {_appSettings.UserProfileImageSizeMb}MB." };
                }

                string fileExtension = Path.GetExtension(profilePicture.FileName).ToLowerInvariant();
                if (fileExtension != ".png" && fileExtension != ".jpeg" && fileExtension != ".jpg" && fileExtension != ".bmp")
                {
                    return new { success = false, error = "Somente arquivos PNG, JPEG e BMP são permitidos." };
                }

                // Lê o arquivo enviado
                byte[] profileImageBytes;
                using (var memoryStream = new MemoryStream())
                {
                    await profilePicture.CopyToAsync(memoryStream);
                    profileImageBytes = memoryStream.ToArray();
                }
                user.ProfilePicture = profileImageBytes;
            }

            // Se enviou a senha atual, checa e se for correto, altera
            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, model.Password);
                if (!passwordCheck)
                {
                    return new { success = false, error = "Senha atual incorreta." };
                }

                if (!string.IsNullOrWhiteSpace(model.NewPassword))
                {
                    if (!model.NewPassword.Equals(model.ConfirmNewPassword))
                    {
                        return new { success = false, error = "Nova senha e confirmação de senha não conferem." };
                    }

                    var passwordChangeResult = await _userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);
                    if (!passwordChangeResult.Succeeded)
                    {
                        return new { success = false, errors = passwordChangeResult.Errors };
                    }
                }
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return new { success = false, errors = result.Errors };

            // Recupera a role atual
            var userRoles = await _userManager.GetRolesAsync(user);
            var userRole = userRoles.FirstOrDefault() ?? string.Empty;

            // Monta objeto de retorno
            var userResponse = new UserResponseRequest
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = userRole,
                ProfilePictureBase64 = ConvertBlobToBase64(user.ProfilePicture),
                UserKey = user.UserKey
            };

            return new
            {
                success = true,
                message = "Usuário atualizado com sucesso.",
                user = userResponse
            };
        }

        public async Task<object> DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return new { success = false, error = "Usuário não encontrado." };

            // Se você tiver documentos para remover, faça aqui
            // Exemplo: _context.UserDocuments.RemoveRange(...);

            await _context.SaveChangesAsync();

            // Excluir documentos do sistema de arquivos
            var documentDirectory = Path.Combine(_appSettings.UserDocumentsPath!, id);
            if (Directory.Exists(documentDirectory))
            {
                Directory.Delete(documentDirectory, true);
            }

            // Excluir usuário
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                return new { success = false, errors = result.Errors };

            return new { success = true, message = "Usuário e documentos deletados com sucesso" };
        }

        // ----------------------------------------------------
        // Métodos auxiliares privados (antes ficavam no controller)
        // ----------------------------------------------------
        private async Task<object> GenerateJwtAsync(string email, string userKey)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserKey", userKey)
            };

            // Adiciona roles
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JwtSecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _appSettings.JwtIssuer,
                audience: _appSettings.JwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(_appSettings.JwtExpiresHours),
                signingCredentials: creds
            );

            // Só para debug
            Console.WriteLine($"Token gerado com claims: {string.Join(", ", token.Claims.Select(c => $"{c.Type}: {c.Value}"))}");

            return new
            {
                success = true,
                data = new
                {
                    key = userKey,
                    accessToken = new JwtSecurityTokenHandler().WriteToken(token),
                    expiresIn = _appSettings.JwtExpiresHours * 3600,
                    userToken = new
                    {
                        id = user.Id,
                        email = user.Email,
                        claims = claims.Select(c => new { type = c.Type, value = c.Value }),
                        role = roles.FirstOrDefault() ?? string.Empty
                    },
                    plano = GetPlanoAtual(userKey)
                }
            };
        }

        private string GetPlanoAtual(string userKey)
        {
            return _context.Empresas
                .Where(x => x.TitularCPF == userKey)
                .Select(y => y.PlanoEscolhido)
                .FirstOrDefault() ?? string.Empty;
        }

        private string ConvertBlobToBase64(byte[]? blob)
        {
            if (blob == null || blob.Length == 0)
            {
                return string.Empty;
            }

            string extension = GetImageExtension(blob);
            return $"data:image/{extension};base64,{Convert.ToBase64String(blob)}";
        }

        private string GetImageExtension(byte[] data)
        {
            if (data == null || data.Length == 0)
            {
                return string.Empty;
            }

            IImageFormat imageFormat = Image.DetectFormat(data);
            return imageFormat switch
            {
                JpegFormat _ => "jpg",
                PngFormat _ => "png",
                BmpFormat _ => "bmp",
                _ => string.Empty,
            };
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
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
using System.Text;
using System.Text.Encodings.Web;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.User;
using WebApiSmartClinic.Helpers;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.ConnectionsService;
using WebApiSmartClinic.Services.MailService;


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
        private readonly IEmailService _servicoEmail;
        private const long TamanhoMaximoFotoPerfilEmBytes = 10L * 1024 * 1024; // 10 MB -- precisa analisar se está sendo utilizada essa constante, se nn tiver, remover ela.
        private const string MensagemSolicitacaoRecuperacao = "Se os dados informados estiverem corretos, você receberá um link para criar uma nova senha.";
        private const string MensagemLinkInvalido = "Este link de recuperação é inválido, já foi utilizado ou expirou.";

        public AuthService(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            AppDbContext identityContext,
            IOptions<AppSettings> appSettings,
            IConnectionsService connectionsService,
            IConnectionStringProvider connectionStringProvider,
            IServiceScopeFactory scopeFactory,
            IEmailService servicoEmail)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = identityContext;
            _appSettings = appSettings.Value;
            _connectionsService = connectionsService;
            _connectionStringProvider = connectionStringProvider;
            _scopeFactory = scopeFactory;
            _servicoEmail = servicoEmail;
        }

        public async Task<object> LoginAsync(UserLoginRequest model, string? userKey)
        {
            // Validações
            var conn = await ResolveConnectionStringAsync(userKey);

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
            var conn = await ResolveConnectionStringAsync(userKey);
            if (string.IsNullOrWhiteSpace(conn))
            {
                return new { success = false, errors = "Database nao encontrado" };
            }

            _connectionStringProvider.SetConnectionString(conn);

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

        public async Task<object> SolicitarRecuperacaoSenha(
            SolicitarRecuperacaoSenhaDto dados,
            string? chaveAcesso)
        {
            var conexao = await ResolveConnectionStringAsync(chaveAcesso);
            if (string.IsNullOrWhiteSpace(conexao))
            {
                return new { sucesso = true, mensagem = MensagemSolicitacaoRecuperacao };
            }

            _connectionStringProvider.SetConnectionString(conexao);

            var usuario = await _userManager.FindByEmailAsync(dados.Email.Trim());

            //deixei a resposta sempre igual, independente se tá certo ou errada, assim se tiver alguem tentando "hackear" ou acessar uma conta q não deveria,
            //a pessoa nn vai conseguir ir fazendo combinações diferentes até acertar o e-mail certo, chave de acesso certa, etc... 
            if (usuario is null || !usuario.EmailConfirmed)
            {
                return new { sucesso = true, mensagem = MensagemSolicitacaoRecuperacao };
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(usuario);
            var tokenCodificado = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var link = MontarLinkRecuperacaoSenha(usuario.Id, tokenCodificado, chaveAcesso!);

            try
            {
                await _servicoEmail.SendEmailAsync(new MailRequest
                {
                    ToEmail = usuario.Email!,
                    Subject = "Recuperação de senha - ClinicSmart",
                    Body = MontarEmailRecuperacaoSenha(usuario.FirstName, link)
                });
            }
            catch (Exception ex)
            {
                // a resposta nn muda pra nn revelar se a conta existe (mesma coisa que a explicação de cima).
                Console.WriteLine($"Falha ao enviar recuperação de senha: {ex.Message}");
            }

            return new { sucesso = true, mensagem = MensagemSolicitacaoRecuperacao };
        }

        public async Task<object> RedefinirSenha(RedefinirSenhaDto dados, string? chaveAcesso)
        {
            var conexao = await ResolveConnectionStringAsync(chaveAcesso);
            if (string.IsNullOrWhiteSpace(conexao))
            {
                return new { sucesso = false, erro = MensagemLinkInvalido };
            }

            _connectionStringProvider.SetConnectionString(conexao);

            var usuario = await _userManager.FindByIdAsync(dados.UsuarioId);
            if (usuario is null || !string.Equals(usuario.UserKey, chaveAcesso, StringComparison.Ordinal))
            {
                return new { sucesso = false, erro = MensagemLinkInvalido };
            }

            string token;
            try
            {
                token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(dados.Token));
            }
            catch (FormatException)
            {
                return new { sucesso = false, erro = MensagemLinkInvalido };
            }

            var resultado = await _userManager.ResetPasswordAsync(usuario, token, dados.NovaSenha);
            if (!resultado.Succeeded)
            {
                if (resultado.Errors.Any(erro => erro.Code == "InvalidToken"))
                {
                    return new { sucesso = false, erro = MensagemLinkInvalido };
                }

                return new
                {
                    sucesso = false,
                    erro = "A nova senha não atende aos requisitos de segurança.",
                    erros = resultado.Errors.Select(erro => erro.Description).ToArray()
                };
            }

            try
            {
                await _servicoEmail.SendEmailAsync(new MailRequest
                {
                    ToEmail = usuario.Email!,
                    Subject = "Senha alterada - ClinicSmart",
                    Body = MontarEmailSenhaAlterada(usuario.FirstName)
                });
            }
            catch (Exception ex)
            {
                //aqui a senha já foi alterada, essa msg de falha no aviso nn desfaz alteração.
                Console.WriteLine($"Falha ao enviar confirmação de senha alterada: {ex.Message}");
            }

            return new
            {
                sucesso = true,
                mensagem = "Senha redefinida com sucesso. Você já pode entrar com a nova senha."
            };
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
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return new { success = false, error = "Usuário não encontrado." };

            if (!PermissionHelper.CanManageUser(id, currentUser))
            {
                return new { success = false, error = "Sem permissão para consultar dados de outro usuário." };
            }

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
            };

            return userResponse;
        }

        public async Task<object> Editar(string id, UserUpdateRequest model, ClaimsPrincipal currentUser)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return new { success = false, error = "Usuário não encontrado." };

            if (!PermissionHelper.CanManageUser(id, currentUser))
            {
                return new { success = false, error = "Você não tem permissão para alterar outros usuários." };
            }

            return await AtualizarUsuarioAsync(user, model, currentUser);
        }


        public async Task<object> UpdateUserAsync(string id, UserUpdateRequest model, ClaimsPrincipal currentUser)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return new { success = false, error = "Usuário não encontrado." };

            if (!PermissionHelper.CanManageUser(id, currentUser))
            {
                return new { success = false, error = "Você não tem permissão para alterar outros usuários." };
            }

            return await AtualizarUsuarioAsync(user, model, currentUser);
        }

        private async Task<object> AtualizarUsuarioAsync(User user, UserUpdateRequest model, ClaimsPrincipal currentUser)
        {
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.UserName = model.Email;

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

            var userRoles = await _userManager.GetRolesAsync(user);
            var currentRole = userRoles.FirstOrDefault() ?? string.Empty;

            if (!string.IsNullOrWhiteSpace(model.Role) && !string.Equals(model.Role, currentRole, StringComparison.OrdinalIgnoreCase))
            {
                if (!PermissionHelper.IsSupportOrAdmin(currentUser))
                {
                    return new { success = false, error = "Você não tem permissão para alterar a permissão deste usuário." };
                }

                if (userRoles.Any())
                {
                    await _userManager.RemoveFromRolesAsync(user, userRoles);
                }

                var roleResult = await _userManager.AddToRoleAsync(user, model.Role);
                if (!roleResult.Succeeded)
                {
                    return new { success = false, errors = roleResult.Errors };
                }

                currentRole = model.Role;
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return new { success = false, errors = result.Errors };

            var userResponse = new UserResponseRequest
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = currentRole,
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
        // Métodos auxiliares privados
        // ----------------------------------------------------

        private string MontarLinkRecuperacaoSenha(
            string usuarioId,
            string tokenCodificado,
            string chaveAcesso)
        {
            var urlFrontend = _appSettings.UrlFrontendRecuperacaoSenha.TrimEnd('/');
            var dadosLink = string.Join('&',
                $"usuario={Uri.EscapeDataString(usuarioId)}",
                $"token={Uri.EscapeDataString(tokenCodificado)}",
                $"chave={Uri.EscapeDataString(chaveAcesso)}");

            return $"{urlFrontend}/redefinir-senha#{dadosLink}";
        }

        private string MontarEmailRecuperacaoSenha(string? primeiroNome, string link)
        {
            var nomeSeguro = HtmlEncoder.Default.Encode(
                string.IsNullOrWhiteSpace(primeiroNome) ? "Olá" : $"Olá, {primeiroNome.Trim()}");
            var linkSeguro = HtmlEncoder.Default.Encode(link);
            var minutosValidade = _appSettings.MinutosValidadeTokenRecuperacaoSenha;

            return $"""
                <!doctype html>
                <html lang="pt-BR">
                <body style="font-family:Arial,sans-serif;color:#263238;line-height:1.5">
                  <div style="max-width:560px;margin:0 auto;padding:24px">
                    <h2 style="color:#005946">Recuperação de senha</h2>
                    <p>{nomeSeguro}.</p>
                    <p>Recebemos uma solicitação para criar uma nova senha de acesso à ClinicSmart.</p>
                    <p style="margin:28px 0">
                      <a href="{linkSeguro}" style="background:#005946;color:#fff;padding:12px 20px;border-radius:24px;text-decoration:none;display:inline-block">
                        Criar nova senha
                      </a>
                    </p>
                    <p>Este link expira em {minutosValidade} minutos e só pode ser utilizado uma vez.</p>
                    <p>Se você não solicitou a recuperação, ignore este e-mail. Sua senha continuará a mesma.</p>
                  </div>
                </body>
                </html>
                """;
        }

        private static string MontarEmailSenhaAlterada(string? primeiroNome)
        {
            var nomeSeguro = HtmlEncoder.Default.Encode(
                string.IsNullOrWhiteSpace(primeiroNome) ? "Olá" : $"Olá, {primeiroNome.Trim()}");

            return $"""
                <!doctype html>
                <html lang="pt-BR">
                <body style="font-family:Arial,sans-serif;color:#263238;line-height:1.5">
                  <div style="max-width:560px;margin:0 auto;padding:24px">
                    <h2 style="color:#005946">Senha alterada</h2>
                    <p>{nomeSeguro}.</p>
                    <p>Sua senha de acesso à ClinicSmart foi redefinida com sucesso.</p>
                    <p>Se você não realizou esta alteração, entre em contato com o suporte imediatamente.</p>
                  </div>
                </body>
                </html>
                """;
        }

        private async Task<string?> ResolveConnectionStringAsync(string? userKey)
        {
            if (string.IsNullOrWhiteSpace(userKey))
            {
                return null;
            }

            try
            {
                return await _connectionsService.GetConnectionsStringByKeyAsync(userKey);
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        private async Task<string> ObterPlanoUsuarioAsync(string userId)
        {
            try
            {
                // Busca a empresa do usuário através do UsuarioEmpresa
                var usuarioEmpresa = await _context.UsuarioEmpresas
                    .Include(ue => ue.Empresa)
                    .FirstOrDefaultAsync(ue => ue.UsuarioId == userId && ue.EmpresaPadrao);

                if (usuarioEmpresa?.Empresa != null)
                {
                    return usuarioEmpresa.Empresa.PlanoEscolhido ?? "Basic";
                }

                return "Basic"; // Default se não encontrar
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter plano: {ex.Message}");
                return "Basic";
            }
        }

        private async Task<object> GenerateJwtAsync(string email, string userKey)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var roles = await _userManager.GetRolesAsync(user);

            // Busca o plano do usuário
            var plano = await ObterPlanoUsuarioAsync(user.Id);
            Console.WriteLine($"🔍 Plano detectado para {email}: {plano}");

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserKey", userKey),
                new Claim("Plano", plano)
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

            Console.WriteLine($"Token gerado com claims: {string.Join(", ", token.Claims.Select(c => $"{c.Type}: {c.Value}"))}");

            return new
            {
                success = true,
                data = new
                {
                    key = userKey,
                    accessToken = new JwtSecurityTokenHandler().WriteToken(token),
                    expiresIn = _appSettings.JwtExpiresHours * 3600,
                    plano = plano,
                    userToken = new
                    {
                        id = user.Id,
                        email = user.Email,
                        claims = claims.Select(c => new { type = c.Type, value = c.Value }).ToList(),
                        role = roles.FirstOrDefault() ?? string.Empty
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

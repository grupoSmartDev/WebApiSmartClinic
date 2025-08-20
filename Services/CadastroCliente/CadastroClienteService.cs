
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Npgsql;
using System.Security.Claims;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.CadastroCliente;
using WebApiSmartClinic.Dto.Profissional;
using WebApiSmartClinic.Dto.User;
using WebApiSmartClinic.Helpers;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.Auth;
using WebApiSmartClinic.Services.ConnectionsService;
using WebApiSmartClinic.Services.MailService;

namespace WebApiSmartClinic.Services.CadastroCliente;

public class CadastroClienteService : ICadastroClienteInterface
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly AppDbContext _context;
    private readonly DataConnectionContext _contextDataConnection;
    private readonly AppSettings _appSettings;
    private const long TamanhoMaximoFotoPerfilEmBytes = 10L * 1024 * 1024; // 10 MB
    private readonly IConnectionsService _connectionsService;
    private readonly IConnectionStringProvider _connectionStringProvider;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IServiceProvider _serviceProvider;
    private readonly EmailService _mailService;

    public CadastroClienteService(
        SignInManager<User> signInManager,
        UserManager<User> userManager, 
        AppDbContext identityContext, 
        IOptions<AppSettings> appSettings, 
        IConnectionsService connectionsService, 
        IConnectionStringProvider connectionStringProvider, 
        DataConnectionContext contextDataConnection, 
        IServiceScopeFactory scopeFactory,
        IServiceProvider serviceProvider, IEmailService emailService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _context = identityContext;
        _contextDataConnection = contextDataConnection;
        _appSettings = appSettings.Value;
        _connectionsService = connectionsService;
        _connectionStringProvider = connectionStringProvider;
        _scopeFactory = scopeFactory;
        _serviceProvider = serviceProvider;
        _mailService = (EmailService?)emailService;
    }

    public async Task CriarUsuario(UserCreateRequest userCreateRequest, string? userKey = null)
    {
        var authService = _serviceProvider.GetRequiredService<AuthService>();
        await authService.RegisterAsync(userCreateRequest, userKey);
    }

    //public async Task<ResponseModel<EmpresaModel>> Criar(CadastroClienteCreateDto dto)
    //{
    //    var resposta = new ResponseModel<EmpresaModel>();

    //    var cpfKey = dto.TitularCPF;
    //    var novoBanco = cpfKey; // cuidado: sanitize nome do DB
    //    var novaStringConexao = $"Host=62.72.51.219;Port=5432;Database={novoBanco};Username=postgres;Password=Elefante01!;Include Error Detail=true;";
    //    var masterConnection = $"Host=62.72.51.219;Port=5432;Database=connections;Username=postgres;Password=Elefante01!;Include Error Detail=true;";

    //    try
    //    {
    //        // 1) Verifica duplicidade na base de conexões
    //        bool existe = await _contextDataConnection.DataConnection.AnyAsync(c => c.Key == cpfKey);
    //        if (existe)
    //        {
    //            resposta.Status = false;
    //            resposta.Mensagem = "Já existe um cliente com esse CPF.";

    //            return resposta;
    //        }

    //        // 2) Cria o database do tenant
    //        await using (var conn = new NpgsqlConnection(masterConnection))
    //        {
    //            await conn.OpenAsync();
    //            using var cmd = conn.CreateCommand();
    //            cmd.CommandText = $"CREATE DATABASE \"{novoBanco}\"";
    //            await cmd.ExecuteNonQueryAsync();
    //        }

    //        // 3) Persiste a connection na base de conexões
    //        var novaConexao = new DataConnections { Key = cpfKey, StringConnection = novaStringConexao };
    //        await _contextDataConnection.DataConnection.AddAsync(novaConexao);
    //        await _contextDataConnection.SaveChangesAsync();

    //        // 4) Fixa a connection do tenant no provider (deve ser SCOPED/AsyncLocal)
    //        _connectionStringProvider.SetConnectionString(novaStringConexao);

    //        // 5) ÚNICO scope para tudo do tenant
    //        await using var scope = _scopeFactory.CreateAsyncScope();

    //        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();          // mesmo AppDbContext para tudo
    //        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    //        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    //        var authService = scope.ServiceProvider.GetRequiredService<AuthService>();           // se você centralizou RegisterAsync nele

    //        // 6) Migrações do tenant
    //        await db.Database.MigrateAsync();

    //        // 7) Transação única do tenant (opcional, mas recomendado)
    //        await using var tx = await db.Database.BeginTransactionAsync();

    //        // 8) Garante roles
    //        foreach (var role in new[] { "Admin", "User" })
    //        {
    //            if (!await roleManager.RoleExistsAsync(role))
    //            {
    //                var rc = await roleManager.CreateAsync(new IdentityRole(role));
    //                if (!rc.Succeeded)
    //                {
    //                    var err = string.Join("; ", rc.Errors.Select(e => e.Description));

    //                    resposta.Status = false;
    //                    resposta.Mensagem = $"Falha ao criar role {role}: {err}";

    //                    return resposta;
    //                }
    //            }
    //        }

    //        // 9) Cria empresa
    //        var empresa = new EmpresaModel
    //        {
    //            Nome = dto.Nome,
    //            Sobrenome = dto.Sobrenome,
    //            Email = dto.Email,
    //            Celular = dto.Celular,
    //            TitularCPF = dto.TitularCPF,
    //            CNPJEmpresaMatriz = dto.CNPJEmpresaMatriz,
    //            Especialidade = dto.Especialidade,
    //            PlanoEscolhido = dto.PlanoEscolhido,
    //            TipoPagamentoId = (int)(dto.TipoPagamentoId == null || dto.TipoPagamentoId == 0 ? 1 : dto.TipoPagamentoId),
    //            QtdeLicencaEmpresaPermitida = 1,
    //            QtdeLicencaUsuarioPermitida = 3,
    //            QtdeLicencaEmpresaUtilizada = 0,
    //            QtdeLicencaUsuarioUtilizada = 0,
    //            DataInicio = DateTime.UtcNow,
    //            PeriodoTeste = true,
    //            DataInicioTeste = DateTime.UtcNow,
    //            DataFim = DateTime.UtcNow.AddDays(7),
    //            DatabaseConnectionString = novaStringConexao,
    //            Ativo = true
    //        };

    //        await db.Empresas.AddAsync(empresa);
    //        await db.SaveChangesAsync();

    //        // 10) Cria Admin do tenant
    //        var adminReq = new UserCreateRequest
    //        {
    //            FirstName = "Admin",
    //            LastName = "Teste",
    //            Email = "gruposmartdesenvolvimentos@gmail.com",
    //            Password = "Admin@123",
    //            ConfirmPassword = "Admin@123",
    //            AcceptTerms = true,
    //        };

    //        try
    //        {
    //            await authService.RegisterAsync(adminReq, cpfKey); // internamente usa userManager e AddToRole("Admin") no MESMO db
    //        }
    //        catch (Exception e)
    //        {
    //            resposta.Status = false;
    //            resposta.Mensagem = e.Message;

    //            return resposta;
    //            throw;
    //        }

    //        // 11) Cria usuário do cliente
    //        var userReq = new UserCreateRequest
    //        {
    //            FirstName = dto.Nome,
    //            LastName = dto.Sobrenome,
    //            Email = dto.Email,
    //            Password = "Admin@123",
    //            ConfirmPassword = "Admin@123",
    //            AcceptTerms = true,
    //        };

    //        try
    //        {
    //            await authService.RegisterAsync(userReq, cpfKey); 
    //        }
    //        catch (Exception e)
    //        {
    //            resposta.Status = false;
    //            resposta.Mensagem = e.Message;

    //            return resposta;
    //            throw;
    //        }

    //        // 12) Cria Profissional usando o MESMO db
    //        var prof = new ProfissionalModel
    //        {
    //            Email = dto.Email,
    //            Nome = $"{dto.Nome} {dto.Sobrenome}",
    //            Cpf = dto.TitularCPF,
    //            Celular = dto.Celular
    //        };

    //        await db.Profissional.AddAsync(prof);
    //        await db.SaveChangesAsync();

    //        await tx.CommitAsync();

    //        // 13) E-mail
    //        //await _mailService.SendEmailAsync(new MailRequest
    //        //{
    //        //    ToEmail = userReq.Email,
    //        //    Subject = "Conta criada com sucesso!",
    //        //    Body = GetHtmlContent(userReq.FirstName)
    //        //});

    //        resposta.Status = true;
    //        resposta.Dados = empresa;
    //        resposta.Mensagem = "Cliente e banco criados com sucesso.";

    //        if (resposta.Status && !existe)
    //        {
    //            try
    //            {
    //                MailRequest mailRequest = new MailRequest();
    //                mailRequest.ToEmail = userReq.Email;
    //                mailRequest.Subject = "Conta criada com sucesso!";
    //                mailRequest.Body = GetHtmlContent(userReq.FirstName);


    //                await _mailService.SendEmailAsync(mailRequest);
    //            }
    //            catch (Exception)
    //            {
    //            }
    //        }

    //        return resposta;
    //    }
    //    catch (Exception ex)
    //    {
    //        resposta.Status = false;
    //        resposta.Mensagem = $"Erro: {ex.Message}";

    //        return resposta;
    //    }
    //}


    public async Task<ResponseModel<EmpresaModel>> Criar(CadastroClienteCreateDto dto)
    {
        var resposta = new ResponseModel<EmpresaModel>();

        var cpfKey = dto.TitularCPF;
        var novoBanco = cpfKey;
        var novaStringConexao = $"Host=62.72.51.219;Port=5432;Database={novoBanco};Username=postgres;Password=Elefante01!;";
        //var novaStringConexao = $"Host=localhost;Port=5432;Database={novoBanco};Username=postgres;Password=Elefante01!;";
        var masterConnection = $"Host=62.72.51.219;Port=5432;Database=connections;Username=postgres;Password=Elefante01!;";
        //var masterConnection = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=Elefante01!;";

        try
        {
            // 1. Verifica se já existe
            bool existe = await _contextDataConnection.DataConnection.AnyAsync(c => c.Key == cpfKey);
            if (existe)
            {
                resposta.Status = false;
                resposta.Mensagem = "Já existe um cliente com esse CPF.";
                return resposta;
            }

            // 2. Cria o banco de dados
            await using (NpgsqlConnection conn = new NpgsqlConnection(masterConnection))
            {
                conn.Open();
                using var cmd = conn.CreateCommand();
                cmd.CommandText = $"CREATE DATABASE \"{novoBanco}\"";
                await cmd.ExecuteNonQueryAsync();
            }

            // 3. Salva a string no banco de conexões
            var novaConexao = new DataConnections { Key = cpfKey, StringConnection = novaStringConexao };
            await _contextDataConnection.DataConnection.AddAsync(novaConexao);
            await _contextDataConnection.SaveChangesAsync();

            // 4. Define string no provider
            _connectionStringProvider.SetConnectionString(novaStringConexao);

            // só agora cria o service scope
            using var scope = _scopeFactory.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // agora o dbContext deve ler a nova connection string
            await dbContext.Database.MigrateAsync();

            // 7. Cria cliente
            var cliente = new EmpresaModel
            {
                Nome = dto.Nome,
                Sobrenome = dto.Sobrenome,
                Email = dto.Email,
                Celular = dto.Celular,
                TitularCPF = dto.TitularCPF,
                CNPJEmpresaMatriz = dto.CNPJEmpresaMatriz,
                Especialidade = dto.Especialidade,
                PlanoEscolhido = dto.PlanoEscolhido,
                TipoPagamentoId = (int)(dto.TipoPagamentoId == null || dto.TipoPagamentoId == 0 ? 1 : dto.TipoPagamentoId),
                QtdeLicencaEmpresaPermitida = 1,
                QtdeLicencaUsuarioPermitida = 3,
                QtdeLicencaEmpresaUtilizada = 0,
                QtdeLicencaUsuarioUtilizada = 0,
                DataInicio = DateTime.UtcNow,
                PeriodoTeste = true,
                DataInicioTeste = DateTime.UtcNow,
                DataFim = DateTime.UtcNow.AddDays(7),
                DatabaseConnectionString = novaStringConexao,
                Ativo = true
            };

            await dbContext.Empresas.AddAsync(cliente);

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                resposta.Status = false;
                resposta.Mensagem = e.Message;

                return resposta;
            }

            var userCreateRequest = new UserCreateRequest
            {
                FirstName = dto.Nome,
                LastName = dto.Sobrenome,
                Email = dto.Email,
                Password = "Admin@123",
                ConfirmPassword = "Admin@123",
                AcceptTerms = true,
            };

            if (!existe)
            {
                var userCreatAdmin = new UserCreateRequest
                {
                    FirstName = "Admin",
                    LastName = "Teste",
                    Email = "gruposmartdesenvolvimentos@gmail.com",
                    Password = "Admin@123",
                    ConfirmPassword = "Admin@123",
                    AcceptTerms = true,
                };
                using var authService2 = _scopeFactory.CreateAsyncScope();
                var cu2 = authService2.ServiceProvider.GetRequiredService<AuthService>();

                await cu2.RegisterAsync(userCreatAdmin, cpfKey);

                var profissionalPadrao = new ProfissionalModel();
                {
                    profissionalPadrao.Email = dto.Email;
                    profissionalPadrao.Nome = $"{dto.Nome} {dto.Sobrenome}";
                    profissionalPadrao.Cpf = dto.TitularCPF;
                    profissionalPadrao.Celular = dto.Celular;

                    await _context.Profissional.AddAsync(profissionalPadrao);
                    await _context.SaveChangesAsync();
                }
            }

            await using (var authScope = _scopeFactory.CreateAsyncScope())
            {
                var cu = authScope.ServiceProvider.GetRequiredService<AuthService>();
                await cu.RegisterAsync(userCreateRequest, cpfKey);
            }
            //using var authService = _scopeFactory.CreateAsyncScope();
            //var cu = authService.ServiceProvider.GetRequiredService<AuthService>();

            //await cu.RegisterAsync(userCreateRequest, cpfKey);

            resposta.Status = true;
            resposta.Dados = cliente;
            resposta.Mensagem = "Cliente e banco criados com sucesso.";

            if (resposta.Status && !existe)
            {
                try
                {
                    MailRequest mailRequest = new MailRequest();
                    mailRequest.ToEmail = userCreateRequest.Email;
                    mailRequest.Subject = "Conta criada com sucesso!";
                    mailRequest.Body = GetHtmlContent(userCreateRequest.FirstName);


                    await _mailService.SendEmailAsync(mailRequest);
                }
                catch (Exception) 
                {
                }
            }

        }
        catch (Exception ex)
        {
            resposta.Mensagem = $"Erro: {ex.Message}";
        }

        return resposta;
    }

    public async Task<ResponseModel<EmpresaModel>> BuscarPorId(int idCadastroCliente)
    {
        ResponseModel<EmpresaModel> resposta = new ResponseModel<EmpresaModel>();
        try
        {
            var cadastrocliente = await _context.Empresas.FirstOrDefaultAsync(x => x.Id == idCadastroCliente);
            if (cadastrocliente == null)
            {
                resposta.Mensagem = "Nenhum CadastroCliente encontrado";
                return resposta;
            }

            resposta.Dados = cadastrocliente;
            resposta.Mensagem = "CadastroCliente Encontrado";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = "Erro ao buscar CadastroCliente";
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<EmpresaModel>>> Delete(int idCadastroCliente)
    {
        ResponseModel<List<EmpresaModel>> resposta = new ResponseModel<List<EmpresaModel>>();

        try
        {
            var cadastrocliente = await _context.Empresas.FirstOrDefaultAsync(x => x.Id == idCadastroCliente);
            if (cadastrocliente == null)
            {
                resposta.Mensagem = "Nenhum CadastroCliente encontrado";
                return resposta;
            }

            _context.Remove(cadastrocliente);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Empresas.ToListAsync();
            resposta.Mensagem = "CadastroCliente Excluido com sucesso";
            return resposta;

        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<EmpresaModel>>> Editar(CadastroClienteEdicaoDto cadastroclienteEdicaoDto)
    {
        ResponseModel<List<EmpresaModel>> resposta = new ResponseModel<List<EmpresaModel>>();

        try
        {
            var cadastrocliente = _context.Empresas.FirstOrDefault(x => x.Id == cadastroclienteEdicaoDto.Id);
            if (cadastrocliente == null)
            {
                resposta.Mensagem = "CadastroCliente não encontrado";
                return resposta;
            }

            // Atualizar para o código de acordo com o necessário
            //cadastrocliente.CadastroCliente = cadastroclienteEdicaoDto.CadastroCliente;

            _context.Update(cadastrocliente);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Empresas.ToListAsync();
            resposta.Mensagem = "CadastroCliente Atualizado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {

            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<EmpresaModel>>> Listar()
    {
        ResponseModel<List<EmpresaModel>> resposta = new ResponseModel<List<EmpresaModel>>();

        try
        {
            var cadastrocliente = await _context.Empresas.ToListAsync();

            resposta.Dados = cadastrocliente;
            resposta.Mensagem = "Todos os CadastroCliente foram encontrados";
            return resposta;


        }
        catch (Exception e)
        {
            resposta.Mensagem = e.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    private string GetHtmlContent(string nome)
    {
        string mensagem = $@"Olá {nome}. Seja bem vindo ao Clinc Smart!

        Obrigado, sua senha padrão é Admin@123 e sua chave de acesso é o CPF informado para cadastro. 

        Após o primeiro Login, recomendamos que você altere sua senha. 

        Qualquer dúvida, entre em contato com o suporte.

        Estamos felizes em ter você na Família Smart.";

        string htmlContent = $@"
                <!DOCTYPE html>
                <html lang=""pt-BR"">
                <head>
                    <meta charset=""UTF-8"">
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            line-height: 1.6;
                            color: #333;
                            max-width: 600px;
                            margin: 0 auto;
                            padding: 20px;
                        }}
                        .welcome-message {{
                            background-color: #f4f4f4;
                            border-left: 4px solid #007bff;
                            padding: 15px;
                        }}
                    </style>
                </head>
                <body>
                    <div class=""welcome-message"">
                        <p>{mensagem.Replace("\n", "<br>")}</p>
                    </div>
                </body>
                </html>";

        return htmlContent;
    }
    }
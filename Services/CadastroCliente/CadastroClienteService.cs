
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using WebApiSmartClinic.Dto.CadastroCliente;
using WebApiSmartClinic.Dto.Profissional;
using WebApiSmartClinic.Dto.User;
using WebApiSmartClinic.Helpers;
using WebApiSmartClinic.Models.Asaas;
using WebApiSmartClinic.Services.Asaas;
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
    private readonly IAsaasService _asaasService;

    public CadastroClienteService(
        SignInManager<User> signInManager,
        UserManager<User> userManager,
        AppDbContext identityContext,
        IOptions<AppSettings> appSettings,
        IConnectionsService connectionsService,
        IConnectionStringProvider connectionStringProvider,
        DataConnectionContext contextDataConnection,
        IServiceScopeFactory scopeFactory,
        IServiceProvider serviceProvider, IEmailService emailService, IAsaasService asaasService)
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
        _asaasService = asaasService;
    }

    public async Task CriarUsuario(UserCreateRequest userCreateRequest, string? userKey = null)
    {
        var authService = _serviceProvider.GetRequiredService<AuthService>();
        await authService.RegisterAsync(userCreateRequest, userKey);
    }

    public async Task<ResponseModel<EmpresaModel>> Criar(CadastroClienteCreateDto dto)
    {
        var resposta = new ResponseModel<EmpresaModel>();

        var cpfKey = dto.TitularCPF;
        var novoBanco = SanitizarNomeBanco(cpfKey);
        var templateDb = "ClinicSmart";
        var novoOwner = "postgres";
        var novaStringConexao = $"Host=62.72.51.219;Port=5432;Database={novoBanco};Username=postgres;Password=Elefante01!;";
        var masterConnection = $"Host=62.72.51.219;Port=5432;Database=connections;Username=postgres;Password=Elefante01!;";

        try
        {
            bool existe = await _contextDataConnection.DataConnection.AnyAsync(c => c.Key == cpfKey);
            if (existe)
            {
                resposta.Status = false;
                resposta.Mensagem = "Já existe um cliente com esse CPF.";
                return resposta;
            }

            try
            {
                await CriarBancoPorTemplateAsync(masterConnection, novoBanco, templateDb, novoOwner);
            }
            catch (Exception exTpl)
            {
                resposta.Status = false;
                resposta.Mensagem = $"Falha ao clonar banco pelo template: {exTpl.Message}";
                return resposta;
            }

            var novaConexao = new DataConnections { Key = cpfKey, StringConnection = novaStringConexao };
            await _contextDataConnection.DataConnection.AddAsync(novaConexao);
            await _contextDataConnection.SaveChangesAsync();

            _connectionStringProvider.SetConnectionString(novaStringConexao);

            await using var scope = _scopeFactory.CreateAsyncScope();
            var sp = scope.ServiceProvider;
            var db = sp.GetRequiredService<AppDbContext>();
            var userMgr = sp.GetRequiredService<UserManager<User>>();
            var roleMgr = sp.GetRequiredService<RoleManager<IdentityRole>>();
            var authSvc = sp.GetRequiredService<AuthService>();

            string? asaasCustomerId = null;
            string? asaasSubscriptionId = null;

            if (!dto.PeriodoTeste && dto.PrecoSelecionado > 0)
            {
                try
                {
                    var customerRequest = new AsaasCustomerRequest
                    {
                        name = $"{dto.Nome} {dto.Sobrenome}",
                        email = dto.Email,
                        phone = LimparTelefone(dto.Celular),
                        mobilePhone = LimparTelefone(dto.Celular),
                        cpfCnpj = dto.TitularCPF,
                        observations = $"ClinicSmart - {dto.PlanoEscolhido} - {dto.PeriodoCobranca}"
                    };
                    var customer = await _asaasService.CreateCustomerAsync(customerRequest);
                    asaasCustomerId = customer.id;

                    var subscriptionRequest = new AsaasSubscriptionRequest
                    {
                        customer = customer.id,
                        billingType = MapearTipoPagamento(dto.TipoPagamentoId ?? 1),
                        value = dto.PrecoSelecionado,
                        nextDueDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"),
                        cycle = dto.PeriodoCobranca == "monthly" ? "MONTHLY" : "SEMIANNUALLY",
                        description = $"ClinicSmart - {dto.PlanoEscolhido}",
                        externalReference = $"clinicsmart_cpf_{cpfKey}"
                    };
                    var subscription = await _asaasService.CreateSubscriptionAsync(subscriptionRequest);
                    asaasSubscriptionId = subscription.id;
                }
                catch (Exception) { }
            }

            try
            {
                var empresa = new EmpresaModel
                {
                    Nome = dto.Nome,
                    Sobrenome = dto.Sobrenome,
                    Email = dto.Email,
                    Celular = dto.Celular,
                    TitularCPF = dto.TitularCPF,
                    CNPJEmpresaMatriz = dto.CNPJEmpresaMatriz,
                    Especialidade = dto.Especialidade,
                    PlanoEscolhido = PlanoEscolhidoLp(dto.PrecoSelecionado),
                    TipoPagamentoId = (int)(dto.TipoPagamentoId == null || dto.TipoPagamentoId == 0 ? 1 : dto.TipoPagamentoId),
                    QtdeLicencaEmpresaPermitida = ObterLicencasPermitidas(PlanoEscolhidoLp(dto.PrecoSelecionado)),
                    QtdeLicencaUsuarioPermitida = ObterLicencasPermitidas(PlanoEscolhidoLp(dto.PrecoSelecionado)),
                    QtdeLicencaEmpresaUtilizada = 0,
                    QtdeLicencaUsuarioUtilizada = 0,
                    DataInicio = DateTime.UtcNow,
                    PeriodoTeste = true,
                    DataInicioTeste = DateTime.UtcNow,
                    DataFim = DateTime.UtcNow.AddDays(7),
                    DatabaseConnectionString = novaStringConexao,
                    Ativo = true,
                    AsaasCustomerId = asaasCustomerId,
                    AsaasSubscriptionId = asaasSubscriptionId,
                    PeriodoCobranca = dto.PeriodoCobranca,
                    PrecoSelecionado = dto.PrecoSelecionado,
                    DataNascimentoTitular = dto.DataNascimentoTitular,
                    TelefoneFixo = dto.TelefoneFixo,
                    CelularComWhatsApp = dto.CelularComWhatsApp,
                    ReceberNotificacoes = dto.ReceberNotificacoes
                };

                await db.Empresas.AddAsync(empresa);
                await db.SaveChangesAsync(); 

                async Task GarantirPerfisAsync()
                {
                    foreach (var p in new[] { Perfis.Admin, Perfis.Support, Perfis.User })
                        if (!await roleMgr.RoleExistsAsync(p))
                            await roleMgr.CreateAsync(new IdentityRole(p));
                }
                await GarantirPerfisAsync();

                var userAdminReq = new UserCreateRequest
                {
                    FirstName = "Admin",
                    LastName = "Teste",
                    Email = "gruposmartdesenvolvimentos@gmail.com",
                    Password = "Admin@123",
                    ConfirmPassword = "Admin@123",
                    AcceptTerms = true,
                };
                await authSvc.RegisterAsync(userAdminReq, cpfKey);

                var userClienteReq = new UserCreateRequest
                {
                    FirstName = dto.Nome,
                    LastName = dto.Sobrenome,
                    Email = dto.Email,
                    Password = "Admin@123",
                    ConfirmPassword = "Admin@123",
                    AcceptTerms = true,
                };
                await authSvc.RegisterAsync(userClienteReq, cpfKey);

                var userAdmin = await userMgr.FindByEmailAsync(userAdminReq.Email);
                var userCliente = await userMgr.FindByEmailAsync(userClienteReq.Email);

                if (userAdmin != null && !(await userMgr.IsInRoleAsync(userAdmin, Perfis.Admin)))
                    await userMgr.AddToRoleAsync(userAdmin, Perfis.Admin);

                if (userCliente != null && !(await userMgr.IsInRoleAsync(userCliente, Perfis.Admin)))
                    await userMgr.AddToRoleAsync(userCliente, Perfis.Admin);

                if (userAdmin != null)
                    db.UsuarioEmpresas.Add(new UsuarioEmpresaModel
                    {
                        UsuarioId = userAdmin.Id,
                        EmpresaId = empresa.Id,
                        PodeLer = true,
                        PodeEscrever = true,
                        PodeExcluir = true,
                        EmpresaPadrao = true
                    });

                if (userCliente != null)
                    db.UsuarioEmpresas.Add(new UsuarioEmpresaModel
                    {
                        UsuarioId = userCliente.Id,
                        EmpresaId = empresa.Id,
                        PodeLer = true,
                        PodeEscrever = true,
                        PodeExcluir = true,
                        EmpresaPadrao = true
                    });

                await db.SaveChangesAsync();

                db.VerTodasEmpresas = true;
                db.EmpresaSelecionada = empresa.Id;
                db.UsuarioAtualId = userAdmin?.Id ?? "system";

                var profissionalPadrao = new ProfissionalModel
                {
                    Email = dto.Email,
                    Nome = $"{dto.Nome} {dto.Sobrenome}",
                    Cpf = dto.TitularCPF,
                    Celular = dto.Celular,
                    EmpresaId = empresa.Id,
                    UsuarioId = userCliente.Id
                };

                await db.Profissional.AddAsync(profissionalPadrao);
                await db.SaveChangesAsync();

                resposta.Status = true;
                resposta.Dados = empresa;
                resposta.Mensagem = "Cliente e banco criados com sucesso.";
            }
            catch (Exception exCriacao)
            {
                try
                {
                    await DroparBancoSeFalharAsync(masterConnection, novoBanco);
                }
                catch { }

                await RemoverDataConnectionSeFalharAsync(cpfKey);

                resposta.Status = false;
                resposta.Mensagem = $"Falha ao criar estrutura do cliente: {exCriacao.Message}";
                return resposta;
            }

            try
            {
                await _mailService.SendEmailAsync(new MailRequest
                {
                    ToEmail = dto.Email,
                    Subject = "Conta criada com sucesso!",
                    Body = GetHtmlContent(dto.Nome)
                });
            }
            catch { }
        }
        catch (Exception ex)
        {
            resposta.Status = false;
            resposta.Mensagem = $"Erro: {ex.Message}";
        }

        return resposta;
    }

    private static string SanitizarNomeBanco(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome) || !Regex.IsMatch(nome, "^[A-Za-z0-9_]+$"))
            throw new InvalidOperationException("Nome de banco inválido. Use apenas letras, números e underscore.");
        return nome;
    }

    private static async Task CriarBancoPorTemplateAsync(string masterConnection, string novoBanco, string templateDb, string owner = "postgres")
    {
        await using var conn = new NpgsqlConnection(masterConnection);
        await conn.OpenAsync();

        await using (var kill = conn.CreateCommand())
        {
            kill.CommandText = @"SELECT pg_terminate_backend(pid)
                             FROM pg_stat_activity
                             WHERE datname = @tpl AND pid <> pg_backend_pid();";
            kill.Parameters.AddWithValue("@tpl", templateDb);
            await kill.ExecuteNonQueryAsync();
        }

        await using (var cmd = conn.CreateCommand())
        {
            cmd.CommandText = $@"CREATE DATABASE ""{novoBanco}"" WITH TEMPLATE ""{templateDb}"" OWNER ""{owner}"";";
            await cmd.ExecuteNonQueryAsync();
        }
    }

    private static async Task DroparBancoSeFalharAsync(string masterConnection, string nomeBanco)
    {
        await using var conn = new NpgsqlConnection(masterConnection);
        await conn.OpenAsync();

        await using (var kill = conn.CreateCommand())
        {
            kill.CommandText = @"SELECT pg_terminate_backend(pid)
                             FROM pg_stat_activity
                             WHERE datname = @db AND pid <> pg_backend_pid();";
            kill.Parameters.AddWithValue("@db", nomeBanco);
            await kill.ExecuteNonQueryAsync();
        }

        await using (var drop = conn.CreateCommand())
        {
            drop.CommandText = $@"DROP DATABASE IF EXISTS ""{nomeBanco}"";";
            await drop.ExecuteNonQueryAsync();
        }
    }

    private async Task RemoverDataConnectionSeFalharAsync(string key)
    {
        var map = await _contextDataConnection.DataConnection.FirstOrDefaultAsync(c => c.Key == key);
        if (map != null)
        {
            _contextDataConnection.DataConnection.Remove(map);
            await _contextDataConnection.SaveChangesAsync();
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

    private string MapearTipoPagamento(int tipoPagamentoId)
    {
        return tipoPagamentoId switch
        {
            1 => "CREDIT_CARD",
            2 => "BOLETO",
            3 => "PIX",
            _ => "BOLETO"
        };
    }

    private string LimparTelefone(string telefone)
    {
        return telefone?.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "");
    }

    private int ObterLicencasPermitidas(string plano)
    {
        return plano?.ToLower() switch
        {
            "basic" => 1,
            "plus" => 5,
            "premium" => 15,
            _ => 1
        };
    }

    public string PlanoEscolhidoLp(decimal precoSelecionado)
    {
        return precoSelecionado switch
        {
            89.0M => "Basic",
            149.0M => "Basic",
            189.0M => "Plus",
            249.0M => "Plus",
            269.0M => "Premium",
            329.0M => "Premium",
            _ => "Basic"
        };
    }
}
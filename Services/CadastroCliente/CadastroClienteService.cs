
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

        // 0) Chaves e conexões
        var cpfKey = dto.TitularCPF;                  // chave do tenant (UserKey)
        var novoBanco = SanitizarNomeBanco(cpfKey);   // nome do DB: use apenas [a-zA-Z0-9_]
        var templateDb = "ClinicSmart";      // <<< configure aqui ou via appsettings
        var novoOwner = "postgres";                  // <<< se quiser definir o OWNER

        // conexões (troque para configs)
        var novaStringConexao = $"Host=62.72.51.219;Port=5432;Database={novoBanco};Username=postgres;Password=Elefante01!;";
        //var novaStringConexao = $"Host=localhost;Port=5432;Database={novoBanco};Username=postgres;Password=Elefante01!;";

        // Base "master" onde você pode executar CREATE DATABASE e matar conexões do template
        var masterConnection = $"Host=62.72.51.219;Port=5432;Database=connections;Username=postgres;Password=Elefante01!;";
        //var masterConnection = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=Elefante01!;";

        try
        {
            // 1) Evita duplicidade da key do tenant
            bool existe = await _contextDataConnection.DataConnection.AnyAsync(c => c.Key == cpfKey);
            if (existe)
            {
                resposta.Status = false;
                resposta.Mensagem = "Já existe um cliente com esse CPF.";
                return resposta;
            }

            // 2) Cria o DB clonando do TEMPLATE (rápido!)
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

            // 3) Registra a connection string na base 'connections'
            var novaConexao = new DataConnections { Key = cpfKey, StringConnection = novaStringConexao };
            await _contextDataConnection.DataConnection.AddAsync(novaConexao);
            await _contextDataConnection.SaveChangesAsync();

            // 4) Define a connection do tenant atual para o restante da operação
            _connectionStringProvider.SetConnectionString(novaStringConexao);

            // 5) Único escopo para trabalhar no DB recém criado (NÃO precisamos de MigrateAsync)
            await using var scope = _scopeFactory.CreateAsyncScope();
            var sp = scope.ServiceProvider;
            var db = sp.GetRequiredService<AppDbContext>();
            var userMgr = sp.GetRequiredService<UserManager<User>>();
            var roleMgr = sp.GetRequiredService<RoleManager<IdentityRole>>();
            var authSvc = sp.GetRequiredService<AuthService>();

            // 6) (NÃO precisa mais) — db.Database.MigrateAsync();

            // =================== NOVA LÓGICA ASAAS (mantida) ===================
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
                catch (Exception) { /* segue sem travar cadastro */ }
            }

            try
            {
                // 7.1) Cria a Empresa (matriz)
                var empresa = new EmpresaModel
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
                await db.SaveChangesAsync(); // garante empresa.Id

                // 7.2) Garante papéis
                async Task GarantirPerfisAsync()
                {
                    foreach (var p in new[] { Perfis.Admin, Perfis.Support, Perfis.User })
                        if (!await roleMgr.RoleExistsAsync(p))
                            await roleMgr.CreateAsync(new IdentityRole(p));
                }
                await GarantirPerfisAsync();

                // 7.3) Cria usuários padrão
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

                // 7.4) Vincula usuários à Empresa (EmpresaPadrao = true)
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

                // 7.5) MODO CONTEXTO: setar flags para criar registros multi-empresa fora do middleware
                db.VerTodasEmpresas = true;                 // ignora exigência de EmpresaSelecionada
                db.EmpresaSelecionada = empresa.Id;         // opcional, por consistência
                db.UsuarioAtualId = userAdmin?.Id ?? "system";

                // 7.6) Cria Profissional padrão
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
                // se falhar depois de criar o banco/clonar template, limpar DB e mapping
                try
                {
                    await DroparBancoSeFalharAsync(masterConnection, novoBanco);
                }
                catch { /* best effort */ }

                await RemoverDataConnectionSeFalharAsync(cpfKey);

                resposta.Status = false;
                resposta.Mensagem = $"Falha ao criar estrutura do cliente: {exCriacao.Message}";
                return resposta;
            }

            // 8) E-mail de boas-vindas (best-effort)
            try
            {
                await _mailService.SendEmailAsync(new MailRequest
                {
                    ToEmail = dto.Email,
                    Subject = "Conta criada com sucesso!",
                    Body = GetHtmlContent(dto.Nome)
                });
            }
            catch { /* não interrompe o fluxo */ }
        }
        catch (Exception ex)
        {
            resposta.Status = false;
            resposta.Mensagem = $"Erro: {ex.Message}";
        }

        return resposta;
    }

    // ---------------------- Helpers (TEMPLATE/cleanup) ----------------------

    private static string SanitizarNomeBanco(string nome)
    {
        // permite somente letras, números e underscore (evita injeção em identificadores)
        if (string.IsNullOrWhiteSpace(nome) || !Regex.IsMatch(nome, "^[A-Za-z0-9_]+$"))
            throw new InvalidOperationException("Nome de banco inválido. Use apenas letras, números e underscore.");
        return nome;
    }

    private static async Task CriarBancoPorTemplateAsync(string masterConnection, string novoBanco, string templateDb, string owner = "postgres")
    {
        await using var conn = new NpgsqlConnection(masterConnection);
        await conn.OpenAsync();

        // garante que o TEMPLATE não tem conexões abertas
        await using (var kill = conn.CreateCommand())
        {
            kill.CommandText = @"SELECT pg_terminate_backend(pid)
                             FROM pg_stat_activity
                             WHERE datname = @tpl AND pid <> pg_backend_pid();";
            kill.Parameters.AddWithValue("@tpl", templateDb);
            await kill.ExecuteNonQueryAsync();
        }

        // cria o banco novo a partir do TEMPLATE
        await using (var cmd = conn.CreateCommand())
        {
            // IMPORTANTE: nomes já estão sanitizados (sem aspas perigosas)
            cmd.CommandText = $@"CREATE DATABASE ""{novoBanco}"" WITH TEMPLATE ""{templateDb}"" OWNER ""{owner}"";";
            await cmd.ExecuteNonQueryAsync();
        }
    }

    private static async Task DroparBancoSeFalharAsync(string masterConnection, string nomeBanco)
    {
        await using var conn = new NpgsqlConnection(masterConnection);
        await conn.OpenAsync();

        // derruba conexões no banco alvo
        await using (var kill = conn.CreateCommand())
        {
            kill.CommandText = @"SELECT pg_terminate_backend(pid)
                             FROM pg_stat_activity
                             WHERE datname = @db AND pid <> pg_backend_pid();";
            kill.Parameters.AddWithValue("@db", nomeBanco);
            await kill.ExecuteNonQueryAsync();
        }

        // drop database
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


    // esse é o ultimo comentado
    //public async Task<ResponseModel<EmpresaModel>> Criar(CadastroClienteCreateDto dto)
    //{
    //    var resposta = new ResponseModel<EmpresaModel>();

    //    // 0) Chaves/strings de conexão
    //    var cpfKey = dto.TitularCPF;             // chave do tenant (UserKey)
    //    var novoBanco = cpfKey;                   // nome do banco por CPF
    //    var novaStringConexao = $"Host=62.72.51.219;Port=5432;Database={novoBanco};Username=postgres;Password=Elefante01!;";
    //    //var novaStringConexao = $"Host=localhost;Port=5432;Database={novoBanco};Username=postgres;Password=Elefante01!;";

    //    var masterConnection = $"Host=62.72.51.219;Port=5432;Database=connections;Username=postgres;Password=Elefante01!;";
    //    //var masterConnection = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=Elefante01!;";

    //    try
    //    {
    //        // 1) Verifica se já existe registro para o CPF na base de conexões
    //        bool existe = await _contextDataConnection.DataConnection.AnyAsync(c => c.Key == cpfKey);
    //        existe = false;
    //        if (existe)
    //        {
    //            resposta.Status = false;
    //            resposta.Mensagem = "Já existe um cliente com esse CPF.";
    //            return resposta;
    //        }

    //        //if (false)
    //        //{
    //            await using (var conn = new Npgsql.NpgsqlConnection(masterConnection))
    //            {
    //                await conn.OpenAsync();
    //                using var cmd = conn.CreateCommand();
    //                cmd.CommandText = $"CREATE DATABASE \"{novoBanco}\"";
    //                await cmd.ExecuteNonQueryAsync();
    //            }

    //            // 3) Registra a connection string na base 'connections'
    //            var novaConexao = new DataConnections { Key = cpfKey, StringConnection = novaStringConexao };
    //            await _contextDataConnection.DataConnection.AddAsync(novaConexao);
    //            await _contextDataConnection.SaveChangesAsync();
    //        //}
    //            // 2) Cria o banco físico do tenant


    //        // 4) Seta a connection do tenant atual
    //        _connectionStringProvider.SetConnectionString(novaStringConexao);

    //        // 5) ÚNICO scope para todas as operações no banco recém criado
    //        await using var scope = _scopeFactory.CreateAsyncScope();
    //        var sp = scope.ServiceProvider;
    //        var dbContext = sp.GetRequiredService<AppDbContext>();
    //        var userMgr = sp.GetRequiredService<UserManager<User>>();
    //        var roleMgr = sp.GetRequiredService<RoleManager<IdentityRole>>();
    //        var authSvc = sp.GetRequiredService<AuthService>();

    //        // 6) Aplica migrations no banco novo
    //        //if (false)
    //            await dbContext.Database.MigrateAsync();


    //        // =================== NOVA LÓGICA ASAAS ===================
    //        string asaasCustomerId = null;
    //        string asaasSubscriptionId = null;

    //        // 5. SE NÃO FOR TRIAL, CRIAR NO ASAAS
    //        if (!dto.PeriodoTeste && dto.PrecoSelecionado > 0)
    //        {
    //            try
    //            {
    //                // Criar customer no Asaas
    //                var customerRequest = new AsaasCustomerRequest
    //                {
    //                    name = $"{dto.Nome} {dto.Sobrenome}",
    //                    email = dto.Email,
    //                    phone = LimparTelefone(dto.Celular),
    //                    mobilePhone = LimparTelefone(dto.Celular),
    //                    cpfCnpj = dto.TitularCPF,
    //                    observations = $"ClinicSmart - {dto.PlanoEscolhido} - {dto.PeriodoCobranca}"
    //                };

    //                var customer = await _asaasService.CreateCustomerAsync(customerRequest);
    //                asaasCustomerId = customer.id;

    //                // Criar subscription no Asaas
    //                var subscriptionRequest = new AsaasSubscriptionRequest
    //                {
    //                    customer = customer.id,
    //                    billingType = MapearTipoPagamento(dto.TipoPagamentoId ?? 1),
    //                    value = dto.PrecoSelecionado,
    //                    nextDueDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"),
    //                    cycle = dto.PeriodoCobranca == "monthly" ? "MONTHLY" : "SEMIANNUALLY",
    //                    description = $"ClinicSmart - {dto.PlanoEscolhido}",
    //                    externalReference = $"clinicsmart_cpf_{cpfKey}"
    //                };

    //                var subscription = await _asaasService.CreateSubscriptionAsync(subscriptionRequest);
    //                asaasSubscriptionId = subscription.id;

    //                Console.WriteLine($"🎉 Asaas integrado com sucesso! Customer: {asaasCustomerId}, Subscription: {asaasSubscriptionId}");
    //            }
    //            catch (Exception asaasEx)
    //            {
    //                Console.WriteLine($"❌ Erro na integração Asaas: {asaasEx.Message}");
    //                // Continua o cadastro mesmo se o Asaas falhar
    //            }
    //        }


    //        //await using var trx = await dbContext.Database.BeginTransactionAsync();
    //        try
    //        {
    //            // 7.1) Cria a empresa (matriz)
    //            //var empresa = new EmpresaModel
    //            //{
    //            //    Nome = dto.Nome,
    //            //    Sobrenome = dto.Sobrenome,
    //            //    Email = dto.Email,
    //            //    Celular = dto.Celular,
    //            //    TitularCPF = dto.TitularCPF,
    //            //    CNPJEmpresaMatriz = dto.CNPJEmpresaMatriz,
    //            //    Especialidade = dto.Especialidade,
    //            //    PlanoEscolhido = dto.PlanoEscolhido,
    //            //    TipoPagamentoId = (int)(dto.TipoPagamentoId == null || dto.TipoPagamentoId == 0 ? 1 : dto.TipoPagamentoId),
    //            //    QtdeLicencaEmpresaPermitida = 1,
    //            //    QtdeLicencaUsuarioPermitida = 3,
    //            //    QtdeLicencaEmpresaUtilizada = 0,
    //            //    QtdeLicencaUsuarioUtilizada = 0,
    //            //    DataInicio = DateTime.UtcNow,
    //            //    PeriodoTeste = true,
    //            //    DataInicioTeste = DateTime.UtcNow,
    //            //    DataFim = DateTime.UtcNow.AddDays(7),
    //            //    DatabaseConnectionString = novaStringConexao,
    //            //    Ativo = true
    //            //};

    //            //await dbContext.Empresas.AddAsync(empresa);
    //            //await dbContext.SaveChangesAsync(); // garante empresa.Id

    //            // 7.1) Cria a empresa (matriz)
    //            var cliente = new EmpresaModel
    //            {
    //                Nome = dto.Nome,
    //                Sobrenome = dto.Sobrenome,
    //                Email = dto.Email,
    //                Celular = dto.Celular,
    //                TitularCPF = dto.TitularCPF,
    //                CNPJEmpresaMatriz = dto.CNPJEmpresaMatriz,
    //                Especialidade = dto.Especialidade,
    //                PlanoEscolhido = dto.PlanoEscolhido,
    //                TipoPagamentoId = (int)(dto.TipoPagamentoId == null || dto.TipoPagamentoId == 0 ? 1 : dto.TipoPagamentoId),
    //                QtdeLicencaEmpresaPermitida = 1,
    //                QtdeLicencaUsuarioPermitida = 3,
    //                QtdeLicencaEmpresaUtilizada = 0,
    //                QtdeLicencaUsuarioUtilizada = 0,
    //                DataInicio = DateTime.UtcNow,
    //                PeriodoTeste = true,
    //                DataInicioTeste = DateTime.UtcNow,
    //                DataFim = DateTime.UtcNow.AddDays(7),
    //                DatabaseConnectionString = novaStringConexao,
    //                Ativo = true,
    //                // NOVOS CAMPOS ASAAS
    //                AsaasCustomerId = asaasCustomerId,
    //                AsaasSubscriptionId = asaasSubscriptionId,
    //                PeriodoCobranca = dto.PeriodoCobranca,
    //                PrecoSelecionado = dto.PrecoSelecionado,
    //                DataNascimentoTitular = dto.DataNascimentoTitular,
    //                TelefoneFixo = dto.TelefoneFixo,
    //                CelularComWhatsApp = dto.CelularComWhatsApp,
    //                ReceberNotificacoes = dto.ReceberNotificacoes
    //            };

    //            await dbContext.Empresas.AddAsync(cliente);
    //            await dbContext.SaveChangesAsync();

    //            // 7.2) (Opcional) garantir papéis padrão — se já faz HasData, pode pular
    //            async Task GarantirPerfisPadraoAsync(RoleManager<IdentityRole> rm)
    //            {
    //                string[] perfis = { Perfis.Admin, Perfis.Support, Perfis.User };
    //                foreach (var p in perfis)
    //                    if (!await rm.RoleExistsAsync(p))
    //                        await rm.CreateAsync(new IdentityRole(p));
    //            }
    //            await GarantirPerfisPadraoAsync(roleMgr);

    //            // 7.3) Cria usuários padrão (admin técnico + titular) usando AuthService
    //            var userAdminReq = new UserCreateRequest
    //            {
    //                FirstName = "Admin",
    //                LastName = "Teste",
    //                Email = "gruposmartdesenvolvimentos@gmail.com",
    //                Password = "Admin@123",
    //                ConfirmPassword = "Admin@123",
    //                AcceptTerms = true,
    //            };
    //            await authSvc.RegisterAsync(userAdminReq, cpfKey);

    //            var userClienteReq = new UserCreateRequest
    //            {
    //                FirstName = dto.Nome,
    //                LastName = dto.Sobrenome,
    //                Email = dto.Email,
    //                Password = "Admin@123",
    //                ConfirmPassword = "Admin@123",
    //                AcceptTerms = true,
    //            };
    //            await authSvc.RegisterAsync(userClienteReq, cpfKey);

    //            // 7.4) Busca usuários para obter os Ids e atribuir papéis
    //            var userAdmin = await userMgr.FindByEmailAsync(userAdminReq.Email);
    //            var userCliente = await userMgr.FindByEmailAsync(userClienteReq.Email);

    //            if (userAdmin != null && !(await userMgr.IsInRoleAsync(userAdmin, Perfis.Admin)))
    //                await userMgr.AddToRoleAsync(userAdmin, Perfis.Admin);

    //            if (userCliente != null && !(await userMgr.IsInRoleAsync(userCliente, Perfis.Admin)))
    //                await userMgr.AddToRoleAsync(userCliente, Perfis.Admin); // titular inicia como Admin

    //            // 7.5) Vínculo Usuario x Empresa com EmpresaPadrao = true (ESSENCIAL)
    //            if (userAdmin != null)
    //            {
    //                dbContext.UsuarioEmpresas.Add(new UsuarioEmpresaModel
    //                {
    //                    UsuarioId = userAdmin.Id,
    //                    EmpresaId = cliente.Id,
    //                    PodeLer = true,
    //                    PodeEscrever = true,
    //                    PodeExcluir = true,
    //                    EmpresaPadrao = true
    //                });
    //            }

    //            if (userCliente != null)
    //            {
    //                dbContext.UsuarioEmpresas.Add(new UsuarioEmpresaModel
    //                {
    //                    UsuarioId = userCliente.Id,
    //                    EmpresaId = cliente.Id,
    //                    PodeLer = true,
    //                    PodeEscrever = true,
    //                    PodeExcluir = true,
    //                    EmpresaPadrao = true
    //                });
    //            }

    //            await dbContext.SaveChangesAsync();

    //            // 7.6) Cria Profissional padrão no mesmo dbContext
    //            var profissionalPadrao = new ProfissionalModel
    //            {
    //                Email = dto.Email,
    //                Nome = $"{dto.Nome} {dto.Sobrenome}",
    //                Cpf = dto.TitularCPF,
    //                Celular = dto.Celular,
    //                EmpresaId = cliente.Id
    //            };

    //            await dbContext.Profissional.AddAsync(profissionalPadrao);
    //            await dbContext.SaveChangesAsync();

    //            //await trx.CommitAsync();

    //            resposta.Status = true;
    //            resposta.Dados = cliente;
    //            resposta.Mensagem = "Cliente e banco criados com sucesso.";
    //        }
    //        catch (Exception exTrx)
    //        {
    //            //await trx.RollbackAsync();
    //            resposta.Status = false;
    //            resposta.Mensagem = $"Falha ao criar estrutura do cliente: {exTrx.Message}";
    //            return resposta;
    //        }

    //        // 8) E-mail de boas-vindas (fora da transação)
    //        try
    //        {
    //            MailRequest mailRequest = new MailRequest
    //            {
    //                ToEmail = dto.Email,
    //                Subject = "Conta criada com sucesso!",
    //                Body = GetHtmlContent(dto.Nome)
    //            };
    //            await _mailService.SendEmailAsync(mailRequest);
    //        }
    //        catch { /* não falha se e-mail der erro */ }
    //    }
    //    catch (Exception ex)
    //    {
    //        resposta.Status = false;
    //        resposta.Mensagem = $"Erro: {ex.Message}";
    //    }

    //    return resposta;
    //}

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

    // NOVOS MÉTODOS AUXILIARES
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

    private string LimparCPF(string cpf)
    {
        return cpf?.Replace(".", "").Replace("-", "");
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
}
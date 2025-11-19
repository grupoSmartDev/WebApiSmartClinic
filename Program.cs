using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Text;
using System.Text.Json.Serialization;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Helpers;
using WebApiSmartClinic.MappingProfiles;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services;
using WebApiSmartClinic.Services.Agenda;
using WebApiSmartClinic.Services.Asaas;
using WebApiSmartClinic.Services.Atividade;
using WebApiSmartClinic.Services.Auth;
using WebApiSmartClinic.Services.Banco;
using WebApiSmartClinic.Services.Boleto;
using WebApiSmartClinic.Services.CadastroCliente;
using WebApiSmartClinic.Services.Categoria;
using WebApiSmartClinic.Services.CentroCusto;
using WebApiSmartClinic.Services.Configuracoes;
using WebApiSmartClinic.Services.ConnectionsService;
using WebApiSmartClinic.Services.Conselho;
using WebApiSmartClinic.Services.Convenio;
using WebApiSmartClinic.Services.DespesaFixa;
using WebApiSmartClinic.Services.EmpresaPermissao;
using WebApiSmartClinic.Services.Evolucao;
using WebApiSmartClinic.Services.Exercicio;
using WebApiSmartClinic.Services.FichaAvaliacao;
using WebApiSmartClinic.Services.Financ_Pagar;
using WebApiSmartClinic.Services.Financ_Receber;
using WebApiSmartClinic.Services.FormaPagamento;
using WebApiSmartClinic.Services.Fornecedor;
using WebApiSmartClinic.Services.HistoricoTransacao;
using WebApiSmartClinic.Services.LogUsuario;
using WebApiSmartClinic.Services.MailService;
using WebApiSmartClinic.Services.Paciente;
using WebApiSmartClinic.Services.Plano;
using WebApiSmartClinic.Services.PlanoConta;
using WebApiSmartClinic.Services.Procedimento;
using WebApiSmartClinic.Services.Profissao;
using WebApiSmartClinic.Services.Profissional;
using WebApiSmartClinic.Services.Sala;
using WebApiSmartClinic.Services.Status;
using WebApiSmartClinic.Services.StripeService;
using WebApiSmartClinic.Services.SubCentroCusto;
using WebApiSmartClinic.Services.TipoPagamento;
using Microsoft.AspNetCore.Diagnostics;
using WebApiSmartClinic.Services.Pacote;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

// AppSettings e JWT
var appSettingsSection = config.GetSection("AppSettings");
services.Configure<AppSettings>(appSettingsSection);
var appSettings = appSettingsSection.Get<AppSettings>();
var key = Encoding.UTF8.GetBytes(appSettings.JwtSecretKey);
services.AddProblemDetails();
// Middleware de conexão por tenant
//services.AddSingleton<IConnectionStringProvider, ConnectionStringProvider>();

// DbContexts
services.AddDbContext<DataConnectionContext>(options =>
    options.UseNpgsql(config.GetConnectionString("ConnectionsContext")));

builder.Services.AddDbContext<AppDbContext>();

// Identity
services.AddIdentity<User, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

// JWT
services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = appSettings.JwtIssuer,
        ValidAudience = appSettings.JwtAudience,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

// Controllers + JSON
services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    x.JsonSerializerOptions.WriteIndented = true;
});

// Cultura pt-BR
services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { new CultureInfo("pt-BR") };
    options.DefaultRequestCulture = new RequestCulture("pt-BR");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
// Swagger
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApiSmartClinic", Version = "v1" });
    c.EnableAnnotations();
    
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Digite 'Bearer' [espaço] e então seu token JWT."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        { 
            new OpenApiSecurityScheme 
            {
                Reference = new OpenApiReference 
                {
                    Type = ReferenceType.SecurityScheme, 
                    Id = "Bearer" 
                } 
            },
            new string[] {} 
        }
    });

    c.OperationFilter<EmpresaHeaderOperationFilter>();
});

// Outros serviços (ex: CadastroClienteService, AuthService etc)
services.AddScoped<IAuthInterface, AuthService>();
services.AddScoped<AuthService>();
//services.AddScoped<ICadastroClienteInterface, CadastroClienteService>();
services.AddScoped<IConnectionsService, ConnectionsService>();

// Vincular interface com os servicos
services.AddScoped<ISalaInterface, SalaService>();
services.AddScoped<ITipoPagamentoInterface, TipoPagamentoService>();
services.AddScoped<IFornecedorInterface, FornecedorService>();
services.AddScoped<IFormaPagamentoInterface, FormaPagamentoService>();
services.AddScoped<ICentroCustoInterface, CentroCustoService>();
services.AddScoped<ISubCentroCustoInterface, SubCentroCustoService>();
services.AddScoped<IStatusInterface, StatusService>();
services.AddScoped<IPacienteInterface, PacienteService>();
services.AddScoped<IConvenioInterface, ConvenioService>();
services.AddScoped<IBancoInterface, BancoService>();
services.AddScoped<IConselhoInterface, ConselhoService>();
services.AddScoped<IProcedimentoInterface, ProcedimentoService>();
services.AddScoped<IBoletoInterface, BoletoService>();
services.AddScoped<ICategoriaInterface, CategoriaService>();
services.AddScoped<IFinanc_PagarInterface, Financ_PagarService>();
services.AddScoped<IFinanc_ReceberInterface, Financ_ReceberService>();
services.AddScoped<IHistoricoTransacaoInterface, HistoricoTransacaoService>();
//services.AddScoped<IComissaoInterface, ComissaoService>();
services.AddScoped<IPlanoInterface, PlanoService>();
services.AddScoped<IAgendaInterface, AgendaService>();
services.AddScoped<IProfissionalInterface, ProfissionalService>();
services.AddScoped<ILogUsuarioInterface, LogUsuarioService>();
services.AddScoped<IExercicioInterface, ExercicioService>();
services.AddScoped<IAtividadeInterface, AtividadeService>();
services.AddScoped<IEvolucaoInterface, EvolucaoService>();
services.AddScoped<IProfissaoInterface, ProfissaoService>();
services.AddScoped<IFichaAvaliacaoInterface, FichaAvaliacaoService>();
services.AddScoped<IEmailService, EmailService>();
services.AddScoped<IPlanoContaInterface, PlanoContaService>();
services.AddScoped<IDespesaFixaInterface, DespesaFixaService>();
services.AddScoped<IConfiguracoesInterface, ConfiguracoesService>();
services.AddScoped<ICadastroClienteInterface, CadastroClienteService>();
services.AddScoped<IConnectionStringProvider, ConnectionStringProvider>();
services.AddScoped<IConnectionsRepository, ConnectionsRepository>();
services.AddScoped<IComissaoService, WebApiSmartClinic.Services.ComissaoService>();
builder.Services.AddScoped<IStripeService, StripeService>();
services.AddScoped<AgendaService>();
services.AddScoped<IEmpresaPermissaoInterface, EmpresaPermissaoService>();
builder.Services.AddScoped<IPacoteInterface, PacoteService>();


builder.Services.AddHttpClient<IAsaasService, AsaasService>();
builder.Services.AddScoped<IAsaasService, AsaasService>();

//services.AddSingleton<IConnectionStringProvider, ConnectionStringProvider>();

services.AddHttpClient();

services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("https://clinicsmart.app.br", "http://localhost:4200", "https://viacep.com.br/ws/")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials()
              .WithExposedHeaders("Authorization");
    });
});

builder.Services.AddAutoMapper(typeof(_AssemblyMarkerProfile));

var app = builder.Build();

// 1) Exceptions (uma vez só) – resposta JSON sem reexecutar pipeline
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async ctx =>
    {
        var ex = ctx.Features.Get<IExceptionHandlerFeature>()?.Error;
        var problem = Results.Problem(
            detail: ex?.Message,
            statusCode: StatusCodes.Status500InternalServerError);
        await problem.ExecuteAsync(ctx);
    });
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseRouting();

// 2) Conexão do tenant PARA rotas anônimas (Auth/login|register) e qualquer outra
//    que precise da connection ANTES de autenticarmos.

// 3) Autenticação (preenche HttpContext.User – não reexecuta pipeline)

app.UseAuthentication(); // 0
app.UseMiddleware<ConnectionStringMiddleware>(); // 1
app.UseMiddleware<TenantMiddleware>(); // 2

app.UseMiddleware<EmpresaContextoMiddleware>(); // 3

// 4) TenantMiddleware – para requests AUTENTICADOS pegar a connection via claim UserKey
//    (e opcionalmente validar acesso).

// 5) Contexto de empresa/visibilidade – apenas para API e idempotente
//app.UseWhen(ctx => ctx.Request.Path.StartsWithSegments("/api"), branch =>
//{
//});

app.UseAuthorization();

app.MapControllers();

app.Run();


//var app = builder.Build();
//app.UseExceptionHandler("/error");
//app.UseExceptionHandler(exceptionApp =>
//{
//    exceptionApp.Run(async ctx =>
//    {
//        var ex = ctx.Features.Get<IExceptionHandlerFeature>()?.Error;
//        var problem = Results.Problem(
//            detail: ex?.Message,
//            statusCode: StatusCodes.Status500InternalServerError);
//        await problem.ExecuteAsync(ctx);
//    });
//});
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();
//app.UseCors("AllowFrontend");
//app.UseRouting();


////app.UseAuthentication();
////  Middleware de tenant (deve vir antes do auth)
//app.UseMiddleware<TenantMiddleware>();
//app.UseWhen(ctx => ctx.Request.Path.StartsWithSegments("/api"), branch =>
//{
//    branch.UseMiddleware<EmpresaContextoMiddleware>();
//});

//app.UseAuthorization();

//app.UseMiddleware<ConnectionStringMiddleware>();


//app.MapControllers();

//app.Run();
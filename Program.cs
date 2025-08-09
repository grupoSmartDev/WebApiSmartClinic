using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Services.Autor;
using WebApiSmartClinic.Services.CentroCusto;
using WebApiSmartClinic.Services.Convenio;
using WebApiSmartClinic.Services.FormaPagamento;
using WebApiSmartClinic.Services.Fornecedor;
using WebApiSmartClinic.Services.Sala;
using WebApiSmartClinic.Services.Paciente;
using WebApiSmartClinic.Services.Status;
using WebApiSmartClinic.Services.SubCentroCusto;
using WebApiSmartClinic.Services.TipoPagamento;
using WebApiSmartClinic.Services.Banco;
using WebApiSmartClinic.Services.Conselho;
using WebApiSmartClinic.Services.Procedimento;
using WebApiSmartClinic.Services.Boleto;
using WebApiSmartClinic.Services.Categoria;
using WebApiSmartClinic.Services.Financ_Pagar;
using WebApiSmartClinic.Services.Financ_Receber;
using Microsoft.OpenApi.Models;
using WebApiSmartClinic.Services.HistoricoTransacao;
using WebApiSmartClinic.Services.Comissao;
using WebApiSmartClinic.Services.Plano;
using WebApiSmartClinic.Services.Agenda;
using WebApiSmartClinic.Services.Profissional;
using System.Globalization;
using WebApiSmartClinic.Services.LogUsuario;
using WebApiSmartClinic.Services.Exercicio;
using WebApiSmartClinic.Services.Atividade;
using WebApiSmartClinic.Services.Evolucao;
using WebApiSmartClinic.Services.Profissao;
using WebApiSmartClinic.Services.FichaAvaliacao;
using WebApiSmartClinic.Services.PlanoConta;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using System.Text.Json.Serialization;
using System.Text;
using WebApiSmartClinic.Helpers;
using WebApiSmartClinic.Models;
using WebApiSmartClinic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WebApiSmartClinic.Services.ConnectionsService;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using WebApiSmartClinic.Services.CadastroCliente;
using WebApiSmartClinic.Services.Auth;
using WebApiSmartClinic.Services.DespesaFixa;
using WebApiSmartClinic.Services.MailService;
using WebApiSmartClinic.Services.StripeService;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

// AppSettings e JWT
var appSettingsSection = config.GetSection("AppSettings");
services.Configure<AppSettings>(appSettingsSection);
var appSettings = appSettingsSection.Get<AppSettings>();
var key = Encoding.UTF8.GetBytes(appSettings.JwtSecretKey);

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
        { new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } }, new string[] {} }
    });
});

// Outros serviços (ex: CadastroClienteService, AuthService etc)
services.AddScoped<IAuthInterface, AuthService>();
services.AddScoped<AuthService>();
services.AddScoped<ICadastroClienteInterface, CadastroClienteService>();
services.AddScoped<IConnectionsService, ConnectionsService>();

// Vincular interface com os servicos
services.AddScoped<IAutorInterface, AutorService>();
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
services.AddScoped<IComissaoInterface, ComissaoService>();
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
services.AddScoped<ICadastroClienteInterface, CadastroClienteService>();
services.AddScoped<IConnectionStringProvider, ConnectionStringProvider>();
services.AddScoped<IConnectionsRepository, ConnectionsRepository>();
builder.Services.AddScoped<IStripeService, StripeService>();
services.AddScoped<AgendaService>();

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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseRouting();

//  Middleware de tenant (deve vir antes do auth)
app.UseMiddleware<TenantMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<ConnectionStringMiddleware>();

app.MapControllers();

app.Run();

//var builder = WebApplication.CreateBuilder(args);

//// add services to DI container
//{
//    var services = builder.Services;
//    var env = builder.Environment;

//    // Adiciona o contexto do Entity Framework Core
//    builder.Services.AddDbContext<DataConnectionContext>(options =>
//        options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionsContext")));

//    builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
//    {
//        var connectionStringProvider = serviceProvider.GetRequiredService<IConnectionStringProvider>();
//        var connectionString = connectionStringProvider.GetConnectionString() ?? builder.Configuration.GetConnectionString("DefaultContext");
//        options.UseNpgsql(connectionString);
//    });

//    services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<AppDbContext>()
//    .AddEntityFrameworkStores<DataConnectionContext>()
//    .AddDefaultTokenProviders();

//    services.AddCors();
//    services.AddControllers().AddJsonOptions(x =>
//    {
//        // serialize enums as strings in api responses (e.g. Role)
//        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

//        // ignore omitted parameters on models to enable optional params (e.g. User update)
//        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

//        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
//        x.JsonSerializerOptions.WriteIndented = true;

//    });

//    services.AddAuthorization(options =>
//    {
//        options.FallbackPolicy = new AuthorizationPolicyBuilder()
//            .RequireAssertion(context =>
//            {
//                // Recupera o endpoint atual (só funciona em .NET 5+ com endpoint routing)
//                var endpoint = context.Resource as Microsoft.AspNetCore.Http.Endpoint;

//                // Verifica se existe algum atributo [AllowAnonymous]
//                var allowAnonymous = endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null;

//                // Se tiver [AllowAnonymous], libera
//                if (allowAnonymous) return true;

//                // Senão, exige que esteja autenticado
//                return context.User.Identity?.IsAuthenticated == true;
//            })
//            .Build();
//    });

//    services.Configure<ConnectionStringConfig>(builder.Configuration);
//    services.Configure<RequestLocalizationOptions>(options =>
//    {
//        var supportedCultures = new[]
//        {
//        new CultureInfo("pt-BR"),
//    };

//        options.DefaultRequestCulture = new RequestCulture("pt-BR");
//        options.SupportedCultures = supportedCultures;
//        options.SupportedUICultures = supportedCultures;
//    });

//    // Configure JWT Authentication
//    // Configuração JWT
//    var appSettingsSection = builder.Configuration.GetSection("AppSettings");
//    builder.Services.Configure<AppSettings>(appSettingsSection);
//    var appSettings = appSettingsSection.Get<AppSettings>();

//    var key = Encoding.UTF8.GetBytes(appSettings.JwtSecretKey);
//    builder.Services.AddAuthentication(x =>
//    {
//        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    })
//    .AddJwtBearer(x =>
//    {
//        x.RequireHttpsMetadata = false;
//        x.SaveToken = true;
//        x.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuerSigningKey = true,
//            IssuerSigningKey = new SymmetricSecurityKey(key),
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidIssuer = appSettings.JwtIssuer,
//            ValidAudience = appSettings.JwtAudience,
//            ValidateLifetime = true,
//            ClockSkew = TimeSpan.Zero
//        };
//    });

//    // Vincular interface com os servicos
//    builder.Services.AddScoped<IAutorInterface, AutorService>();
//    builder.Services.AddScoped<ISalaInterface, SalaService>();
//    builder.Services.AddScoped<ITipoPagamentoInterface, TipoPagamentoService>();
//    builder.Services.AddScoped<IFornecedorInterface, FornecedorService>();
//    builder.Services.AddScoped<IFormaPagamentoInterface, FormaPagamentoService>();
//    builder.Services.AddScoped<ICentroCustoInterface, CentroCustoService>();
//    builder.Services.AddScoped<ISubCentroCustoInterface, SubCentroCustoService>();
//    builder.Services.AddScoped<IStatusInterface, StatusService>();
//    builder.Services.AddScoped<IPacienteInterface, PacienteService>();
//    builder.Services.AddScoped<IConvenioInterface, ConvenioService>();
//    builder.Services.AddScoped<IBancoInterface, BancoService>();
//    builder.Services.AddScoped<IConselhoInterface, ConselhoService>();
//    builder.Services.AddScoped<IProcedimentoInterface, ProcedimentoService>();
//    builder.Services.AddScoped<IBoletoInterface, BoletoService>();
//    builder.Services.AddScoped<ICategoriaInterface, CategoriaService>();
//    builder.Services.AddScoped<IFinanc_PagarInterface, Financ_PagarService>();
//    builder.Services.AddScoped<IFinanc_ReceberInterface, Financ_ReceberService>();
//    builder.Services.AddScoped<IHistoricoTransacaoInterface, HistoricoTransacaoService>();
//    builder.Services.AddScoped<IComissaoInterface, ComissaoService>();
//    builder.Services.AddScoped<IPlanoInterface, PlanoService>();
//    builder.Services.AddScoped<IAgendaInterface, AgendaService>();
//    builder.Services.AddScoped<IProfissionalInterface, ProfissionalService>();
//    builder.Services.AddScoped<ILogUsuarioInterface, LogUsuarioService>();
//    builder.Services.AddScoped<IExercicioInterface, ExercicioService>();
//    builder.Services.AddScoped<IAtividadeInterface, AtividadeService>();
//    builder.Services.AddScoped<IEvolucaoInterface, EvolucaoService>();
//    builder.Services.AddScoped<IProfissaoInterface, ProfissaoService>();
//    builder.Services.AddScoped<IFichaAvaliacaoInterface, FichaAvaliacaoService>();
//    builder.Services.AddScoped<IPlanoContaInterface, PlanoContaService>();
//    builder.Services.AddScoped<IDespesaFixaInterface, DespesaFixaService>();
//    builder.Services.AddScoped<ICadastroClienteInterface, CadastroClienteService>();

//    //builder.Services.AddScoped<IConnectionStringProvider, ConnectionStringProvider>();
//    builder.Services.AddSingleton<IConnectionStringProvider, ConnectionStringProvider>();

//    builder.Services.AddScoped<AgendaService>();
//    builder.Services.AddScoped<AuthService>();
//    builder.Services.AddScoped<IAuthInterface, AuthService>();


//    builder.Services.AddScoped<IConnectionsRepository, ConnectionsRepository>();
//    // Registrar IConnectionsRepository e ConnectionsRepository
//    builder.Services.AddScoped<IConnectionsRepository, ConnectionsRepository>();

//    // Registrar IConnectionsService e ConnectionsService
//    builder.Services.AddScoped<IConnectionsService, ConnectionsService>();
//}

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowFrontend", policy =>
//    {
//        policy.WithOrigins("http://localhost:4200", "https://smart-clinic-angular-it7o.vercel.app", "https://smart-clinic-angular-tsxt.vercel.app", "https://viacep.com.br/ws/")
//              .AllowAnyMethod()
//              .AllowAnyHeader()
//              .WithExposedHeaders("Authorization")
//              .AllowCredentials();
//    });
//});

//// Na configuração do app
//// IMPORTANTE: Colocar ANTES 

//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApiSmartClinic", Version = "v1" });
//    c.EnableAnnotations(); // Habilita as anotações do Swagger

//    c.OperationFilter<AddRequiredHeaderParameter>();
//    // JWT Bearer Authentication configuration
//    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        Name = "Authorization",
//        Type = SecuritySchemeType.ApiKey,
//        Scheme = "bearer",
//        BearerFormat = "JWT",
//        In = ParameterLocation.Header,
//        Description = "Digite 'Bearer' [espaço] e então seu token JWT."
//    });

//    c.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {
//            new OpenApiSecurityScheme
//            {
//                Reference = new OpenApiReference
//                {
//                    Type = ReferenceType.SecurityScheme,
//                    Id = "Bearer"
//                }
//            },
//            Array.Empty<string>()
//        }
//    });

//    //c.OperationFilter<AllowAnonymousOperationFilter>();
//});


//builder.Services.AddHttpClient();

//var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//// CORS antes da autenticação
//app.UseCors("AllowFrontend");

//app.UseRouting();

//// Autenticação antes da autorização
//app.UseAuthentication();
//app.UseAuthorization();

//// Seus middlewares personalizados depois
//app.UseMiddleware<ErrorHandlerMiddleware>();
//app.UseMiddleware<ConnectionStringMiddleware>();

//app.MapControllers();

//app.Run();
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
using WebApiSmartClinic.Services.Auth;
using WebApiSmartClinic.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sua API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
});

// Configure DbContexts
builder.Services.AddDbContext<MasterDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao"),
        sqlOptions => sqlOptions.EnableRetryOnFailure());
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer("Server=dummy",
        sqlOptions => sqlOptions.EnableRetryOnFailure());
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
                };
            });

// Configure CORS - Versão corrigida
var corsOrigins = builder.Configuration.GetSection("Cors:Origins").Get<string[]>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultPolicy", policy =>
    {
        policy.WithOrigins(corsOrigins ?? new[] { "http://localhost:4200" })
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Register Services
builder.Services.AddScoped<IAuthService, AuthService>();
// Vincular interface com os servicos
builder.Services.AddScoped<IAutorInterface, AutorService>();
builder.Services.AddScoped<ISalaInterface, SalaService>();
builder.Services.AddScoped<ITipoPagamentoInterface, TipoPagamentoService>();
builder.Services.AddScoped<IFornecedorInterface, FornecedorService>();
builder.Services.AddScoped<IFormaPagamentoInterface, FormaPagamentoService>();
builder.Services.AddScoped<ICentroCustoInterface, CentroCustoService>();
builder.Services.AddScoped<ISubCentroCustoInterface, SubCentroCustoService>();
builder.Services.AddScoped<IStatusInterface, StatusService>();
builder.Services.AddScoped<IPacienteInterface, PacienteService>();
builder.Services.AddScoped<IConvenioInterface, ConvenioService>();
builder.Services.AddScoped<IBancoInterface, BancoService>();
builder.Services.AddScoped<IConselhoInterface, ConselhoService>();
builder.Services.AddScoped<IProcedimentoInterface, ProcedimentoService>();
builder.Services.AddScoped<IBoletoInterface, BoletoService>();
builder.Services.AddScoped<ICategoriaInterface, CategoriaService>();
builder.Services.AddScoped<IFinanc_PagarInterface, Financ_PagarService>();
builder.Services.AddScoped<IFinanc_ReceberInterface, Financ_ReceberService>();
builder.Services.AddScoped<IHistoricoTransacaoInterface, HistoricoTransacaoService>();
builder.Services.AddScoped<IComissaoInterface, ComissaoService>();
builder.Services.AddScoped<IPlanoInterface, PlanoService>();
builder.Services.AddScoped<IAgendaInterface, AgendaService>();
builder.Services.AddScoped<IProfissionalInterface, ProfissionalService>();
builder.Services.AddScoped<ILogUsuarioInterface, LogUsuarioService>();
builder.Services.AddScoped<IExercicioInterface, ExercicioService>();
builder.Services.AddScoped<IAtividadeInterface, AtividadeService>();
builder.Services.AddScoped<IEvolucaoInterface, EvolucaoService>();
builder.Services.AddScoped<IProfissaoInterface, ProfissaoService>();
builder.Services.AddScoped<IFichaAvaliacaoInterface, FichaAvaliacaoService>();
builder.Services.AddScoped<IPlanoContaInterface, PlanoContaService>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("DefaultPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<EmpresasMiddleware>();
app.MapControllers();

app.Run();
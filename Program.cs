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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

var cultureInfo = new CultureInfo("pt-BR"); // Use "en-US" para separador decimal como ponto
cultureInfo.NumberFormat.NumberDecimalSeparator = ",";
cultureInfo.NumberFormat.NumberGroupSeparator = ".";

// Aplica a cultura a nível global
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.WriteIndented = true;
    });


builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Culture = new CultureInfo("pt-BR");
    });

// Add CORS service (libera acesso para qualquer origem)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApiSmartClinic", Version = "v1" });
    c.EnableAnnotations(); // Habilita as anotações do Swagger
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao"));
});

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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
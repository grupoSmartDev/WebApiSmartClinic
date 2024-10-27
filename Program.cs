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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

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
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao"));
});

// Vincular interface com os servi�os
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use CORS middleware (aplicando a pol�tica que permite tudo)
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();

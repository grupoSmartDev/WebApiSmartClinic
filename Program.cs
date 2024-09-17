using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Services.Autor;
using WebApiSmartClinic.Services.Status;
using WebApiSmartClinic.Services.TipoPagamento;

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

// Vincular interface com os serviços
builder.Services.AddScoped<IAutorInterface, AutorService>();
builder.Services.AddScoped<IStatusInterface, StatusService>();
builder.Services.AddScoped<ITipoPagamentoInterface, TipoPagamentoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use CORS middleware (aplicando a política que permite tudo)
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();

using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<AutorModel> Autores { get; set; }
    public DbSet<LivroModel> Livros { get; set; }
    public DbSet<StatusModel> Status { get; set; }
    public DbSet<TipoPagamentoModel> TipoPagamento { get; set; }
    public DbSet<FormaPagamentoModel> FormaPagamento { get; set; }
    public DbSet<FornecedorModel> Fornecedor { get; set; }
    public DbSet<CentroCustoModel> CentroCusto { get; set; }

}

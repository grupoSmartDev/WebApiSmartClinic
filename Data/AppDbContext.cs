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
    public DbSet<CentroCustoModel> CentroCusto { get; set; }  // Define uma tabela CentroCusto no banco de dados
    public DbSet<SubCentroCustoModel> SubCentroCusto { get; set; }  // Define uma tabela SubCentroCusto no banco de dados

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configura a entidade CentroCustoModel para ter uma rela��o de um para muitos com SubCentroCustoModel
        modelBuilder.Entity<CentroCustoModel>()  // Inicia a configura��o para a entidade CentroCustoModel
            .HasMany(c => c.SubCentrosCusto)  // Um CentroCustoModel pode ter muitos SubCentroCustoModel associados
            .WithOne(sc => sc.CentroCusto)  // Cada SubCentroCustoModel est� relacionado com um �nico CentroCustoModel
            .HasForeignKey(sc => sc.CentroCustoId);  // O campo CentroCustoId em SubCentroCustoModel � a chave estrangeira que referencia o Id de CentroCustoModel
    }
}

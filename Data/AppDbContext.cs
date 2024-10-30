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
    public DbSet<PacienteModel> Paciente { get; set; }
    public DbSet<ConvenioModel> Convenio { get; set; }
    public DbSet<CentroCustoModel> CentroCusto { get; set; }  // Define uma tabela CentroCusto no banco de dados
    public DbSet<SubCentroCustoModel> SubCentroCusto { get; set; }  // Define uma tabela SubCentroCusto no banco de dados
    public DbSet<SalaModel> Sala{ get; set; }
    public DbSet<BancoModel> Banco { get; set; }
    public DbSet<ConselhoModel> Conselho { get; set; }
    public DbSet<ProcedimentoModel> Procedimento { get; set; }
    public DbSet<BoletoModel> Boleto { get; set; }
    public DbSet<CategoriaModel> Categoria { get; set; }
    public DbSet<Financ_PagarModel> Financ_Pagar { get; set; }
    public DbSet<Financ_ReceberModel> Financ_Receber { get; set; }
    public DbSet<ProfissionalModel> Profissional { get; set; }
    public DbSet<UsuarioModel> Usuario { get; set; }
    public DbSet<HistoricoTransacaoModel> HistoricoTransacao { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configura a entidade CentroCustoModel para ter uma relação de um para muitos com SubCentroCustoModel
        modelBuilder.Entity<CentroCustoModel>()  // Inicia a configuração para a entidade CentroCustoModel
            .HasMany(c => c.SubCentrosCusto)  // Um CentroCustoModel pode ter muitos SubCentroCustoModel associados
            .WithOne(sc => sc.CentroCusto)  // Cada SubCentroCustoModel está relacionado com um único CentroCustoModel
            .HasForeignKey(sc => sc.CentroCustoId);  // O campo CentroCustoId em SubCentroCustoModel é a chave estrangeira que referencia o Id de CentroCustoModel
    }
}

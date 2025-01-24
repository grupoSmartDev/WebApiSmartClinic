using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebApiSmartClinic.Helpers;
using Microsoft.Extensions.Options;

namespace WebApiSmartClinic.Data;

public class AppDbContext : IdentityDbContext<User>
{
    private readonly ConnectionStringConfig _connectionStringConfig;
    private readonly IConnectionStringProvider _connectionStringProvider;

    public AppDbContext(DbContextOptions<AppDbContext> options, IOptions<ConnectionStringConfig> connectionStringConfig, IConnectionStringProvider connectionStringProvider)
     : base(options)
    {
        _connectionStringConfig = connectionStringConfig.Value;
        _connectionStringProvider = connectionStringProvider;
    }
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
    public DbSet<SalaModel> Sala { get; set; }
    public DbSet<BancoModel> Banco { get; set; }
    public DbSet<ConselhoModel> Conselho { get; set; }
    public DbSet<ProcedimentoModel> Procedimento { get; set; }
    public DbSet<BoletoModel> Boleto { get; set; }
    public DbSet<CategoriaModel> Categoria { get; set; }
    public DbSet<Financ_PagarModel> Financ_Pagar { get; set; }
    public DbSet<Financ_PagarSubModel> Financ_PagarSub { get; set; }
    public DbSet<Financ_ReceberModel> Financ_Receber { get; set; }
    public DbSet<Financ_ReceberSubModel> Financ_ReceberSub { get; set; }
    public DbSet<ProfissionalModel> Profissional { get; set; }
    public DbSet<UsuarioModel> Usuario { get; set; }
    public DbSet<HistoricoTransacaoModel> HistoricoTransacao { get; set; }
    public DbSet<ComissaoModel> Comissao { get; set; }
    public DbSet<PlanoModel> Plano { get; set; }
    public DbSet<AgendaModel> Agenda { get; set; }
    public DbSet<LogUsuarioModel> LogUsuario { get; set; }
    public DbSet<ExercicioModel> Exercicio { get; set; }
    public DbSet<AtividadeModel> Atividade { get; set; }
    public DbSet<EvolucaoModel> Evolucoes { get; set; }
    public DbSet<ProfissaoModel> Profissao { get; set; }
    public DbSet<FichaAvaliacaoModel> FichaAvaliacao { get; set; }
    public DbSet<EmpresaModel> Empresa { get; set; }
    public DbSet<PlanoContaModel> PlanoConta { get; set; }
    public DbSet<PlanoContaSubModel> PlanoContaSub { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_connectionStringProvider.GetConnectionString());
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Papéis para o Identity
        var roles = new List<IdentityRole>
        {
            new IdentityRole { Id = "1", Name = "User", NormalizedName = "USER" },
            new IdentityRole { Id = "2", Name = "Support", NormalizedName = "SUPPORT" },
            new IdentityRole { Id = "3", Name = "Admin", NormalizedName = "ADMIN" }
        };

        modelBuilder.Entity<IdentityRole>().HasData(roles);

        modelBuilder.Entity<PlanoModel>()
          .HasOne(p => p.Paciente)
          .WithOne(p => p.Plano)
          .HasForeignKey<PacienteModel>(p => p.PlanoId)
          .IsRequired(false);

        // Relacionamento: Paciente -> Evoluções
        modelBuilder.Entity<PacienteModel>()
            .HasMany(p => p.Evolucoes)
            .WithOne(e => e.Paciente)
            .HasForeignKey(e => e.PacienteId)
            .OnDelete(DeleteBehavior.Cascade); // Exclusão em cascata: remove evoluções ao excluir paciente

        // Relacionamento: Evolução -> Exercícios
        modelBuilder.Entity<EvolucaoModel>()
            .HasMany(e => e.Exercicios)
            .WithOne(e => e.Evolucao) // Navegação reversa
            .HasForeignKey(e => e.EvolucaoId) // FK configurada corretamente
            .OnDelete(DeleteBehavior.Cascade); // Exclusão em cascata: remove exercícios ao excluir evolução

        // Relacionamento: Evolução -> Atividades
        modelBuilder.Entity<EvolucaoModel>()
            .HasMany(e => e.Atividades)
            .WithOne(a => a.Evolucao) // Navegação reversa
            .HasForeignKey(a => a.EvolucaoId) // FK configurada corretamente
            .OnDelete(DeleteBehavior.Cascade); // Exclusão em cascata: remove atividades ao excluir evolução

        // Relacionamento: Centro de Custo -> SubCentro
        modelBuilder.Entity<CentroCustoModel>()
            .HasMany(c => c.SubCentrosCusto)
            .WithOne(sc => sc.CentroCusto)
            .HasForeignKey(sc => sc.CentroCustoId);

        // Relacionamento: Financ_Receber -> SubFinanc_Receber
        modelBuilder.Entity<Financ_ReceberModel>()
            .HasMany(f => f.subFinancReceber)
            .WithOne(s => s.FinancReceber)
            .HasForeignKey(s => s.financReceberId);

        // Relacionamento: Financ_Pagar -> SubFinanc_Pagar
        modelBuilder.Entity<Financ_PagarModel>()
            .HasMany(f => f.subFinancPagar)
            .WithOne(s => s.FinancPagar)
            .HasForeignKey(s => s.financPagarId);

        // Relacionamento: PlanoConta -> SubPlanoConta
        modelBuilder.Entity<PlanoContaModel>()
            .HasMany(f => f.SubPlanos)
            .WithOne(s => s.PlanoConta)
            .HasForeignKey(s => s.PlanoContaId);
    }
}

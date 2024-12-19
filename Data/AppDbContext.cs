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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configura a entidade CentroCustoModel para ter uma relação de um para muitos com SubCentroCustoModel
        modelBuilder.Entity<CentroCustoModel>()  // Inicia a configuração para a entidade CentroCustoModel
            .HasMany(c => c.SubCentrosCusto)  // Um CentroCustoModel pode ter muitos SubCentroCustoModel associados
            .WithOne(sc => sc.CentroCusto)  // Cada SubCentroCustoModel está relacionado com um único CentroCustoModel
            .HasForeignKey(sc => sc.CentroCustoId);  // O campo CentroCustoId em SubCentroCustoModel é a chave estrangeira que referencia o Id de CentroCustoModel

        // Configura a entidade Financ_ReceberModel para ter uma relação de um para muitos com Financ_ReceberSubModel
        modelBuilder.Entity<Financ_ReceberModel>()
            .HasMany(f => f.subFinancReceber)  // Um Financ_ReceberModel pode ter muitas Financ_ReceberSubModel associadas
            .WithOne()  // Cada Financ_ReceberSubModel pertence a um único Financ_ReceberModel
            .HasForeignKey(p => p.financReceberId);  // O campo Financ_ReceberId é a chave estrangeira que referencia o Id de Financ_ReceberModel

        modelBuilder.Entity<PacienteModel>()
        .HasOne(p => p.Plano)
        .WithOne()
        .HasForeignKey<PacienteModel>(p => p.PlanoId)
        .OnDelete(DeleteBehavior.Restrict); // Ou Cascade, conforme necessidade


        modelBuilder.Entity<EvolucaoModel>()
             .HasMany(e => e.Exercicios) // Evolução tem vários exercícios
             .WithOne() // Relacionamento não tem navegação reversa
             .HasForeignKey("EvolucaoId") // Define EvolucaoId como chave estrangeira
             .OnDelete(DeleteBehavior.Cascade); // Configuração de exclusão em cascata

        // Configuração para Evolucao -> Atividade
        modelBuilder.Entity<EvolucaoModel>()
            .HasMany(e => e.Atividades) // Evolução tem várias atividades
            .WithOne() // Relacionamento não tem navegação reversa
            .HasForeignKey("EvolucaoId") // Define EvolucaoId como chave estrangeira
            .OnDelete(DeleteBehavior.Cascade); // Configuração de exclusão em cascata

        //configuração para financ receber e sub financ receber
        modelBuilder.Entity<Financ_ReceberModel>()
      .HasKey(f => f.Id);

        modelBuilder.Entity<Financ_ReceberSubModel>()
            .HasKey(s => s.Id);

        modelBuilder.Entity<Financ_ReceberSubModel>()
            .HasOne(s => s.FinancReceber)
            .WithMany(f => f.subFinancReceber)
            .HasForeignKey(s => s.financReceberId);

        //configuração para financ receber e sub financ receber
        modelBuilder.Entity<Financ_PagarModel>()
      .HasKey(f => f.Id);

        modelBuilder.Entity<Financ_PagarSubModel>()
            .HasKey(s => s.Id);

        modelBuilder.Entity<Financ_PagarSubModel>()
            .HasOne(s => s.FinancPagar)
            .WithMany(f => f.subFinancPagar)
            .HasForeignKey(s => s.financPagarId);


        base.OnModelCreating(modelBuilder);
    }
}

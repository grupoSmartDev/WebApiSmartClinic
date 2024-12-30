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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configura a entidade CentroCustoModel para ter uma rela��o de um para muitos com SubCentroCustoModel
        modelBuilder.Entity<CentroCustoModel>()  // Inicia a configura��o para a entidade CentroCustoModel
            .HasMany(c => c.SubCentrosCusto)  // Um CentroCustoModel pode ter muitos SubCentroCustoModel associados
            .WithOne(sc => sc.CentroCusto)  // Cada SubCentroCustoModel est� relacionado com um �nico CentroCustoModel
            .HasForeignKey(sc => sc.CentroCustoId);  // O campo CentroCustoId em SubCentroCustoModel � a chave estrangeira que referencia o Id de CentroCustoModel

        // Configura a entidade Financ_ReceberModel para ter uma rela��o de um para muitos com Financ_ReceberSubModel
        modelBuilder.Entity<Financ_ReceberModel>()
            .HasMany(f => f.subFinancReceber)  // Um Financ_ReceberModel pode ter muitas Financ_ReceberSubModel associadas
            .WithOne()  // Cada Financ_ReceberSubModel pertence a um �nico Financ_ReceberModel
            .HasForeignKey(p => p.financReceberId);  // O campo Financ_ReceberId � a chave estrangeira que referencia o Id de Financ_ReceberModel

        modelBuilder.Entity<PacienteModel>()
            .HasOne(p => p.Plano)
            .WithOne()
            .HasForeignKey<PacienteModel>(p => p.PlanoId)
            .OnDelete(DeleteBehavior.Restrict); // Ou Cascade, conforme necessidade


        modelBuilder.Entity<EvolucaoModel>()
             .HasMany(e => e.Exercicios) // Evolu��o tem v�rios exerc�cios
             .WithOne() // Relacionamento n�o tem navega��o reversa
             .HasForeignKey("EvolucaoId") // Define EvolucaoId como chave estrangeira
             .OnDelete(DeleteBehavior.Cascade); // Configura��o de exclus�o em cascata

        // Configura��o para Evolucao -> Atividade
        modelBuilder.Entity<EvolucaoModel>()
            .HasMany(e => e.Atividades) // Evolu��o tem v�rias atividades
            .WithOne() // Relacionamento n�o tem navega��o reversa
            .HasForeignKey("EvolucaoId") // Define EvolucaoId como chave estrangeira
            .OnDelete(DeleteBehavior.Cascade); // Configura��o de exclus�o em cascata

        //configura��o para financ receber e sub financ receber
        modelBuilder.Entity<Financ_ReceberModel>()
            .HasKey(f => f.Id);

        modelBuilder.Entity<Financ_ReceberSubModel>()
            .HasKey(s => s.Id);

        modelBuilder.Entity<Financ_ReceberSubModel>()
            .HasOne(s => s.FinancReceber)
            .WithMany(f => f.subFinancReceber)
            .HasForeignKey(s => s.financReceberId);

        //configura��o para financ pagar e sub financ pagar
        modelBuilder.Entity<Financ_PagarModel>()
            .HasKey(f => f.Id);

        modelBuilder.Entity<Financ_PagarSubModel>()
            .HasKey(s => s.Id);

        modelBuilder.Entity<Financ_PagarSubModel>()
            .HasOne(s => s.FinancPagar)
            .WithMany(f => f.subFinancPagar)
            .HasForeignKey(s => s.financPagarId);

        //configuracao para plano de contas e sub plano de contas
        modelBuilder.Entity<PlanoContaModel>()
            .HasKey(f => f.Id);

        modelBuilder.Entity<PlanoContaSubModel>()
            .HasKey(s => s.Id);

        modelBuilder.Entity<PlanoContaSubModel>()
            .HasOne(s => s.PlanoConta)
            .WithMany(f => f.SubPlanos)
            .HasForeignKey(s => s.PlanoContaId);


        base.OnModelCreating(modelBuilder);
    }
}

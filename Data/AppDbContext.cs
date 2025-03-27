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
    public DbSet<RecorrenciaPacienteModel> RecorrenciaPaciente { get; set; }
    public DbSet<CadastroClienteModel> CadastroCliente { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(_connectionStringProvider.GetConnectionString());
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IdentityRole>().HasData(
            new List<IdentityRole>
            {
                new IdentityRole { Id = "1", Name = "User", NormalizedName = "USER" },
                new IdentityRole { Id = "2", Name = "Support", NormalizedName = "SUPPORT" },
                new IdentityRole { Id = "3", Name = "Admin", NormalizedName = "ADMIN" }
            }
        );

        modelBuilder.Entity<ConselhoModel>().HasData(
            new ConselhoModel { Id = 1, Nome = "Conselho Regional de Fisioterapia e Terapia Ocupacional", Sigla = "CREFITO", IsSystemDefault = true }
        );

        modelBuilder.Entity<ProfissaoModel>().HasData(
            new ProfissaoModel { Id = 1, Descricao = "Psicólogo", IsSystemDefault = true }
        );

        modelBuilder.Entity<StatusModel>().HasData(
            new StatusModel { Id = 1, Cor = "#00FF00", Legenda = "Agendamento realizado com sucesso!", Status = "Agendado", IsSystemDefault = true }
        );

        modelBuilder.Entity<FormaPagamentoModel>().HasData(
            new FormaPagamentoModel { Id = 1, Descricao = "À Vista", Parcelas = 1, IsSystemDefault = true },
            new FormaPagamentoModel { Id = 2, Descricao = "A Prazo - 2x", Parcelas = 2, IsSystemDefault = true },
            new FormaPagamentoModel { Id = 3, Descricao = "A Prazo - 3x", Parcelas = 3, IsSystemDefault = true }
        );

        modelBuilder.Entity<TipoPagamentoModel>().HasData(
            new TipoPagamentoModel { Id = 1, Descricao = "Dinheiro", IsSystemDefault = true },
            new TipoPagamentoModel { Id = 2, Descricao = "Cartão de Crédito", IsSystemDefault = true },
            new TipoPagamentoModel { Id = 3, Descricao = "Cartão de Débito", IsSystemDefault = true },
            new TipoPagamentoModel { Id = 4, Descricao = "Boleto", IsSystemDefault = true },
            new TipoPagamentoModel { Id = 5, Descricao = "Pix", IsSystemDefault = true },
            new TipoPagamentoModel { Id = 6, Descricao = "Depósito", IsSystemDefault = true }
        );

        modelBuilder.Entity<ConvenioModel>().HasData(
            new ConvenioModel { Id = 1, Nome = "Unimed", PeriodoCarencia = "0", Ativo = true, RegistroAvs = "ABC", Telefone = "3434-3434", Email = "email@email.com", IsSystemDefault = true },
            new ConvenioModel { Id = 2, Nome = "Santa Casa", PeriodoCarencia = "0", Ativo = true, RegistroAvs = "ABC", Telefone = "3434-3434", Email = "email@email.com", IsSystemDefault = true }
        );

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

        modelBuilder.Entity<FichaAvaliacaoModel>()
         .HasOne(f => f.Paciente)
         .WithOne(p => p.FichaAvaliacao)
         .HasForeignKey<FichaAvaliacaoModel>(f => f.PacienteId);

        // Configuração Profissional-FichaAvaliacao
        modelBuilder.Entity<FichaAvaliacaoModel>()
            .HasOne(f => f.Profissional)
            .WithMany(p => p.FichasAvaliacao)
            .HasForeignKey(f => f.ProfissionalId)
            .IsRequired(false)  // Temporariamente opcional
            .OnDelete(DeleteBehavior.SetNull);

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

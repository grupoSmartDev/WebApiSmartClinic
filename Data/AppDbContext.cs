using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebApiSmartClinic.Helpers;
using Microsoft.Extensions.Options;
using WebApiSmartClinic.Dto.User;

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
    public DbSet<SalaHorarioModel> SalaHorario { get; set; }
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
   // public DbSet<EmpresaModel> Empresa { get; set; }
    public DbSet<PlanoContaModel> PlanoConta { get; set; }
    public DbSet<PlanoContaSubModel> PlanoContaSub { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<RecorrenciaPacienteModel> RecorrenciaPaciente { get; set; }
    public DbSet<EmpresaModel> Empresas { get; set; }
    public DbSet<DespesaFixaModel> Despesas { get; set; }

    public DbSet<FilialModel> Filiais { get; set; }
    public DbSet<SubscriptionModel> SubsCricao{ get; set; }
    public DbSet<PaymentModel> Pagamentos{ get; set; }
    public DbSet<ComissaoCalculadaModel> Comissoes{ get; set; }
    public DbSet<PacientePlanoHistoricoModel> PacientePlanoHistoricos { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var conn = _connectionStringProvider.GetConnectionString();
            if (!string.IsNullOrWhiteSpace(conn))
            {
                optionsBuilder.UseNpgsql(conn);
            }
            else
            {
                throw new InvalidOperationException("Connection string do tenant não está definida.");
            }
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
            new ConselhoModel { Id = 1, Nome = "Conselho Regional de Fisioterapia e Terapia Ocupacional", Sigla = "CREFITO", IsSystemDefault = true },
            new ConselhoModel { Id = 2, Nome = "Conselho Federal de Psicologia", Sigla = "CFP", IsSystemDefault = true }
        );

        modelBuilder.Entity<ProfissaoModel>().HasData(
            new ProfissaoModel { Id = 1, Descricao = "Administrador(a)", IsSystemDefault = true, Ativo = true },
            new ProfissaoModel { Id = 2, Descricao = "Psicólogo(a)", IsSystemDefault = true, Ativo = true },
            new ProfissaoModel { Id = 3, Descricao = "Fisioterapeuta", IsSystemDefault = true, Ativo = true },
            new ProfissaoModel { Id = 4, Descricao = "Dentista", IsSystemDefault = true, Ativo = true },
            new ProfissaoModel { Id = 5, Descricao = "Médico", IsSystemDefault = true, Ativo = true }
        );

        modelBuilder.Entity<StatusModel>().HasData(
            new StatusModel { Id = 1, Cor = "#4B89DC", Legenda = "Agendamento realizado com sucesso!", Status = "Agendado", IsSystemDefault = true },
            new StatusModel { Id = 2, Cor = "#3498DB", Legenda = "Confirmado", Status = "Confirmado", IsSystemDefault = true },
            new StatusModel { Id = 3, Cor = "#5D9CEC", Legenda = "Em atendimento", Status = "Em atendimento", IsSystemDefault = true },
            new StatusModel { Id = 4, Cor = "#2ECC71", Legenda = "Concluído", Status = "Concluído", IsSystemDefault = true },
            new StatusModel { Id = 5, Cor = "#E74C3C", Legenda = "Cancelado pelo paciente", Status = "Cancelado pelo paciente", IsSystemDefault = true },
            new StatusModel { Id = 6, Cor = "#E57373", Legenda = "Cancelado pela clínica", Status = "Cancelado pela clínica", IsSystemDefault = true },
            new StatusModel { Id = 7, Cor = "#F9A825", Legenda = "Remarcado", Status = "Remarcado", IsSystemDefault = true },
            new StatusModel { Id = 8, Cor = "#E67E22", Legenda = "Não compareceu", Status = "Não compareceu", IsSystemDefault = true }
        );

        modelBuilder.Entity<FormaPagamentoModel>().HasData(
            new FormaPagamentoModel { Id = 1, Descricao = "Dinheiro", Parcelas = 1, IsSystemDefault = true },
            new FormaPagamentoModel { Id = 2, Descricao = "Cartão de Crédito", Parcelas = 1, IsSystemDefault = true },
            new FormaPagamentoModel { Id = 3, Descricao = "Cartão de Débito", Parcelas = 1, IsSystemDefault = true },
            new FormaPagamentoModel { Id = 4, Descricao = "Boleto", Parcelas = 1, IsSystemDefault = true },
            new FormaPagamentoModel { Id = 5, Descricao = "Pix", Parcelas = 1, IsSystemDefault = true },
            new FormaPagamentoModel { Id = 6, Descricao = "Depósito", Parcelas = 1, IsSystemDefault = true }
        );

        modelBuilder.Entity<TipoPagamentoModel>().HasData(
            new TipoPagamentoModel { Id = 1, Descricao = "À Vista", IsSystemDefault = true },
            new TipoPagamentoModel { Id = 2, Descricao = "Parcelado", IsSystemDefault = true },
            new TipoPagamentoModel { Id = 3, Descricao = "Convênio", IsSystemDefault = true },
            new TipoPagamentoModel { Id = 4, Descricao = "Recorrente", IsSystemDefault = true }
        );

        modelBuilder.Entity<SalaModel>().HasData(
            new SalaModel { Id = 1, Nome = "Principal", Capacidade = 10, Tipo = "Geral", local = "Principal", Status = true, IsSystemDefault = true }
        );

        modelBuilder.Entity<ConvenioModel>().HasData(
            new ConvenioModel { Id = 1, Nome = "Unimed", PeriodoCarencia = "0", Ativo = true, RegistroAvs = "ABC", Telefone = "343434-3434", Email = "email@email.com", IsSystemDefault = true }
        );


        modelBuilder.Entity<CentroCustoModel>().HasData(
            new CentroCustoModel { Id = 1, Descricao = "Geral - Receita", Tipo = "R", IsSystemDefault = true },
            new CentroCustoModel { Id = 2, Descricao = "Geral - Despesa", Tipo = "D", IsSystemDefault = true }
        );

        modelBuilder.Entity<PlanoContaModel>().HasData(
            new PlanoContaModel { Id = 1, Nome = "Geral - Ativo", Tipo = "A", Codigo = "1", IsSystemDefault = true },
            new PlanoContaModel { Id = 2, Nome = "Geral - Passívo", Tipo = "P", Codigo = "2", IsSystemDefault = true },
            new PlanoContaModel { Id = 3, Nome = "Geral - Receita", Tipo = "R", Codigo = "3", IsSystemDefault = true },
            new PlanoContaModel { Id = 4, Nome = "Geral - Despesa", Tipo = "D", Codigo = "4", IsSystemDefault = true }
        );

        modelBuilder.Entity<CategoriaModel>().HasData(
          new CategoriaModel { Id = 1, Nome = "Geral", IsSystemDefault = true, Ativo = true }
      );

        modelBuilder.Entity<PlanoModel>()
          .HasOne(p => p.Paciente)
          .WithOne(p => p.Plano)
          .HasForeignKey<PacienteModel>(p => p.PlanoId)
          .IsRequired(false);

        modelBuilder.Entity<FichaAvaliacaoModel>()
            .HasOne(f => f.Paciente)
            .WithOne(p => p.FichaAvaliacao)
            .HasForeignKey<FichaAvaliacaoModel>(f => f.PacienteId)
            .OnDelete(DeleteBehavior.Restrict); // Evita a exclusão em cascata

        // Configuração da relação 1:N entre Profissional e FichaAvaliacao
        modelBuilder.Entity<FichaAvaliacaoModel>()
            .HasOne(f => f.Profissional)
            .WithMany(p => p.FichasAvaliacao)
            .HasForeignKey(f => f.ProfissionalId)
            .OnDelete(DeleteBehavior.Restrict); // Evita a exclusão em cascata

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

        modelBuilder.Entity<Financ_ReceberModel>()
           .HasOne(f => f.TipoPagamento)
           .WithMany()
           .HasForeignKey(f => f.TipoPagamentoId);

        // Relacionamento: Financ_Pagar -> SubFinanc_Pagar
        modelBuilder.Entity<Financ_PagarModel>()
            .HasMany(f => f.subFinancPagar)
            .WithOne(s => s.FinancPagar)
            .HasForeignKey(s => s.financPagarId);

        modelBuilder.Entity<Financ_PagarModel>()
            .HasOne(s => s.TipoPagamento)
            .WithMany()
            .HasForeignKey(s => s.TipoPagamentoId);

        modelBuilder.Entity<Financ_PagarModel>()
          .HasOne(f => f.Fornecedor)
          .WithMany(fp => fp.Financ_Pagar)
          .HasForeignKey(s => s.FornecedorId);


        // Relacionamento: PlanoConta -> SubPlanoConta
        modelBuilder.Entity<PlanoContaModel>()
            .HasMany(f => f.SubPlanos)
            .WithOne(s => s.PlanoConta)
            .HasForeignKey(s => s.PlanoContaId);

        modelBuilder.Entity<DespesaFixaModel>(entity =>
        {
            entity.ToTable("DespesasFixas");

            // Outros relacionamentos da despesa fixa
            entity.HasOne(d => d.PlanoConta)
                .WithMany()
                .HasForeignKey(d => d.PlanoContaId);

            entity.HasOne(d => d.CentroCusto)
                .WithMany()
                .HasForeignKey(d => d.CentroCustoId);

            entity.HasOne(d => d.Fornecedor)
                .WithMany()
                .HasForeignKey(d => d.FornecedorId);

            entity.HasOne(d => d.TipoPagamento)
                .WithMany()
                .HasForeignKey(d => d.TipoPagamentoId);

            entity.HasOne(d => d.FormaPagamento)
                .WithMany()
                .HasForeignKey(d => d.FormaPagamentoId);
        });

        modelBuilder.Entity<Financ_PagarSubModel>(entity =>
        {
            entity.ToTable("Financ_PagarSub");

          // Relacionamento com o cabeçalho continua
            entity.HasOne(s => s.FinancPagar)
                .WithMany(f => f.subFinancPagar)
                .HasForeignKey(s => s.financPagarId);

            // Outros relacionamentos do sub item
            entity.HasOne(s => s.TipoPagamento)
                .WithMany()
                .HasForeignKey(s => s.TipoPagamentoId);

            entity.HasOne(s => s.FormaPagamento)
                .WithMany()
                .HasForeignKey(s => s.FormaPagamentoId);
        });

        modelBuilder.Entity<ProfissionalModel>()
             .HasOne(p => p.Conselho)
             .WithMany()  // Se ConselhoModel não tiver uma coleção de profissionais
             .HasForeignKey(p => p.ConselhoId)
             .IsRequired(false);  // Torna o relacionamento opcional

        // Configuração do relacionamento entre Profissional e Profissão (opcional)
        modelBuilder.Entity<ProfissionalModel>()
            .HasOne(p => p.Profissao)
            .WithMany()  // Se ProfissaoModel não tiver uma coleção de profissionais
            .HasForeignKey(p => p.ProfissaoId)
            .IsRequired(false);  // Torna o relacionamento opcional

        modelBuilder.Entity<SalaModel>()
            .HasMany(s => s.HorariosFuncionamento)
            .WithOne(h => h.Sala)
            .HasForeignKey(h => h.SalaId)
            .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<EmpresaModel>(entity =>
        {
            // Relacionamento com TipoPagamento (N:1)
            entity.HasOne(e => e.TipoPagamento)
                .WithMany()
                .HasForeignKey(e => e.TipoPagamentoId);

            // Relacionamento com Usuarios (1:N)
            entity.HasMany(e => e.Usuarios)
                .WithOne(u => u.Empresa)
                .HasForeignKey(u => u.EmpresaId);

            // Relacionamento com Filiais (1:N)
            entity.HasMany(e => e.Filiais)
                .WithOne(f => f.Empresa)
                .HasForeignKey(f => f.EmpresaId);

            // Relacionamento com Subscription (1:N)
            entity.HasMany(e => e.Subscription)
                .WithOne(s => s.Empresa)
                .HasForeignKey(s => s.EmpresaId);
        });

        modelBuilder.Entity<PaymentModel>(entity =>
        {
            // Relacionamento com Empresa (N:1)
            entity.HasOne(p => p.Empresa)
                .WithMany()
                .HasForeignKey(p => p.EmpresaId);

            // Relacionamento com Subscription (N:1)
            entity.HasOne(p => p.Subscription)
                .WithMany()
                .HasForeignKey(p => p.SubscriptionId)
                .IsRequired(false); // Permite que SubscriptionId seja nulo
        });

        modelBuilder.Entity<SubscriptionModel>(entity =>
        {
            // Relacionamento com Empresa (N:1)
            entity.HasOne(s => s.Empresa)
                .WithMany(e => e.Subscription)
                .HasForeignKey(s => s.EmpresaId);
        });

        modelBuilder.Entity<Financ_PagarModel>()
        .HasOne(f => f.DespesaFixa)
        .WithMany(d => d.FinancPagar)
        .HasForeignKey(f => f.DespesaFixaId)
        .OnDelete(DeleteBehavior.Cascade);

        // Configuração do relacionamento Sala -> Agenda (1:N)
        modelBuilder.Entity<AgendaModel>()
            .HasOne(a => a.Sala)
            .WithMany(s => s.Agendamentos)
            .HasForeignKey(a => a.SalaId)
            .OnDelete(DeleteBehavior.Restrict); // Impede delete em cascata

        modelBuilder.Entity<AgendaModel>()
      .HasOne(a => a.Paciente)
      .WithMany(p => p.Agendamentos)
      .HasForeignKey(a => a.PacienteId)
      .OnDelete(DeleteBehavior.Restrict);

        // Relacionamento Agenda -> Profissional
        modelBuilder.Entity<AgendaModel>()
            .HasOne(a => a.Profissional)
            .WithMany(p => p.Agendamentos)
            .HasForeignKey(a => a.ProfissionalId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ComissaoCalculadaModel>()
        .HasOne(c => c.Profissional)
        .WithMany(p => p.ComissoesCalculadas)
        .HasForeignKey(c => c.ProfissionalId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ComissaoCalculadaModel>()
         .HasOne(c => c.Agendamento)
         .WithOne(a => a.ComissaoCalculada)
         .HasForeignKey<ComissaoCalculadaModel>(c => c.AgendamentoId)
         .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<PacientePlanoHistoricoModel>()
        .HasOne(h => h.Paciente)
        .WithMany(p => p.HistoricoPlanos)
        .HasForeignKey(h => h.PacienteId)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<PacientePlanoHistoricoModel>()
         .HasOne(h => h.Plano)
         .WithMany(p => p.HistoricoPacientes)
         .HasForeignKey(h => h.PlanoId)
         .OnDelete(DeleteBehavior.Restrict);

        // Configurações de precisão para decimais
        modelBuilder.Entity<ComissaoCalculadaModel>()
            .Property(c => c.ValorComissao)
            .HasPrecision(18, 2);

        modelBuilder.Entity<ComissaoCalculadaModel>()
            .Property(c => c.ValorBase)
            .HasPrecision(18, 2);

        modelBuilder.Entity<ComissaoCalculadaModel>()
            .Property(c => c.PercentualOuValor)
            .HasPrecision(18, 2);

        modelBuilder.Entity<ProfissionalModel>()
            .Property(p => p.ValorComissao)
            .HasPrecision(18, 2);

        modelBuilder.Entity<PacientePlanoHistoricoModel>()
            .Property(h => h.ValorPago)
            .HasPrecision(18, 2);
    }
}
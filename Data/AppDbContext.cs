using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebApiSmartClinic.Helpers;
using Microsoft.Extensions.Options;
using WebApiSmartClinic.Dto.User;
using WebApiSmartClinic.Models.Abstractions;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata;

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

    public bool VerTodasEmpresas { get; set; }            // Admin = true
    public int? EmpresaSelecionada { get; set; }
    public bool EhAdmin { get; set; }
    public bool EhSupport { get; set; }
    public bool EhUser { get; set; }                      // Perfil User?
    public int? ProfissionalAtualId { get; set; }         // Profissional vinculado ao User
    public string? UsuarioAtualId { get; set; }           // Auditoria


    //public DbSet<AutorModel> Autores { get; set; }
    //public DbSet<LivroModel> Livros { get; set; }
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
    public DbSet<UsuarioEmpresaModel> UsuarioEmpresas { get; set; }
    public DbSet<FilialModel> Filiais { get; set; }
    public DbSet<SubscriptionModel> SubsCricao { get; set; }
    public DbSet<PaymentModel> Pagamentos { get; set; }
    public DbSet<ComissaoCalculadaModel> Comissoes { get; set; }
    public DbSet<PacientePlanoHistoricoModel> PacientePlanoHistoricos { get; set; }
    // Tipos a ignorar no filtro global (Identity, agregadores globais, etc.)
    private static readonly HashSet<string> _tiposIgnorados = new(StringComparer.Ordinal)
    {
        // Identity
        "Microsoft.AspNetCore.Identity.IdentityUser",
        "Microsoft.AspNetCore.Identity.IdentityRole",
        "Microsoft.AspNetCore.Identity.IdentityUserRole`2",
        "Microsoft.AspNetCore.Identity.IdentityUserClaim`1",
        "Microsoft.AspNetCore.Identity.IdentityUserLogin`1",
        "Microsoft.AspNetCore.Identity.IdentityRoleClaim`1",
        "Microsoft.AspNetCore.Identity.IdentityUserToken`1",

        // Entidades que NÃO devem sofrer filtro multi-empresa
        "WebApiSmartClinic.Models.EmpresaModel",
        "WebApiSmartClinic.Models.UsuarioEmpresaModel",
        // Adicione aqui catálogos “globais do tenant”, se existirem (ex.: TipoPagamentoModel global)
    };

    private static bool DeveIgnorar(IMutableEntityType et)
    {
        var t = et.ClrType;
        if (_tiposIgnorados.Contains(t.FullName!)) return true;

        var nomeTabela = et.GetTableName();
        if (!string.IsNullOrEmpty(nomeTabela) && nomeTabela.StartsWith("AspNet", StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    }

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

        modelBuilder.Entity<ProfissionalModel>(e =>
        {
            e.HasIndex(p => p.UsuarioId).IsUnique();

            // FK para AspNetUsers (User). OnDelete: Restrict para não apagar profissional ao remover usuário
            e.HasOne(p => p.Usuario)
             .WithMany()
             .HasForeignKey(p => p.UsuarioId)
             .OnDelete(DeleteBehavior.Restrict);

            e.Property(p => p.UsuarioId).HasMaxLength(450);
        });

        // Helper local: EF.Property<T>(e, "Nome")
        static MethodCallExpression EfProp<T>(ParameterExpression e, string nome) =>
            Expression.Call(typeof(EF), nameof(EF.Property), new[] { typeof(T) }, e, Expression.Constant(nome));

        foreach (var et in modelBuilder.Model.GetEntityTypes())
        {
            if (DeveIgnorar(et)) continue;

            var clr = et.ClrType;

            // Só aplicamos em tipos multi-empresa
            if (!typeof(IEntidadeEmpresa).IsAssignableFrom(clr))
                continue;

            // Param do lambda: (e) =>
            var e = Expression.Parameter(clr, "e");
            var thisDb = Expression.Constant(this);

            // (VerTodasEmpresas || (EmpresaSelecionada != null && e.EmpresaId == EmpresaSelecionada.Value))
            var verTodas = Expression.Property(thisDb, nameof(VerTodasEmpresas));
            var empSel = Expression.Property(thisDb, nameof(EmpresaSelecionada));
            var empSelNotNull = Expression.NotEqual(empSel, Expression.Constant(null, typeof(int?)));
            var empresaIdEqSel = Expression.Equal(EfProp<int>(e, nameof(IEntidadeEmpresa.EmpresaId)),
                                                  Expression.Property(empSel, "Value"));
            var condEmpresa = Expression.OrElse(verTodas, Expression.AndAlso(empSelNotNull, empresaIdEqSel));

            // && e.Ativo == true
            var condAtivo = Expression.Equal(EfProp<bool>(e, nameof(IEntidadeEmpresa.Ativo)), Expression.Constant(true));

            // Base: empresa + ativo
            Expression condicao = Expression.AndAlso(condEmpresa, condAtivo);

            if (typeof(IEntidadeDoProfissional).IsAssignableFrom(clr))
            {
                var ehUser = Expression.Property(thisDb, nameof(EhUser));
                var profSel = Expression.Property(thisDb, nameof(ProfissionalAtualId)); // int?
                var profNotNull = Expression.NotEqual(profSel, Expression.Constant(null, typeof(int?)));

                // ATENÇÃO: usar int? aqui
                var profProp = EfProp<int?>(e, nameof(IEntidadeDoProfissional.ProfissionalId));

                // Comparar nullable com nullable (null == X => false, do jeito que queremos)
                var profEq = Expression.Equal(profProp, profSel);

                // (!EhUser) || (ProfissionalAtualId != null && e.ProfissionalId == ProfissionalAtualId)
                var condProf = Expression.OrElse(Expression.Not(ehUser), Expression.AndAlso(profNotNull, profEq));

                condicao = Expression.AndAlso(condicao, condProf);
            }

            // Se a entidade também é “do profissional”, aplica corte extra quando EhUser == true:
            // && ( !EhUser || (ProfissionalAtualId != null && e.ProfissionalId == ProfissionalAtualId.Value) )
            //if (typeof(IEntidadeDoProfissional).IsAssignableFrom(clr))
            //{
            //    var ehUser = Expression.Property(thisDb, nameof(EhUser));
            //    var profSel = Expression.Property(thisDb, nameof(ProfissionalAtualId));
            //    var profNotNull = Expression.NotEqual(profSel, Expression.Constant(null, typeof(int?)));
            //    var profEq = Expression.Equal(EfProp<int>(e, nameof(IEntidadeDoProfissional.ProfissionalId)),
            //                                         Expression.Property(profSel, "Value"));
            //    var condProf = Expression.OrElse(Expression.Not(ehUser), Expression.AndAlso(profNotNull, profEq));

            //    condicao = Expression.AndAlso(condicao, condProf);
            //}

            // Lambda final: e => <condicao>
            var lambda = Expression.Lambda(condicao, e);

            // Aplica o filtro na entidade
            modelBuilder.Entity(clr).HasQueryFilter(lambda);
        }
    }


    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var agora = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries())
        {
            // Auditoria + Soft Delete
            if (entry.Entity is IEntidadeAuditavel aud)
            {
                if (entry.State == EntityState.Added)
                {
                    aud.DataCriacao = agora;
                    aud.UsuarioCriacaoId = UsuarioAtualId;

                    // Em criação, mantenha registros visíveis por padrão
                    if (entry.Entity is IEntidadeEmpresa emp && !emp.Ativo)
                        emp.Ativo = true;
                }
                else if (entry.State == EntityState.Modified)
                {
                    aud.DataAlteracao = agora;
                    aud.UsuarioAlteracaoId = UsuarioAtualId;
                }
                else if (entry.State == EntityState.Deleted)
                {
                    // Soft delete: marca inativo e converte para Modified
                    entry.State = EntityState.Modified;
                    aud.DataAlteracao = agora;
                    aud.UsuarioAlteracaoId = UsuarioAtualId;

                    if (entry.Entity is IEntidadeEmpresa emp)
                        emp.Ativo = false;
                }
            }

            // Regras multi-empresa
            if (entry.Entity is IEntidadeEmpresa eemp)
            {
                if (entry.State == EntityState.Added)
                {
                    // Support/User: força EmpresaSelecionada
                    if (!VerTodasEmpresas)
                    {
                        if (EmpresaSelecionada is null)
                            throw new InvalidOperationException("Empresa selecionada não definida para criação.");
                        eemp.EmpresaId = EmpresaSelecionada.Value;
                    }
                    // Admin: pode aceitar EmpresaId recebido (ou você pode validar aqui também)
                }
                else if (entry.State == EntityState.Modified)
                {
                    // Nunca permitir troca de empresa por update
                    entry.Property(nameof(IEntidadeEmpresa.EmpresaId)).IsModified = false;
                }
            }

            // Amarração por Profissional para perfil User
            if (EhUser && entry.Entity is IEntidadeDoProfissional dono)
            {
                if (ProfissionalAtualId is null)
                    throw new InvalidOperationException("Profissional do usuário não definido.");

                if (entry.State == EntityState.Added)
                {
                    // Se vier vazio, amarra ao próprio
                    if (!dono.ProfissionalId.HasValue || dono.ProfissionalId.Value == 0)
                        dono.ProfissionalId = ProfissionalAtualId;
                }
                else if (entry.State == EntityState.Modified)
                {
                    // Não deixa trocar o dono
                    entry.Property(nameof(IEntidadeDoProfissional.ProfissionalId)).IsModified = false;
                }
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }



        // Aplica filtro global para TODA entidade com EmpresaId
        //foreach (var et in modelBuilder.Model.GetEntityTypes())
        //{
        //    if (typeof(IEntidadeEmpresa).IsAssignableFrom(et.ClrType))
        //    {
        //        var metodo = typeof(AppDbContext)
        //            .GetMethod(nameof(AplicarFiltroEmpresa), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
        //            .MakeGenericMethod(et.ClrType);

        //        metodo.Invoke(this, new object[] { modelBuilder });
        //    }
        //}

    //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    //{
    //    var agora = DateTime.UtcNow;

    //    foreach (var entry in ChangeTracker.Entries<IEntidadeAuditavel>())
    //    {
    //        if (entry.State == EntityState.Added)
    //        {
    //            entry.Entity.DataCriacao = agora;
    //            entry.Entity.UsuarioCriacaoId = UsuarioAtualId;
    //            entry.Entity.Ativo = true;
    //        }
    //        else if (entry.State == EntityState.Modified)
    //        {
    //            entry.Entity.DataAlteracao = agora;
    //            entry.Entity.UsuarioAlteracaoId = UsuarioAtualId;
    //        }
    //    }

    //    foreach (var entry in ChangeTracker.Entries<IEntidadeEmpresa>())
    //    {
    //        if (entry.State == EntityState.Added)
    //        {
    //            // Quem não pode ver todas grava na empresa selecionada
    //            if (!VerTodasEmpresas && EmpresaSelecionada is not null)
    //                entry.Entity.EmpresaId = EmpresaSelecionada.Value;
    //        }

    //        // Protege contra troca indevida de EmpresaId em updates
    //        if (entry.State == EntityState.Modified)
    //        {
    //            entry.Property(x => x.EmpresaId).IsModified = false;
    //        }
    //    }

    //    return await base.SaveChangesAsync(cancellationToken);
    //}

    private void AplicarFiltroEmpresa<T>(ModelBuilder modelBuilder) where T : class, IEntidadeEmpresa
    {
        modelBuilder.Entity<T>().HasQueryFilter(e =>
            VerTodasEmpresas || (EmpresaSelecionada != null && e.EmpresaId == EmpresaSelecionada));
    }
}
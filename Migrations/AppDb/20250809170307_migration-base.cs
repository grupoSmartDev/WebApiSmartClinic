using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApiSmartClinic.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class migrationbase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserKey = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    ProfilePicture = table.Column<byte[]>(type: "bytea", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Autores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Sobrenome = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Banco",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NomeBanco = table.Column<string>(type: "text", nullable: false),
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    Agencia = table.Column<string>(type: "text", nullable: false),
                    NumeroConta = table.Column<string>(type: "text", nullable: false),
                    TipoConta = table.Column<string>(type: "text", nullable: false),
                    NomeTitular = table.Column<string>(type: "text", nullable: false),
                    DocumentoTitular = table.Column<string>(type: "text", nullable: false),
                    SaldoInicial = table.Column<decimal>(type: "numeric", nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    CodigoConvenio = table.Column<string>(type: "text", nullable: false),
                    Carteira = table.Column<string>(type: "text", nullable: false),
                    VariacaoCarteira = table.Column<string>(type: "text", nullable: false),
                    CodigoBeneficiario = table.Column<string>(type: "text", nullable: false),
                    NumeroContrato = table.Column<string>(type: "text", nullable: false),
                    CodigoTransmissao = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banco", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CentroCusto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Tipo = table.Column<string>(type: "text", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    IsSystemDefault = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentroCusto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Conselho",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Sigla = table.Column<string>(type: "text", nullable: false),
                    IsSystemDefault = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conselho", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Convenio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    RegistroAvs = table.Column<string>(type: "text", nullable: false),
                    PeriodoCarencia = table.Column<string>(type: "text", nullable: false),
                    Telefone = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    IsSystemDefault = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Convenio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormaPagamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Parcelas = table.Column<int>(type: "integer", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    IsSystemDefault = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormaPagamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fornecedor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Razao = table.Column<string>(type: "text", nullable: true),
                    Fantasia = table.Column<string>(type: "text", nullable: true),
                    Tipo = table.Column<string>(type: "text", nullable: true),
                    EstadoCivil = table.Column<string>(type: "text", nullable: true),
                    Sexo = table.Column<string>(type: "text", nullable: true),
                    IE = table.Column<string>(type: "text", nullable: true),
                    IM = table.Column<string>(type: "text", nullable: true),
                    CPF = table.Column<string>(type: "text", nullable: true),
                    CNPJ = table.Column<string>(type: "text", nullable: true),
                    Pais = table.Column<string>(type: "text", nullable: true),
                    UF = table.Column<string>(type: "text", nullable: true),
                    Cidade = table.Column<string>(type: "text", nullable: true),
                    Bairro = table.Column<string>(type: "text", nullable: true),
                    Complemento = table.Column<string>(type: "text", nullable: true),
                    Logradouro = table.Column<string>(type: "text", nullable: true),
                    NrLogradouro = table.Column<string>(type: "text", nullable: true),
                    CEP = table.Column<string>(type: "text", nullable: true),
                    Celular = table.Column<string>(type: "text", nullable: true),
                    TelefoneFixo = table.Column<string>(type: "text", nullable: true),
                    Banco = table.Column<string>(type: "text", nullable: true),
                    Agencia = table.Column<string>(type: "text", nullable: true),
                    Conta = table.Column<string>(type: "text", nullable: true),
                    TipoPIX = table.Column<string>(type: "text", nullable: true),
                    ChavePIX = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Nome = table.Column<string>(type: "text", nullable: true),
                    Observacao = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlanoConta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: false),
                    Inativo = table.Column<bool>(type: "boolean", nullable: true),
                    Observacao = table.Column<string>(type: "text", nullable: true),
                    IsSystemDefault = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanoConta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profissao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    IsSystemDefault = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profissao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecorrenciaPaciente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PacienteId = table.Column<int>(type: "integer", nullable: true),
                    DiaSemana = table.Column<int>(type: "integer", nullable: true),
                    HoraInicio = table.Column<string>(type: "text", nullable: true),
                    HoraFim = table.Column<string>(type: "text", nullable: true),
                    ProfissionalId = table.Column<int>(type: "integer", nullable: true),
                    SalaId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecorrenciaPaciente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sala",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Capacidade = table.Column<int>(type: "integer", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: true),
                    local = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    HorarioFincionamento = table.Column<string>(type: "text", nullable: true),
                    Observacao = table.Column<string>(type: "text", nullable: true),
                    IsSystemDefault = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sala", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Legenda = table.Column<string>(type: "text", nullable: false),
                    Cor = table.Column<string>(type: "text", nullable: false),
                    IsSystemDefault = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoPagamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descricao = table.Column<string>(type: "text", nullable: true),
                    IsSystemDefault = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPagamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Livros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Titulo = table.Column<string>(type: "text", nullable: false),
                    AutorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Livros_Autores_AutorId",
                        column: x => x.AutorId,
                        principalTable: "Autores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Boleto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NossoNumero = table.Column<string>(type: "text", nullable: false),
                    NumeroDocumento = table.Column<string>(type: "text", nullable: false),
                    Valor = table.Column<decimal>(type: "numeric", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Juros = table.Column<decimal>(type: "numeric", nullable: false),
                    Multa = table.Column<decimal>(type: "numeric", nullable: false),
                    NomeSacado = table.Column<string>(type: "text", nullable: false),
                    DocumentoSacado = table.Column<string>(type: "text", nullable: false),
                    BancoId = table.Column<int>(type: "integer", nullable: false),
                    Pago = table.Column<bool>(type: "boolean", nullable: false),
                    DataPagamento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CodigoDeBarras = table.Column<string>(type: "text", nullable: false),
                    LinhaDigitavel = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boleto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Boleto_Banco_BancoId",
                        column: x => x.BancoId,
                        principalTable: "Banco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Procedimento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    Preco = table.Column<decimal>(type: "numeric", nullable: false),
                    Duracao = table.Column<string>(type: "text", nullable: true),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    CategoriaId = table.Column<int>(type: "integer", nullable: true),
                    PercentualComissao = table.Column<decimal>(type: "numeric", nullable: true),
                    MateriaisNecessarios = table.Column<string>(type: "text", nullable: true),
                    CategoriaModelId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Procedimento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Procedimento_Categoria_CategoriaModelId",
                        column: x => x.CategoriaModelId,
                        principalTable: "Categoria",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SubCentroCusto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    CentroCustoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCentroCusto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCentroCusto_CentroCusto_CentroCustoId",
                        column: x => x.CentroCustoId,
                        principalTable: "CentroCusto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlanoContaSub",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlanoContaId = table.Column<int>(type: "integer", nullable: false),
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanoContaSub", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanoContaSub_PlanoConta_PlanoContaId",
                        column: x => x.PlanoContaId,
                        principalTable: "PlanoConta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profissional",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Sobrenome = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    Cpf = table.Column<string>(type: "text", nullable: true),
                    Celular = table.Column<string>(type: "text", nullable: false),
                    Sexo = table.Column<string>(type: "text", nullable: true),
                    ConselhoId = table.Column<int>(type: "integer", nullable: true),
                    RegistroConselho = table.Column<string>(type: "text", nullable: true),
                    UfConselho = table.Column<string>(type: "text", nullable: true),
                    ProfissaoId = table.Column<int>(type: "integer", nullable: true),
                    Cbo = table.Column<string>(type: "text", nullable: true),
                    Rqe = table.Column<string>(type: "text", nullable: true),
                    Cnes = table.Column<string>(type: "text", nullable: true),
                    TipoPagamento = table.Column<string>(type: "text", nullable: true),
                    ChavePix = table.Column<string>(type: "text", nullable: true),
                    BancoNome = table.Column<string>(type: "text", nullable: true),
                    BancoAgencia = table.Column<string>(type: "text", nullable: true),
                    BancoConta = table.Column<string>(type: "text", nullable: true),
                    BancoTipoConta = table.Column<string>(type: "text", nullable: true),
                    BancoCpfTitular = table.Column<string>(type: "text", nullable: true),
                    EhUsuario = table.Column<bool>(type: "boolean", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profissional", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profissional_Conselho_ConselhoId",
                        column: x => x.ConselhoId,
                        principalTable: "Conselho",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Profissional_Profissao_ProfissaoId",
                        column: x => x.ProfissaoId,
                        principalTable: "Profissao",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SalaHorario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SalaId = table.Column<int>(type: "integer", nullable: false),
                    DiaSemana = table.Column<int>(type: "integer", nullable: false),
                    HoraInicio = table.Column<TimeSpan>(type: "interval", nullable: false),
                    HoraFim = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaHorario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalaHorario_Sala_SalaId",
                        column: x => x.SalaId,
                        principalTable: "Sala",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DespesasFixas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descricao = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Valor = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    DiaVencimento = table.Column<int>(type: "integer", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DataFim = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Ativo = table.Column<bool>(type: "boolean", nullable: true),
                    Frequencia = table.Column<int>(type: "integer", nullable: false),
                    FornecedorId = table.Column<int>(type: "integer", nullable: true),
                    PlanoContaId = table.Column<int>(type: "integer", nullable: true),
                    CentroCustoId = table.Column<int>(type: "integer", nullable: true),
                    TipoPagamentoId = table.Column<int>(type: "integer", nullable: true),
                    FormaPagamentoId = table.Column<int>(type: "integer", nullable: true),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DespesasFixas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DespesasFixas_CentroCusto_CentroCustoId",
                        column: x => x.CentroCustoId,
                        principalTable: "CentroCusto",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DespesasFixas_FormaPagamento_FormaPagamentoId",
                        column: x => x.FormaPagamentoId,
                        principalTable: "FormaPagamento",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DespesasFixas_Fornecedor_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DespesasFixas_PlanoConta_PlanoContaId",
                        column: x => x.PlanoContaId,
                        principalTable: "PlanoConta",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DespesasFixas_TipoPagamento_TipoPagamentoId",
                        column: x => x.TipoPagamentoId,
                        principalTable: "TipoPagamento",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DataInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DataFim = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DataInicioTeste = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DataNascimentoTitular = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    TitularCPF = table.Column<string>(type: "text", nullable: false),
                    CNPJEmpresaMatriz = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Celular = table.Column<string>(type: "text", nullable: false),
                    Sobrenome = table.Column<string>(type: "text", nullable: false),
                    Especialidade = table.Column<string>(type: "text", nullable: false),
                    PlanoEscolhido = table.Column<string>(type: "text", nullable: false),
                    TelefoneFixo = table.Column<string>(type: "text", nullable: true),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    PeriodoTeste = table.Column<bool>(type: "boolean", nullable: false),
                    CelularComWhatsApp = table.Column<bool>(type: "boolean", nullable: false),
                    ReceberNotificacoes = table.Column<bool>(type: "boolean", nullable: false),
                    QtdeLicencaEmpresaPermitida = table.Column<int>(type: "integer", nullable: false),
                    QtdeLicencaUsuarioPermitida = table.Column<int>(type: "integer", nullable: false),
                    QtdeLicencaEmpresaUtilizada = table.Column<int>(type: "integer", nullable: false),
                    QtdeLicencaUsuarioUtilizada = table.Column<int>(type: "integer", nullable: false),
                    QtdeLicencaFilialPermitida = table.Column<int>(type: "integer", nullable: false),
                    QtdeLicencaFilialUtilizada = table.Column<int>(type: "integer", nullable: false),
                    StripeCustomerId = table.Column<string>(type: "text", nullable: true),
                    TipoPagamentoId = table.Column<int>(type: "integer", nullable: false),
                    DatabaseConnectionString = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Empresas_TipoPagamento_TipoPagamentoId",
                        column: x => x.TipoPagamentoId,
                        principalTable: "TipoPagamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comissao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProfissionalId = table.Column<int>(type: "integer", nullable: false),
                    ProcedimentoId = table.Column<int>(type: "integer", nullable: false),
                    DataAtendimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ValorProcedimento = table.Column<decimal>(type: "numeric", nullable: false),
                    PercentualComissao = table.Column<decimal>(type: "numeric", nullable: false),
                    ValorComissao = table.Column<decimal>(type: "numeric", nullable: false),
                    Pago = table.Column<bool>(type: "boolean", nullable: false),
                    DataPagamento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comissao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comissao_Procedimento_ProcedimentoId",
                        column: x => x.ProcedimentoId,
                        principalTable: "Procedimento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comissao_Profissional_ProfissionalId",
                        column: x => x.ProfissionalId,
                        principalTable: "Profissional",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Filiais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    CNPJ = table.Column<string>(type: "text", nullable: true),
                    Endereco = table.Column<string>(type: "text", nullable: true),
                    Telefone = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    EmpresaId = table.Column<int>(type: "integer", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filiais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Filiais_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubsCricao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmpresaId = table.Column<int>(type: "integer", nullable: false),
                    StripeSubscriptionId = table.Column<string>(type: "text", nullable: false),
                    StripePriceId = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CurrentPeriodStart = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CurrentPeriodEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Currency = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubsCricao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubsCricao_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Senha = table.Column<string>(type: "text", nullable: false),
                    Permissao = table.Column<int[]>(type: "integer[]", nullable: false),
                    CPF = table.Column<string>(type: "text", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    ProfissionalId = table.Column<int>(type: "integer", nullable: true),
                    EmpresaId = table.Column<int>(type: "integer", nullable: false),
                    FilialId = table.Column<int>(type: "integer", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "text", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Usuario_Filiais_FilialId",
                        column: x => x.FilialId,
                        principalTable: "Filiais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Usuario_Profissional_ProfissionalId",
                        column: x => x.ProfissionalId,
                        principalTable: "Profissional",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Pagamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmpresaId = table.Column<int>(type: "integer", nullable: false),
                    SubscriptionId = table.Column<int>(type: "integer", nullable: true),
                    StripePaymentIntentId = table.Column<string>(type: "text", nullable: false),
                    StripeInvoiceId = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Currency = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pagamentos_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pagamentos_SubsCricao_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "SubsCricao",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HistoricoTransacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BancoId = table.Column<int>(type: "integer", nullable: false),
                    DataTransacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TipoTransacao = table.Column<string>(type: "text", nullable: false),
                    Valor = table.Column<decimal>(type: "numeric", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    Referencia = table.Column<string>(type: "text", nullable: false),
                    UsuarioId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoTransacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricoTransacao_Banco_BancoId",
                        column: x => x.BancoId,
                        principalTable: "Banco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistoricoTransacao_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LogUsuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdMovimentacao = table.Column<int>(type: "integer", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    Rotina = table.Column<string>(type: "text", nullable: false),
                    UsuarioId = table.Column<string>(type: "text", nullable: false),
                    DataMovimentacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogUsuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogUsuario_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Agenda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Titulo = table.Column<string>(type: "text", nullable: false),
                    Data = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DataFimRecorrencia = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    HoraInicio = table.Column<TimeSpan>(type: "interval", nullable: false),
                    HoraFim = table.Column<TimeSpan>(type: "interval", nullable: false),
                    PacienteId = table.Column<int>(type: "integer", nullable: true),
                    ProfissionalId = table.Column<int>(type: "integer", nullable: true),
                    Convenio = table.Column<bool>(type: "boolean", nullable: false),
                    Valor = table.Column<decimal>(type: "numeric", nullable: true),
                    FormaPagamento = table.Column<string>(type: "text", nullable: true),
                    Pago = table.Column<bool>(type: "boolean", nullable: false),
                    FinancReceberId = table.Column<int>(type: "integer", nullable: true),
                    SalaId = table.Column<int>(type: "integer", nullable: true),
                    PacoteId = table.Column<int>(type: "integer", nullable: true),
                    LembreteSms = table.Column<bool>(type: "boolean", nullable: false),
                    LembreteWhatsapp = table.Column<bool>(type: "boolean", nullable: false),
                    LembreteEmail = table.Column<bool>(type: "boolean", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: true),
                    IntegracaoGmail = table.Column<bool>(type: "boolean", nullable: false),
                    Avulso = table.Column<bool>(type: "boolean", nullable: false),
                    StatusFinal = table.Column<bool>(type: "boolean", nullable: false),
                    DiasRecorrencia = table.Column<int[]>(type: "integer[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agenda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agenda_Profissional_ProfissionalId",
                        column: x => x.ProfissionalId,
                        principalTable: "Profissional",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Agenda_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Atividade",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Titulo = table.Column<string>(type: "text", nullable: true),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    Tempo = table.Column<string>(type: "text", nullable: true),
                    EvolucaoId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atividade", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Evolucoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Observacao = table.Column<string>(type: "text", nullable: false),
                    DataEvolucao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PacienteId = table.Column<int>(type: "integer", nullable: true),
                    ProfissionalId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evolucoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exercicio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    Obs = table.Column<string>(type: "text", nullable: true),
                    Peso = table.Column<int>(type: "integer", nullable: true),
                    Tempo = table.Column<int>(type: "integer", nullable: true),
                    Repeticoes = table.Column<int>(type: "integer", nullable: true),
                    Series = table.Column<int>(type: "integer", nullable: true),
                    EvolucaoId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercicio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercicio_Evolucoes_EvolucaoId",
                        column: x => x.EvolucaoId,
                        principalTable: "Evolucoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FichaAvaliacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PacienteId = table.Column<int>(type: "integer", nullable: true),
                    DataAvaliacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Especialidade = table.Column<string>(type: "text", nullable: true),
                    Idade = table.Column<int>(type: "integer", nullable: true),
                    Altura = table.Column<double>(type: "double precision", nullable: true),
                    Peso = table.Column<double>(type: "double precision", nullable: true),
                    Sexo = table.Column<string>(type: "text", nullable: true),
                    ObservacoesGerais = table.Column<string>(type: "text", nullable: true),
                    HistoricoDoencas = table.Column<bool>(type: "boolean", nullable: false),
                    DoencasPreExistentes = table.Column<string>(type: "text", nullable: true),
                    MedicacaoUsoContinuo = table.Column<bool>(type: "boolean", nullable: false),
                    Medicacao = table.Column<string>(type: "text", nullable: true),
                    CirurgiasPrevias = table.Column<bool>(type: "boolean", nullable: false),
                    DetalheCirurgias = table.Column<string>(type: "text", nullable: true),
                    Alergias = table.Column<string>(type: "text", nullable: true),
                    QueixaPrincipal = table.Column<string>(type: "text", nullable: true),
                    ObjetivosDoTratamento = table.Column<string>(type: "text", nullable: true),
                    IMC = table.Column<double>(type: "double precision", nullable: true),
                    AvaliacaoPostural = table.Column<string>(type: "text", nullable: true),
                    AmplitudeMovimento = table.Column<string>(type: "text", nullable: true),
                    AssinaturaProfissional = table.Column<string>(type: "text", nullable: true),
                    AssinaturaCliente = table.Column<string>(type: "text", nullable: true),
                    HistoriaPregressa = table.Column<string>(type: "text", nullable: true),
                    HistoriaAtual = table.Column<string>(type: "text", nullable: true),
                    TipoDor = table.Column<string>(type: "text", nullable: true),
                    SinaisVitais = table.Column<string>(type: "text", nullable: true),
                    DoencasCronicas = table.Column<string>(type: "text", nullable: true),
                    Cirurgia = table.Column<string>(type: "text", nullable: true),
                    DoencaNeurodegenerativa = table.Column<string>(type: "text", nullable: true),
                    TratamentosRealizados = table.Column<string>(type: "text", nullable: true),
                    AlergiaMedicamentos = table.Column<string>(type: "text", nullable: true),
                    FrequenciaConsumoAlcool = table.Column<string>(type: "text", nullable: true),
                    PraticaAtividade = table.Column<bool>(type: "boolean", nullable: false),
                    Tabagista = table.Column<bool>(type: "boolean", nullable: false),
                    ProfissionalId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FichaAvaliacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FichaAvaliacao_Profissional_ProfissionalId",
                        column: x => x.ProfissionalId,
                        principalTable: "Profissional",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Financ_Pagar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdOrigem = table.Column<int>(type: "integer", nullable: true),
                    NrDocto = table.Column<int>(type: "integer", nullable: true),
                    DataEmissao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ValorOriginal = table.Column<decimal>(type: "numeric", nullable: true),
                    ValorPago = table.Column<decimal>(type: "numeric", nullable: true),
                    Valor = table.Column<decimal>(type: "numeric", nullable: true),
                    Parcela = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    NotaFiscal = table.Column<string>(type: "text", nullable: true),
                    Descricao = table.Column<string>(type: "text", nullable: true),
                    Classificacao = table.Column<string>(type: "text", nullable: true),
                    Observacao = table.Column<string>(type: "text", nullable: true),
                    PacienteId = table.Column<int>(type: "integer", nullable: true),
                    FornecedorId = table.Column<int>(type: "integer", nullable: true),
                    CentroCustoId = table.Column<int>(type: "integer", nullable: true),
                    BancoId = table.Column<int>(type: "integer", nullable: true),
                    TipoPagamentoId = table.Column<int>(type: "integer", nullable: true),
                    PlanoContaId = table.Column<int>(type: "integer", nullable: true),
                    DespesaFixaId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Financ_Pagar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Financ_Pagar_Banco_BancoId",
                        column: x => x.BancoId,
                        principalTable: "Banco",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Financ_Pagar_CentroCusto_CentroCustoId",
                        column: x => x.CentroCustoId,
                        principalTable: "CentroCusto",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Financ_Pagar_DespesasFixas_DespesaFixaId",
                        column: x => x.DespesaFixaId,
                        principalTable: "DespesasFixas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Financ_Pagar_Fornecedor_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Financ_Pagar_PlanoConta_PlanoContaId",
                        column: x => x.PlanoContaId,
                        principalTable: "PlanoConta",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Financ_Pagar_TipoPagamento_TipoPagamentoId",
                        column: x => x.TipoPagamentoId,
                        principalTable: "TipoPagamento",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Financ_PagarSub",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    financPagarId = table.Column<int>(type: "integer", nullable: true),
                    Parcela = table.Column<int>(type: "integer", nullable: true),
                    Valor = table.Column<decimal>(type: "numeric", nullable: false),
                    ValorPago = table.Column<decimal>(type: "numeric", nullable: true),
                    Desconto = table.Column<decimal>(type: "numeric", nullable: true),
                    Juros = table.Column<decimal>(type: "numeric", nullable: true),
                    Multa = table.Column<decimal>(type: "numeric", nullable: true),
                    TipoPagamentoId = table.Column<int>(type: "integer", nullable: true),
                    FormaPagamentoId = table.Column<int>(type: "integer", nullable: true),
                    DataPagamento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DataVencimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Observacao = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Financ_PagarSub", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Financ_PagarSub_Financ_Pagar_financPagarId",
                        column: x => x.financPagarId,
                        principalTable: "Financ_Pagar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Financ_PagarSub_FormaPagamento_FormaPagamentoId",
                        column: x => x.FormaPagamentoId,
                        principalTable: "FormaPagamento",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Financ_PagarSub_TipoPagamento_TipoPagamentoId",
                        column: x => x.TipoPagamentoId,
                        principalTable: "TipoPagamento",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Financ_Receber",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdOrigem = table.Column<int>(type: "integer", nullable: true),
                    NrDocto = table.Column<int>(type: "integer", nullable: true),
                    DataEmissao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ValorOriginal = table.Column<decimal>(type: "numeric", nullable: true),
                    ValorPago = table.Column<decimal>(type: "numeric", nullable: true),
                    Valor = table.Column<decimal>(type: "numeric", nullable: true),
                    Parcela = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    NotaFiscal = table.Column<string>(type: "text", nullable: true),
                    Descricao = table.Column<string>(type: "text", nullable: true),
                    Classificacao = table.Column<string>(type: "text", nullable: true),
                    Observacao = table.Column<string>(type: "text", nullable: true),
                    PacienteId = table.Column<int>(type: "integer", nullable: true),
                    FornecedorId = table.Column<int>(type: "integer", nullable: true),
                    CentroCustoId = table.Column<int>(type: "integer", nullable: true),
                    BancoId = table.Column<int>(type: "integer", nullable: true),
                    TipoPagamentoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Financ_Receber", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Financ_Receber_Banco_BancoId",
                        column: x => x.BancoId,
                        principalTable: "Banco",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Financ_Receber_CentroCusto_CentroCustoId",
                        column: x => x.CentroCustoId,
                        principalTable: "CentroCusto",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Financ_Receber_Fornecedor_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Financ_Receber_TipoPagamento_TipoPagamentoId",
                        column: x => x.TipoPagamentoId,
                        principalTable: "TipoPagamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Financ_ReceberSub",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    financReceberId = table.Column<int>(type: "integer", nullable: true),
                    Parcela = table.Column<int>(type: "integer", nullable: true),
                    Valor = table.Column<decimal>(type: "numeric", nullable: false),
                    ValorPago = table.Column<decimal>(type: "numeric", nullable: false),
                    Desconto = table.Column<decimal>(type: "numeric", nullable: true),
                    Juros = table.Column<decimal>(type: "numeric", nullable: true),
                    Multa = table.Column<decimal>(type: "numeric", nullable: true),
                    TipoPagamentoId = table.Column<int>(type: "integer", nullable: true),
                    FormaPagamentoId = table.Column<int>(type: "integer", nullable: true),
                    DataPagamento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DataVencimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Observacao = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Financ_ReceberSub", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Financ_ReceberSub_Financ_Receber_financReceberId",
                        column: x => x.financReceberId,
                        principalTable: "Financ_Receber",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Financ_ReceberSub_FormaPagamento_FormaPagamentoId",
                        column: x => x.FormaPagamentoId,
                        principalTable: "FormaPagamento",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Financ_ReceberSub_TipoPagamento_TipoPagamentoId",
                        column: x => x.TipoPagamentoId,
                        principalTable: "TipoPagamento",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Plano",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descricao = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    TempoMinutos = table.Column<int>(type: "integer", nullable: false),
                    DiasSemana = table.Column<int>(type: "integer", nullable: false),
                    CentroCustoId = table.Column<int>(type: "integer", nullable: true),
                    ValorBimestral = table.Column<decimal>(type: "numeric", nullable: true),
                    ValorTrimestral = table.Column<decimal>(type: "numeric", nullable: true),
                    ValorQuadrimestral = table.Column<decimal>(type: "numeric", nullable: true),
                    ValorSemestral = table.Column<decimal>(type: "numeric", nullable: true),
                    ValorAnual = table.Column<decimal>(type: "numeric", nullable: true),
                    ValorMensal = table.Column<decimal>(type: "numeric", nullable: true),
                    DataInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DataFim = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    PacienteId = table.Column<int>(type: "integer", nullable: true),
                    FinanceiroId = table.Column<int>(type: "integer", nullable: true),
                    TipoMes = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plano", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plano_Financ_Receber_FinanceiroId",
                        column: x => x.FinanceiroId,
                        principalTable: "Financ_Receber",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Paciente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Bairro = table.Column<string>(type: "text", nullable: true),
                    BreveDiagnostico = table.Column<string>(type: "text", nullable: true),
                    Celular = table.Column<string>(type: "text", nullable: true),
                    Cep = table.Column<string>(type: "text", nullable: true),
                    Cidade = table.Column<string>(type: "text", nullable: true),
                    ComoConheceu = table.Column<string>(type: "text", nullable: true),
                    Complemento = table.Column<string>(type: "text", nullable: true),
                    ConvenioId = table.Column<int>(type: "integer", nullable: true),
                    Cpf = table.Column<string>(type: "text", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Uf = table.Column<string>(type: "text", nullable: true),
                    EstadoCivil = table.Column<string>(type: "text", nullable: true),
                    Logradouro = table.Column<string>(type: "text", nullable: true),
                    Medicamento = table.Column<string>(type: "text", nullable: true),
                    ProfissionalId = table.Column<int>(type: "integer", nullable: true),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Numero = table.Column<string>(type: "text", nullable: true),
                    Pais = table.Column<string>(type: "text", nullable: true),
                    PermitirLembretes = table.Column<bool>(type: "boolean", nullable: false),
                    PreferenciaDeContato = table.Column<string>(type: "text", nullable: true),
                    Profissao = table.Column<string>(type: "text", nullable: true),
                    Responsavel = table.Column<bool>(type: "boolean", nullable: false),
                    Rg = table.Column<string>(type: "text", nullable: true),
                    Sexo = table.Column<string>(type: "text", nullable: true),
                    Telefone = table.Column<string>(type: "text", nullable: true),
                    PlanoId = table.Column<int>(type: "integer", nullable: true),
                    DataUltimoAtendimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FichaAvaliacaoId = table.Column<int>(type: "integer", nullable: true),
                    DataFimRecorrencia = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paciente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Paciente_Convenio_ConvenioId",
                        column: x => x.ConvenioId,
                        principalTable: "Convenio",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Paciente_Plano_PlanoId",
                        column: x => x.PlanoId,
                        principalTable: "Plano",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Paciente_Profissional_ProfissionalId",
                        column: x => x.ProfissionalId,
                        principalTable: "Profissional",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RecorrenciaPacienteDto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PacienteId = table.Column<int>(type: "integer", nullable: true),
                    DiaSemana = table.Column<int>(type: "integer", nullable: false),
                    HoraInicio = table.Column<string>(type: "text", nullable: true),
                    HoraFim = table.Column<string>(type: "text", nullable: true),
                    ProfissionalId = table.Column<int>(type: "integer", nullable: true),
                    SalaId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecorrenciaPacienteDto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecorrenciaPacienteDto_Paciente_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Paciente",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", null, "User", "USER" },
                    { "2", null, "Support", "SUPPORT" },
                    { "3", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "CentroCusto",
                columns: new[] { "Id", "Descricao", "IsSystemDefault", "Tipo" },
                values: new object[,]
                {
                    { 1, "Geral - Receita", true, "R" },
                    { 2, "Geral - Despesa", true, "D" }
                });

            migrationBuilder.InsertData(
                table: "Conselho",
                columns: new[] { "Id", "IsSystemDefault", "Nome", "Sigla" },
                values: new object[,]
                {
                    { 1, true, "Conselho Regional de Fisioterapia e Terapia Ocupacional", "CREFITO" },
                    { 2, true, "Conselho Federal de Psicologia", "CFP" }
                });

            migrationBuilder.InsertData(
                table: "Convenio",
                columns: new[] { "Id", "Ativo", "Email", "IsSystemDefault", "Nome", "PeriodoCarencia", "RegistroAvs", "Telefone" },
                values: new object[] { 1, true, "email@email.com", true, "Unimed", "0", "ABC", "343434-3434" });

            migrationBuilder.InsertData(
                table: "FormaPagamento",
                columns: new[] { "Id", "Descricao", "IsSystemDefault", "Parcelas" },
                values: new object[,]
                {
                    { 1, "Dinheiro", true, 1 },
                    { 2, "Cartão de Crédito", true, 1 },
                    { 3, "Cartão de Débito", true, 1 },
                    { 4, "Boleto", true, 1 },
                    { 5, "Pix", true, 1 },
                    { 6, "Depósito", true, 1 }
                });

            migrationBuilder.InsertData(
                table: "PlanoConta",
                columns: new[] { "Id", "Codigo", "Inativo", "IsSystemDefault", "Nome", "Observacao", "Tipo" },
                values: new object[,]
                {
                    { 1, "1", false, true, "Geral - Ativo", null, "A" },
                    { 2, "2", false, true, "Geral - Passívo", null, "P" },
                    { 3, "3", false, true, "Geral - Receita", null, "R" },
                    { 4, "4", false, true, "Geral - Despesa", null, "D" }
                });

            migrationBuilder.InsertData(
                table: "Profissao",
                columns: new[] { "Id", "Descricao", "IsSystemDefault" },
                values: new object[,]
                {
                    { 1, "Administrador(a)", true },
                    { 2, "Psicólogo(a)", true },
                    { 3, "Fisioterapeuta", true },
                    { 4, "Dentista", true },
                    { 5, "Médico", true }
                });

            migrationBuilder.InsertData(
                table: "Sala",
                columns: new[] { "Id", "Capacidade", "HorarioFincionamento", "IsSystemDefault", "Nome", "Observacao", "Status", "Tipo", "local" },
                values: new object[] { 1, 10, null, true, "Principal", null, true, "Geral", "Principal" });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "Id", "Cor", "IsSystemDefault", "Legenda", "Status" },
                values: new object[,]
                {
                    { 1, "#4B89DC", true, "Agendamento realizado com sucesso!", "Agendado" },
                    { 2, "#3498DB", true, "Confirmado", "Confirmado" },
                    { 3, "#5D9CEC", true, "Em atendimento", "Em atendimento" },
                    { 4, "#2ECC71", true, "Concluído", "Concluído" },
                    { 5, "#E74C3C", true, "Cancelado pelo paciente", "Cancelado pelo paciente" },
                    { 6, "#E57373", true, "Cancelado pela clínica", "Cancelado pela clínica" },
                    { 7, "#F9A825", true, "Remarcado", "Remarcado" },
                    { 8, "#E67E22", true, "Não compareceu", "Não compareceu" }
                });

            migrationBuilder.InsertData(
                table: "TipoPagamento",
                columns: new[] { "Id", "Descricao", "IsSystemDefault" },
                values: new object[,]
                {
                    { 1, "À Vista", true },
                    { 2, "Parcelado", true },
                    { 3, "Convênio", true },
                    { 4, "Recorrente", true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agenda_FinancReceberId",
                table: "Agenda",
                column: "FinancReceberId");

            migrationBuilder.CreateIndex(
                name: "IX_Agenda_PacienteId",
                table: "Agenda",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Agenda_PacoteId",
                table: "Agenda",
                column: "PacoteId");

            migrationBuilder.CreateIndex(
                name: "IX_Agenda_ProfissionalId",
                table: "Agenda",
                column: "ProfissionalId");

            migrationBuilder.CreateIndex(
                name: "IX_Agenda_StatusId",
                table: "Agenda",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Atividade_EvolucaoId",
                table: "Atividade",
                column: "EvolucaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Boleto_BancoId",
                table: "Boleto",
                column: "BancoId");

            migrationBuilder.CreateIndex(
                name: "IX_Comissao_ProcedimentoId",
                table: "Comissao",
                column: "ProcedimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Comissao_ProfissionalId",
                table: "Comissao",
                column: "ProfissionalId");

            migrationBuilder.CreateIndex(
                name: "IX_DespesasFixas_CentroCustoId",
                table: "DespesasFixas",
                column: "CentroCustoId");

            migrationBuilder.CreateIndex(
                name: "IX_DespesasFixas_FormaPagamentoId",
                table: "DespesasFixas",
                column: "FormaPagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_DespesasFixas_FornecedorId",
                table: "DespesasFixas",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_DespesasFixas_PlanoContaId",
                table: "DespesasFixas",
                column: "PlanoContaId");

            migrationBuilder.CreateIndex(
                name: "IX_DespesasFixas_TipoPagamentoId",
                table: "DespesasFixas",
                column: "TipoPagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_TipoPagamentoId",
                table: "Empresas",
                column: "TipoPagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Evolucoes_PacienteId",
                table: "Evolucoes",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercicio_EvolucaoId",
                table: "Exercicio",
                column: "EvolucaoId");

            migrationBuilder.CreateIndex(
                name: "IX_FichaAvaliacao_PacienteId",
                table: "FichaAvaliacao",
                column: "PacienteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FichaAvaliacao_ProfissionalId",
                table: "FichaAvaliacao",
                column: "ProfissionalId");

            migrationBuilder.CreateIndex(
                name: "IX_Filiais_EmpresaId",
                table: "Filiais",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Financ_Pagar_BancoId",
                table: "Financ_Pagar",
                column: "BancoId");

            migrationBuilder.CreateIndex(
                name: "IX_Financ_Pagar_CentroCustoId",
                table: "Financ_Pagar",
                column: "CentroCustoId");

            migrationBuilder.CreateIndex(
                name: "IX_Financ_Pagar_DespesaFixaId",
                table: "Financ_Pagar",
                column: "DespesaFixaId");

            migrationBuilder.CreateIndex(
                name: "IX_Financ_Pagar_FornecedorId",
                table: "Financ_Pagar",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Financ_Pagar_PacienteId",
                table: "Financ_Pagar",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Financ_Pagar_PlanoContaId",
                table: "Financ_Pagar",
                column: "PlanoContaId");

            migrationBuilder.CreateIndex(
                name: "IX_Financ_Pagar_TipoPagamentoId",
                table: "Financ_Pagar",
                column: "TipoPagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Financ_PagarSub_financPagarId",
                table: "Financ_PagarSub",
                column: "financPagarId");

            migrationBuilder.CreateIndex(
                name: "IX_Financ_PagarSub_FormaPagamentoId",
                table: "Financ_PagarSub",
                column: "FormaPagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Financ_PagarSub_TipoPagamentoId",
                table: "Financ_PagarSub",
                column: "TipoPagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Financ_Receber_BancoId",
                table: "Financ_Receber",
                column: "BancoId");

            migrationBuilder.CreateIndex(
                name: "IX_Financ_Receber_CentroCustoId",
                table: "Financ_Receber",
                column: "CentroCustoId");

            migrationBuilder.CreateIndex(
                name: "IX_Financ_Receber_FornecedorId",
                table: "Financ_Receber",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Financ_Receber_PacienteId",
                table: "Financ_Receber",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Financ_Receber_TipoPagamentoId",
                table: "Financ_Receber",
                column: "TipoPagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Financ_ReceberSub_financReceberId",
                table: "Financ_ReceberSub",
                column: "financReceberId");

            migrationBuilder.CreateIndex(
                name: "IX_Financ_ReceberSub_FormaPagamentoId",
                table: "Financ_ReceberSub",
                column: "FormaPagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Financ_ReceberSub_TipoPagamentoId",
                table: "Financ_ReceberSub",
                column: "TipoPagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoTransacao_BancoId",
                table: "HistoricoTransacao",
                column: "BancoId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoTransacao_UsuarioId",
                table: "HistoricoTransacao",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Livros_AutorId",
                table: "Livros",
                column: "AutorId");

            migrationBuilder.CreateIndex(
                name: "IX_LogUsuario_UsuarioId",
                table: "LogUsuario",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_ConvenioId",
                table: "Paciente",
                column: "ConvenioId");

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_PlanoId",
                table: "Paciente",
                column: "PlanoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_ProfissionalId",
                table: "Paciente",
                column: "ProfissionalId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamentos_EmpresaId",
                table: "Pagamentos",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamentos_SubscriptionId",
                table: "Pagamentos",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Plano_FinanceiroId",
                table: "Plano",
                column: "FinanceiroId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanoContaSub_PlanoContaId",
                table: "PlanoContaSub",
                column: "PlanoContaId");

            migrationBuilder.CreateIndex(
                name: "IX_Procedimento_CategoriaModelId",
                table: "Procedimento",
                column: "CategoriaModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Profissional_ConselhoId",
                table: "Profissional",
                column: "ConselhoId");

            migrationBuilder.CreateIndex(
                name: "IX_Profissional_ProfissaoId",
                table: "Profissional",
                column: "ProfissaoId");

            migrationBuilder.CreateIndex(
                name: "IX_RecorrenciaPacienteDto_PacienteId",
                table: "RecorrenciaPacienteDto",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaHorario_SalaId",
                table: "SalaHorario",
                column: "SalaId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCentroCusto_CentroCustoId",
                table: "SubCentroCusto",
                column: "CentroCustoId");

            migrationBuilder.CreateIndex(
                name: "IX_SubsCricao_EmpresaId",
                table: "SubsCricao",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_EmpresaId",
                table: "Usuario",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_FilialId",
                table: "Usuario",
                column: "FilialId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_ProfissionalId",
                table: "Usuario",
                column: "ProfissionalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agenda_Financ_Receber_FinancReceberId",
                table: "Agenda",
                column: "FinancReceberId",
                principalTable: "Financ_Receber",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Agenda_Paciente_PacienteId",
                table: "Agenda",
                column: "PacienteId",
                principalTable: "Paciente",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Agenda_Paciente_PacoteId",
                table: "Agenda",
                column: "PacoteId",
                principalTable: "Paciente",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Atividade_Evolucoes_EvolucaoId",
                table: "Atividade",
                column: "EvolucaoId",
                principalTable: "Evolucoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evolucoes_Paciente_PacienteId",
                table: "Evolucoes",
                column: "PacienteId",
                principalTable: "Paciente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FichaAvaliacao_Paciente_PacienteId",
                table: "FichaAvaliacao",
                column: "PacienteId",
                principalTable: "Paciente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Financ_Pagar_Paciente_PacienteId",
                table: "Financ_Pagar",
                column: "PacienteId",
                principalTable: "Paciente",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Financ_Receber_Paciente_PacienteId",
                table: "Financ_Receber",
                column: "PacienteId",
                principalTable: "Paciente",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plano_Financ_Receber_FinanceiroId",
                table: "Plano");

            migrationBuilder.DropTable(
                name: "Agenda");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Atividade");

            migrationBuilder.DropTable(
                name: "Boleto");

            migrationBuilder.DropTable(
                name: "Comissao");

            migrationBuilder.DropTable(
                name: "Exercicio");

            migrationBuilder.DropTable(
                name: "FichaAvaliacao");

            migrationBuilder.DropTable(
                name: "Financ_PagarSub");

            migrationBuilder.DropTable(
                name: "Financ_ReceberSub");

            migrationBuilder.DropTable(
                name: "HistoricoTransacao");

            migrationBuilder.DropTable(
                name: "Livros");

            migrationBuilder.DropTable(
                name: "LogUsuario");

            migrationBuilder.DropTable(
                name: "Pagamentos");

            migrationBuilder.DropTable(
                name: "PlanoContaSub");

            migrationBuilder.DropTable(
                name: "RecorrenciaPaciente");

            migrationBuilder.DropTable(
                name: "RecorrenciaPacienteDto");

            migrationBuilder.DropTable(
                name: "SalaHorario");

            migrationBuilder.DropTable(
                name: "SubCentroCusto");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Procedimento");

            migrationBuilder.DropTable(
                name: "Evolucoes");

            migrationBuilder.DropTable(
                name: "Financ_Pagar");

            migrationBuilder.DropTable(
                name: "Autores");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "SubsCricao");

            migrationBuilder.DropTable(
                name: "Sala");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "DespesasFixas");

            migrationBuilder.DropTable(
                name: "Filiais");

            migrationBuilder.DropTable(
                name: "FormaPagamento");

            migrationBuilder.DropTable(
                name: "PlanoConta");

            migrationBuilder.DropTable(
                name: "Empresas");

            migrationBuilder.DropTable(
                name: "Financ_Receber");

            migrationBuilder.DropTable(
                name: "Banco");

            migrationBuilder.DropTable(
                name: "CentroCusto");

            migrationBuilder.DropTable(
                name: "Fornecedor");

            migrationBuilder.DropTable(
                name: "Paciente");

            migrationBuilder.DropTable(
                name: "TipoPagamento");

            migrationBuilder.DropTable(
                name: "Convenio");

            migrationBuilder.DropTable(
                name: "Plano");

            migrationBuilder.DropTable(
                name: "Profissional");

            migrationBuilder.DropTable(
                name: "Conselho");

            migrationBuilder.DropTable(
                name: "Profissao");
        }
    }
}

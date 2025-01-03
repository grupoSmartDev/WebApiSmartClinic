using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSmartClinic.Migrations.MasterDb
{
    /// <inheritdoc />
    public partial class inicitalCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Identificador = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DatabaseConnectionString = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfissionalModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Celular = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sexo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConselhoId = table.Column<int>(type: "int", nullable: true),
                    RegistroConselho = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UfConselho = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfissaoId = table.Column<int>(type: "int", nullable: true),
                    Cbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rqe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cnes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoPagamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChavePix = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BancoNome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BancoAgencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BancoConta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BancoTipoConta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BancoCpfTitular = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EhUsuario = table.Column<bool>(type: "bit", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfissionalModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Permissao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    ProfissionalId = table.Column<int>(type: "int", nullable: true),
                    EmpresaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioModel_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioModel_ProfissionalModel_ProfissionalId",
                        column: x => x.ProfissionalId,
                        principalTable: "ProfissionalModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_Identificador",
                table: "Empresas",
                column: "Identificador",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioModel_EmpresaId",
                table: "UsuarioModel",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioModel_ProfissionalId",
                table: "UsuarioModel",
                column: "ProfissionalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioModel");

            migrationBuilder.DropTable(
                name: "Empresas");

            migrationBuilder.DropTable(
                name: "ProfissionalModel");
        }
    }
}

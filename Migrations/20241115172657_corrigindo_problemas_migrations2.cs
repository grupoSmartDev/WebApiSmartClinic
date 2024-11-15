using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSmartClinic.Migrations
{
    /// <inheritdoc />
    public partial class corrigindo_problemas_migrations2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paciente_Plano_PlanoId1",
                table: "Paciente");

            migrationBuilder.DropForeignKey(
                name: "FK_Paciente_Profissional_ProfissionalId1",
                table: "Paciente");

            migrationBuilder.DropForeignKey(
                name: "FK_Plano_Profissional_ProfissionalId",
                table: "Plano");

            migrationBuilder.DropForeignKey(
                name: "FK_Plano_Sala_SalaId",
                table: "Plano");

            migrationBuilder.DropForeignKey(
                name: "FK_Procedimento_Categoria_CategoriaId",
                table: "Procedimento");

            migrationBuilder.DropIndex(
                name: "IX_Procedimento_CategoriaId",
                table: "Procedimento");

            migrationBuilder.DropIndex(
                name: "IX_Plano_ProfissionalId",
                table: "Plano");

            migrationBuilder.DropIndex(
                name: "IX_Plano_SalaId",
                table: "Plano");

            migrationBuilder.DropIndex(
                name: "IX_Paciente_PlanoId1",
                table: "Paciente");

            migrationBuilder.DropIndex(
                name: "IX_Paciente_ProfissionalId1",
                table: "Paciente");

            migrationBuilder.DropColumn(
                name: "PlanoGratuito",
                table: "Plano");

            migrationBuilder.DropColumn(
                name: "ProfissionalId",
                table: "Plano");

            migrationBuilder.DropColumn(
                name: "SalaId",
                table: "Plano");

            migrationBuilder.DropColumn(
                name: "TipoCobranca",
                table: "Plano");

            migrationBuilder.DropColumn(
                name: "PlanoId1",
                table: "Paciente");

            migrationBuilder.DropColumn(
                name: "ProfissionalId1",
                table: "Paciente");

            migrationBuilder.AlterColumn<string>(
                name: "Duracao",
                table: "Procedimento",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<int>(
                name: "CategoriaId",
                table: "Procedimento",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CategoriaModelId",
                table: "Procedimento",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Uf",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Sexo",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Rg",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "ProfissionalId",
                table: "Paciente",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Profissao",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PreferenciaDeContato",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "PlanoId",
                table: "Paciente",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Pais",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Numero",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Medicamento",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Logradouro",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "EstadoCivil",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Complemento",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ComoConheceu",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Cidade",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Cep",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Celular",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "BreveDiagnostico",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Bairro",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Procedimento_CategoriaModelId",
                table: "Procedimento",
                column: "CategoriaModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_PlanoId",
                table: "Paciente",
                column: "PlanoId");

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_ProfissionalId",
                table: "Paciente",
                column: "ProfissionalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Paciente_Plano_PlanoId",
                table: "Paciente",
                column: "PlanoId",
                principalTable: "Plano",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Paciente_Profissional_ProfissionalId",
                table: "Paciente",
                column: "ProfissionalId",
                principalTable: "Profissional",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Procedimento_Categoria_CategoriaModelId",
                table: "Procedimento",
                column: "CategoriaModelId",
                principalTable: "Categoria",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paciente_Plano_PlanoId",
                table: "Paciente");

            migrationBuilder.DropForeignKey(
                name: "FK_Paciente_Profissional_ProfissionalId",
                table: "Paciente");

            migrationBuilder.DropForeignKey(
                name: "FK_Procedimento_Categoria_CategoriaModelId",
                table: "Procedimento");

            migrationBuilder.DropIndex(
                name: "IX_Procedimento_CategoriaModelId",
                table: "Procedimento");

            migrationBuilder.DropIndex(
                name: "IX_Paciente_PlanoId",
                table: "Paciente");

            migrationBuilder.DropIndex(
                name: "IX_Paciente_ProfissionalId",
                table: "Paciente");

            migrationBuilder.DropColumn(
                name: "CategoriaModelId",
                table: "Procedimento");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Duracao",
                table: "Procedimento",
                type: "time",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "CategoriaId",
                table: "Procedimento",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PlanoGratuito",
                table: "Plano",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ProfissionalId",
                table: "Plano",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SalaId",
                table: "Plano",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoCobranca",
                table: "Plano",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Uf",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Sexo",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Rg",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProfissionalId",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Profissao",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PreferenciaDeContato",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PlanoId",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Pais",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Numero",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Medicamento",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Logradouro",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EstadoCivil",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Complemento",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ComoConheceu",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cidade",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cep",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Celular",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BreveDiagnostico",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Bairro",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlanoId1",
                table: "Paciente",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfissionalId1",
                table: "Paciente",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Procedimento_CategoriaId",
                table: "Procedimento",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Plano_ProfissionalId",
                table: "Plano",
                column: "ProfissionalId");

            migrationBuilder.CreateIndex(
                name: "IX_Plano_SalaId",
                table: "Plano",
                column: "SalaId");

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_PlanoId1",
                table: "Paciente",
                column: "PlanoId1");

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_ProfissionalId1",
                table: "Paciente",
                column: "ProfissionalId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Paciente_Plano_PlanoId1",
                table: "Paciente",
                column: "PlanoId1",
                principalTable: "Plano",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Paciente_Profissional_ProfissionalId1",
                table: "Paciente",
                column: "ProfissionalId1",
                principalTable: "Profissional",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Plano_Profissional_ProfissionalId",
                table: "Plano",
                column: "ProfissionalId",
                principalTable: "Profissional",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Plano_Sala_SalaId",
                table: "Plano",
                column: "SalaId",
                principalTable: "Sala",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Procedimento_Categoria_CategoriaId",
                table: "Procedimento",
                column: "CategoriaId",
                principalTable: "Categoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

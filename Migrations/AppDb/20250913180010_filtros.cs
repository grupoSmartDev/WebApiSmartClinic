using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApiSmartClinic.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class filtros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Livros");

            migrationBuilder.DropTable(
                name: "Autores");

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "TipoPagamento",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "TipoPagamento",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "TipoPagamento",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "TipoPagamento",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioAlteracaoId",
                table: "TipoPagamento",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCriacaoId",
                table: "TipoPagamento",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "SubCentroCusto",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "SubCentroCusto",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "SubCentroCusto",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "SubCentroCusto",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioAlteracaoId",
                table: "SubCentroCusto",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCriacaoId",
                table: "SubCentroCusto",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Status",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Status",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Status",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Status",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioAlteracaoId",
                table: "Status",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCriacaoId",
                table: "Status",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "SalaHorario",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "SalaHorario",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "SalaHorario",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioAlteracaoId",
                table: "SalaHorario",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCriacaoId",
                table: "SalaHorario",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Sala",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Sala",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Sala",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioAlteracaoId",
                table: "Sala",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCriacaoId",
                table: "Sala",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Profissional",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Profissional",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Profissional",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioAlteracaoId",
                table: "Profissional",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCriacaoId",
                table: "Profissional",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Profissao",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Profissao",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Profissao",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioAlteracaoId",
                table: "Profissao",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCriacaoId",
                table: "Profissao",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Procedimento",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Procedimento",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Procedimento",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioAlteracaoId",
                table: "Procedimento",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCriacaoId",
                table: "Procedimento",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "PlanoContaSub",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "PlanoContaSub",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "PlanoContaSub",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "PlanoContaSub",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioAlteracaoId",
                table: "PlanoContaSub",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCriacaoId",
                table: "PlanoContaSub",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "PlanoConta",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "PlanoConta",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "PlanoConta",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "PlanoConta",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioAlteracaoId",
                table: "PlanoConta",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCriacaoId",
                table: "PlanoConta",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Plano",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Plano",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Plano",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioAlteracaoId",
                table: "Plano",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCriacaoId",
                table: "Plano",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "PacientePlanoHistoricos",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "PacientePlanoHistoricos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioAlteracaoId",
                table: "PacientePlanoHistoricos",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCriacaoId",
                table: "PacientePlanoHistoricos",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Paciente",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Paciente",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioAlteracaoId",
                table: "Paciente",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCriacaoId",
                table: "Paciente",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "LogUsuario",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "LogUsuario",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "LogUsuario",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "LogUsuario",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioAlteracaoId",
                table: "LogUsuario",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCriacaoId",
                table: "LogUsuario",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "HistoricoTransacao",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "HistoricoTransacao",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "HistoricoTransacao",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "HistoricoTransacao",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioAlteracaoId",
                table: "HistoricoTransacao",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCriacaoId",
                table: "HistoricoTransacao",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Fornecedor",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Fornecedor",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioAlteracaoId",
                table: "Fornecedor",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCriacaoId",
                table: "Fornecedor",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "FormaPagamento",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "FormaPagamento",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "FormaPagamento",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "FormaPagamento",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioAlteracaoId",
                table: "FormaPagamento",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCriacaoId",
                table: "FormaPagamento",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Financ_Receber",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Financ_Receber",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Financ_Receber",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Financ_Receber",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioAlteracaoId",
                table: "Financ_Receber",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCriacaoId",
                table: "Financ_Receber",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Financ_Pagar",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Financ_Pagar",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Financ_Pagar",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Financ_Pagar",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioAlteracaoId",
                table: "Financ_Pagar",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCriacaoId",
                table: "Financ_Pagar",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Filiais",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioAlteracaoId",
                table: "Filiais",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCriacaoId",
                table: "Filiais",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "FichaAvaliacao",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "FichaAvaliacao",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "FichaAvaliacao",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "FichaAvaliacao",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioAlteracaoId",
                table: "FichaAvaliacao",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCriacaoId",
                table: "FichaAvaliacao",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Exercicio",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Exercicio",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Exercicio",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Exercicio",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioAlteracaoId",
                table: "Exercicio",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCriacaoId",
                table: "Exercicio",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Evolucoes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Evolucoes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Evolucoes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Evolucoes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioAlteracaoId",
                table: "Evolucoes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCriacaoId",
                table: "Evolucoes",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PeriodoCobranca",
                table: "Empresas",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "AsaasSubscriptionId",
                table: "Empresas",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "AsaasCustomerId",
                table: "Empresas",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAlteracao",
                table: "DespesasFixas",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<bool>(
                name: "Ativo",
                table: "DespesasFixas",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "DespesasFixas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "DespesasFixas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioAlteracaoId",
                table: "DespesasFixas",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCriacaoId",
                table: "DespesasFixas",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Convenio",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Convenio",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Convenio",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioAlteracaoId",
                table: "Convenio",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCriacaoId",
                table: "Convenio",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Conselho",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Conselho",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Conselho",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Conselho",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioAlteracaoId",
                table: "Conselho",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCriacaoId",
                table: "Conselho",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Comissoes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Comissoes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Comissoes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioAlteracaoId",
                table: "Comissoes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCriacaoId",
                table: "Comissoes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Comissao",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Comissao",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Comissao",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Comissao",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioAlteracaoId",
                table: "Comissao",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCriacaoId",
                table: "Comissao",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "CentroCusto",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "CentroCusto",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "CentroCusto",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "CentroCusto",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioAlteracaoId",
                table: "CentroCusto",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCriacaoId",
                table: "CentroCusto",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Categoria",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Categoria",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Categoria",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Categoria",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioAlteracaoId",
                table: "Categoria",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCriacaoId",
                table: "Categoria",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Boleto",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Boleto",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Boleto",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Boleto",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioAlteracaoId",
                table: "Boleto",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCriacaoId",
                table: "Boleto",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Banco",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Banco",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Banco",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioAlteracaoId",
                table: "Banco",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCriacaoId",
                table: "Banco",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Atividade",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Atividade",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Atividade",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Atividade",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioAlteracaoId",
                table: "Atividade",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCriacaoId",
                table: "Atividade",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Agenda",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioAlteracaoId",
                table: "Agenda",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCriacaoId",
                table: "Agenda",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UsuarioEmpresas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<string>(type: "text", nullable: false),
                    EmpresaId = table.Column<int>(type: "integer", nullable: false),
                    PodeLer = table.Column<bool>(type: "boolean", nullable: false),
                    PodeEscrever = table.Column<bool>(type: "boolean", nullable: false),
                    PodeExcluir = table.Column<bool>(type: "boolean", nullable: false),
                    EmpresaPadrao = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioEmpresas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioEmpresas_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioEmpresas_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "CentroCusto",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Ativo", "DataAlteracao", "DataCriacao", "EmpresaId", "UsuarioAlteracaoId", "UsuarioCriacaoId" },
                values: new object[] { false, null, new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(2061), 0, null, null });

            migrationBuilder.UpdateData(
                table: "CentroCusto",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Ativo", "DataAlteracao", "DataCriacao", "EmpresaId", "UsuarioAlteracaoId", "UsuarioCriacaoId" },
                values: new object[] { false, null, new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(2070), 0, null, null });

            migrationBuilder.UpdateData(
                table: "Conselho",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Ativo", "DataAlteracao", "DataCriacao", "EmpresaId", "UsuarioAlteracaoId", "UsuarioCriacaoId" },
                values: new object[] { false, null, new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1494), 0, null, null });

            migrationBuilder.UpdateData(
                table: "Conselho",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Ativo", "DataAlteracao", "DataCriacao", "EmpresaId", "UsuarioAlteracaoId", "UsuarioCriacaoId" },
                values: new object[] { false, null, new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1498), 0, null, null });

            migrationBuilder.UpdateData(
                table: "Convenio",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DataAlteracao", "DataCriacao", "EmpresaId", "UsuarioAlteracaoId", "UsuarioCriacaoId" },
                values: new object[] { null, new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(2024), 0, null, null });

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Ativo", "DataAlteracao", "DataCriacao", "EmpresaId", "UsuarioAlteracaoId", "UsuarioCriacaoId" },
                values: new object[] { false, null, new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1870), 0, null, null });

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Ativo", "DataAlteracao", "DataCriacao", "EmpresaId", "UsuarioAlteracaoId", "UsuarioCriacaoId" },
                values: new object[] { false, null, new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1872), 0, null, null });

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Ativo", "DataAlteracao", "DataCriacao", "EmpresaId", "UsuarioAlteracaoId", "UsuarioCriacaoId" },
                values: new object[] { false, null, new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1873), 0, null, null });

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Ativo", "DataAlteracao", "DataCriacao", "EmpresaId", "UsuarioAlteracaoId", "UsuarioCriacaoId" },
                values: new object[] { false, null, new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1874), 0, null, null });

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Ativo", "DataAlteracao", "DataCriacao", "EmpresaId", "UsuarioAlteracaoId", "UsuarioCriacaoId" },
                values: new object[] { false, null, new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1875), 0, null, null });

            migrationBuilder.UpdateData(
                table: "FormaPagamento",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Ativo", "DataAlteracao", "DataCriacao", "EmpresaId", "UsuarioAlteracaoId", "UsuarioCriacaoId" },
                values: new object[] { false, null, new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1876), 0, null, null });

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Ativo", "DataAlteracao", "DataCriacao", "EmpresaId", "UsuarioAlteracaoId", "UsuarioCriacaoId" },
                values: new object[] { false, null, new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(2117), 0, null, null });

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Ativo", "DataAlteracao", "DataCriacao", "EmpresaId", "UsuarioAlteracaoId", "UsuarioCriacaoId" },
                values: new object[] { false, null, new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(2122), 0, null, null });

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Ativo", "DataAlteracao", "DataCriacao", "EmpresaId", "UsuarioAlteracaoId", "UsuarioCriacaoId" },
                values: new object[] { false, null, new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(2123), 0, null, null });

            migrationBuilder.UpdateData(
                table: "PlanoConta",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Ativo", "DataAlteracao", "DataCriacao", "EmpresaId", "UsuarioAlteracaoId", "UsuarioCriacaoId" },
                values: new object[] { false, null, new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(2124), 0, null, null });

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DataAlteracao", "DataCriacao", "EmpresaId", "UsuarioAlteracaoId", "UsuarioCriacaoId" },
                values: new object[] { null, new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1694), 0, null, null });

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DataAlteracao", "DataCriacao", "EmpresaId", "UsuarioAlteracaoId", "UsuarioCriacaoId" },
                values: new object[] { null, new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1696), 0, null, null });

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DataAlteracao", "DataCriacao", "EmpresaId", "UsuarioAlteracaoId", "UsuarioCriacaoId" },
                values: new object[] { null, new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1697), 0, null, null });

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DataAlteracao", "DataCriacao", "EmpresaId", "UsuarioAlteracaoId", "UsuarioCriacaoId" },
                values: new object[] { null, new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1698), 0, null, null });

            migrationBuilder.UpdateData(
                table: "Profissao",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DataAlteracao", "DataCriacao", "EmpresaId", "UsuarioAlteracaoId", "UsuarioCriacaoId" },
                values: new object[] { null, new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1699), 0, null, null });

            migrationBuilder.UpdateData(
                table: "Sala",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Ativo", "DataCriacao", "EmpresaId", "UsuarioAlteracaoId", "UsuarioCriacaoId" },
                values: new object[] { false, new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1976), 0, null, null });

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Ativo", "DataAlteracao", "DataCriacao", "EmpresaId", "UsuarioAlteracaoId", "UsuarioCriacaoId" },
                values: new object[] { false, null, new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1747), 0, null, null });

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Ativo", "DataAlteracao", "DataCriacao", "EmpresaId", "UsuarioAlteracaoId", "UsuarioCriacaoId" },
                values: new object[] { false, null, new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1749), 0, null, null });

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Ativo", "DataAlteracao", "DataCriacao", "EmpresaId", "UsuarioAlteracaoId", "UsuarioCriacaoId" },
                values: new object[] { false, null, new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1750), 0, null, null });

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Ativo", "DataAlteracao", "DataCriacao", "EmpresaId", "UsuarioAlteracaoId", "UsuarioCriacaoId" },
                values: new object[] { false, null, new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1751), 0, null, null });

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Ativo", "DataAlteracao", "DataCriacao", "EmpresaId", "UsuarioAlteracaoId", "UsuarioCriacaoId" },
                values: new object[] { false, null, new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1753), 0, null, null });

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Ativo", "DataAlteracao", "DataCriacao", "EmpresaId", "UsuarioAlteracaoId", "UsuarioCriacaoId" },
                values: new object[] { false, null, new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1755), 0, null, null });

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Ativo", "DataAlteracao", "DataCriacao", "EmpresaId", "UsuarioAlteracaoId", "UsuarioCriacaoId" },
                values: new object[] { false, null, new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1756), 0, null, null });

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Ativo", "DataAlteracao", "DataCriacao", "EmpresaId", "UsuarioAlteracaoId", "UsuarioCriacaoId" },
                values: new object[] { false, null, new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1757), 0, null, null });

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Ativo", "DataAlteracao", "DataCriacao", "EmpresaId", "UsuarioAlteracaoId", "UsuarioCriacaoId" },
                values: new object[] { false, null, new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1930), 0, null, null });

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Ativo", "DataAlteracao", "DataCriacao", "EmpresaId", "UsuarioAlteracaoId", "UsuarioCriacaoId" },
                values: new object[] { false, null, new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1932), 0, null, null });

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Ativo", "DataAlteracao", "DataCriacao", "EmpresaId", "UsuarioAlteracaoId", "UsuarioCriacaoId" },
                values: new object[] { false, null, new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1933), 0, null, null });

            migrationBuilder.UpdateData(
                table: "TipoPagamento",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Ativo", "DataAlteracao", "DataCriacao", "EmpresaId", "UsuarioAlteracaoId", "UsuarioCriacaoId" },
                values: new object[] { false, null, new DateTime(2025, 9, 13, 18, 0, 9, 605, DateTimeKind.Utc).AddTicks(1934), 0, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioEmpresas_EmpresaId",
                table: "UsuarioEmpresas",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioEmpresas_UsuarioId",
                table: "UsuarioEmpresas",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioEmpresas");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "TipoPagamento");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "TipoPagamento");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "TipoPagamento");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "TipoPagamento");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracaoId",
                table: "TipoPagamento");

            migrationBuilder.DropColumn(
                name: "UsuarioCriacaoId",
                table: "TipoPagamento");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "SubCentroCusto");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "SubCentroCusto");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "SubCentroCusto");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "SubCentroCusto");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracaoId",
                table: "SubCentroCusto");

            migrationBuilder.DropColumn(
                name: "UsuarioCriacaoId",
                table: "SubCentroCusto");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracaoId",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "UsuarioCriacaoId",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "SalaHorario");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "SalaHorario");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "SalaHorario");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracaoId",
                table: "SalaHorario");

            migrationBuilder.DropColumn(
                name: "UsuarioCriacaoId",
                table: "SalaHorario");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Sala");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Sala");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Sala");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracaoId",
                table: "Sala");

            migrationBuilder.DropColumn(
                name: "UsuarioCriacaoId",
                table: "Sala");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Profissional");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Profissional");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Profissional");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracaoId",
                table: "Profissional");

            migrationBuilder.DropColumn(
                name: "UsuarioCriacaoId",
                table: "Profissional");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Profissao");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Profissao");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Profissao");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracaoId",
                table: "Profissao");

            migrationBuilder.DropColumn(
                name: "UsuarioCriacaoId",
                table: "Profissao");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Procedimento");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Procedimento");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Procedimento");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracaoId",
                table: "Procedimento");

            migrationBuilder.DropColumn(
                name: "UsuarioCriacaoId",
                table: "Procedimento");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "PlanoContaSub");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "PlanoContaSub");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "PlanoContaSub");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "PlanoContaSub");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracaoId",
                table: "PlanoContaSub");

            migrationBuilder.DropColumn(
                name: "UsuarioCriacaoId",
                table: "PlanoContaSub");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "PlanoConta");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "PlanoConta");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "PlanoConta");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "PlanoConta");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracaoId",
                table: "PlanoConta");

            migrationBuilder.DropColumn(
                name: "UsuarioCriacaoId",
                table: "PlanoConta");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Plano");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Plano");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Plano");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracaoId",
                table: "Plano");

            migrationBuilder.DropColumn(
                name: "UsuarioCriacaoId",
                table: "Plano");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "PacientePlanoHistoricos");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "PacientePlanoHistoricos");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracaoId",
                table: "PacientePlanoHistoricos");

            migrationBuilder.DropColumn(
                name: "UsuarioCriacaoId",
                table: "PacientePlanoHistoricos");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Paciente");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Paciente");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracaoId",
                table: "Paciente");

            migrationBuilder.DropColumn(
                name: "UsuarioCriacaoId",
                table: "Paciente");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "LogUsuario");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "LogUsuario");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "LogUsuario");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "LogUsuario");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracaoId",
                table: "LogUsuario");

            migrationBuilder.DropColumn(
                name: "UsuarioCriacaoId",
                table: "LogUsuario");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "HistoricoTransacao");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "HistoricoTransacao");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "HistoricoTransacao");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "HistoricoTransacao");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracaoId",
                table: "HistoricoTransacao");

            migrationBuilder.DropColumn(
                name: "UsuarioCriacaoId",
                table: "HistoricoTransacao");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracaoId",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "UsuarioCriacaoId",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "FormaPagamento");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "FormaPagamento");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "FormaPagamento");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "FormaPagamento");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracaoId",
                table: "FormaPagamento");

            migrationBuilder.DropColumn(
                name: "UsuarioCriacaoId",
                table: "FormaPagamento");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Financ_Receber");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Financ_Receber");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Financ_Receber");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Financ_Receber");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracaoId",
                table: "Financ_Receber");

            migrationBuilder.DropColumn(
                name: "UsuarioCriacaoId",
                table: "Financ_Receber");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Financ_Pagar");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Financ_Pagar");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Financ_Pagar");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Financ_Pagar");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracaoId",
                table: "Financ_Pagar");

            migrationBuilder.DropColumn(
                name: "UsuarioCriacaoId",
                table: "Financ_Pagar");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Filiais");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracaoId",
                table: "Filiais");

            migrationBuilder.DropColumn(
                name: "UsuarioCriacaoId",
                table: "Filiais");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "FichaAvaliacao");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "FichaAvaliacao");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "FichaAvaliacao");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "FichaAvaliacao");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracaoId",
                table: "FichaAvaliacao");

            migrationBuilder.DropColumn(
                name: "UsuarioCriacaoId",
                table: "FichaAvaliacao");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Exercicio");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Exercicio");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Exercicio");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Exercicio");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracaoId",
                table: "Exercicio");

            migrationBuilder.DropColumn(
                name: "UsuarioCriacaoId",
                table: "Exercicio");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Evolucoes");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Evolucoes");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Evolucoes");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Evolucoes");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracaoId",
                table: "Evolucoes");

            migrationBuilder.DropColumn(
                name: "UsuarioCriacaoId",
                table: "Evolucoes");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "DespesasFixas");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "DespesasFixas");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracaoId",
                table: "DespesasFixas");

            migrationBuilder.DropColumn(
                name: "UsuarioCriacaoId",
                table: "DespesasFixas");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Convenio");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Convenio");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Convenio");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracaoId",
                table: "Convenio");

            migrationBuilder.DropColumn(
                name: "UsuarioCriacaoId",
                table: "Convenio");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Conselho");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Conselho");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Conselho");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Conselho");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracaoId",
                table: "Conselho");

            migrationBuilder.DropColumn(
                name: "UsuarioCriacaoId",
                table: "Conselho");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Comissoes");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Comissoes");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Comissoes");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracaoId",
                table: "Comissoes");

            migrationBuilder.DropColumn(
                name: "UsuarioCriacaoId",
                table: "Comissoes");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Comissao");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Comissao");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Comissao");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Comissao");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracaoId",
                table: "Comissao");

            migrationBuilder.DropColumn(
                name: "UsuarioCriacaoId",
                table: "Comissao");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "CentroCusto");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "CentroCusto");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "CentroCusto");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "CentroCusto");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracaoId",
                table: "CentroCusto");

            migrationBuilder.DropColumn(
                name: "UsuarioCriacaoId",
                table: "CentroCusto");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Categoria");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Categoria");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Categoria");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Categoria");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracaoId",
                table: "Categoria");

            migrationBuilder.DropColumn(
                name: "UsuarioCriacaoId",
                table: "Categoria");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Boleto");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Boleto");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Boleto");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Boleto");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracaoId",
                table: "Boleto");

            migrationBuilder.DropColumn(
                name: "UsuarioCriacaoId",
                table: "Boleto");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Banco");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Banco");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Banco");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracaoId",
                table: "Banco");

            migrationBuilder.DropColumn(
                name: "UsuarioCriacaoId",
                table: "Banco");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Atividade");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Atividade");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Atividade");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Atividade");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracaoId",
                table: "Atividade");

            migrationBuilder.DropColumn(
                name: "UsuarioCriacaoId",
                table: "Atividade");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Agenda");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracaoId",
                table: "Agenda");

            migrationBuilder.DropColumn(
                name: "UsuarioCriacaoId",
                table: "Agenda");

            migrationBuilder.AlterColumn<string>(
                name: "PeriodoCobranca",
                table: "Empresas",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AsaasSubscriptionId",
                table: "Empresas",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AsaasCustomerId",
                table: "Empresas",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAlteracao",
                table: "DespesasFixas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Ativo",
                table: "DespesasFixas",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

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
                name: "Livros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AutorId = table.Column<int>(type: "integer", nullable: false),
                    Titulo = table.Column<string>(type: "text", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Livros_AutorId",
                table: "Livros",
                column: "AutorId");
        }
    }
}

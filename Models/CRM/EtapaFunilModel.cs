using System.ComponentModel.DataAnnotations;
using WebApiSmartClinic.Models.Abstractions;

namespace WebApiSmartClinic.Models;

/// <summary>
/// Etapa do funil de vendas (coluna do Kanban do CRM).
/// Etapas fixas (IsFixa = true) usam EmpresaId = 0 e são compartilhadas entre todas as empresas —
/// o filtro global do AppDbContext trata esse caso (EmpresaId == EmpresaSelecionada OR EmpresaId == 0).
/// </summary>
public class EtapaFunilModel : IEntidadeEmpresa, IEntidadeAuditavel
{
    // Ids das etapas fixas usadas em regras de negócio (seed em AppDbContext)
    public const int IdNovoLead = 1;
    public const int IdGanho = 13;
    public const int IdPerdido = 14;
    public const int EmpresaGlobal = 0;

    public int Id { get; set; }
    public int EmpresaId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nome { get; set; } = string.Empty;
    public int Ordem { get; set; }
    public bool IsFixa { get; set; } = false;

    [MaxLength(20)]
    public string? Cor { get; set; }              // hex para o card do Kanban
    public string? ScriptSugerido { get; set; }   // mensagem padrão para o atendente copiar
    public bool Ativo { get; set; } = true;

    // Auditoria
    public string? UsuarioCriacaoId { get; set; }
    public DateTime DataCriacao { get; set; }
    public string? UsuarioAlteracaoId { get; set; }
    public DateTime? DataAlteracao { get; set; }
}

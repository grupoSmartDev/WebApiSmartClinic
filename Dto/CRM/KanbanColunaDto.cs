using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Dto.CRM;

/// <summary>Uma coluna do Kanban: a etapa + os leads que estão nela.</summary>
public sealed class KanbanColunaDto
{
    public int EtapaFunilId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Ordem { get; set; }
    public string? Cor { get; set; }
    public string? ScriptSugerido { get; set; }
    public bool IsFixa { get; set; }
    public int Total { get; set; }
    public List<LeadModel> Leads { get; set; } = new();
}

namespace WebApiSmartClinic.Dto.CRM;

public sealed class EtapaFunilEdicaoDto
{
    public int Id { get; set; }

    // Sem [Required]: para etapas fixas o front envia só { id, scriptSugerido }.
    // Para etapas customizadas a obrigatoriedade do Nome é validada no EtapaFunilService.Editar.
    public string? Nome { get; set; }
    public string? Cor { get; set; }
    public string? ScriptSugerido { get; set; }
    public int? Ordem { get; set; }
}

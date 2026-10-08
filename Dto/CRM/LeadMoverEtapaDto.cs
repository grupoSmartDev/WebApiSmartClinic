namespace WebApiSmartClinic.Dto.CRM;

public sealed class LeadMoverEtapaDto
{
    public int LeadId { get; set; }
    public int NovaEtapaFunilId { get; set; }

    // Obrigatório quando a nova etapa for "Perdido"
    public string? MotivoPerdaId { get; set; }
}

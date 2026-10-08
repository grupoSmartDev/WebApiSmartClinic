namespace WebApiSmartClinic.Dto.CRM;

public sealed class FollowUpCreateDto
{
    public int LeadId { get; set; }
    public int? EtapaFunilId { get; set; } // null/0 → etapa atual do lead
    public string? Canal { get; set; }
    public string? MensagemEnviada { get; set; }
    public string? RespostaRecebida { get; set; }
    public DateTime? ProximaAcaoData { get; set; }
    public string? ProximaAcaoDescricao { get; set; }
    public DateTime? DataContato { get; set; } // null → agora (UTC)
}

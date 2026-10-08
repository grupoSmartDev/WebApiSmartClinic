namespace WebApiSmartClinic.Dto.CRM;

/// <summary>
/// Edição de follow-up: só o desfecho ("o que aconteceu"), a próxima ação e a conclusão.
/// MensagemEnviada, Canal, DataContato e Etapa são imutáveis após o registro.
/// </summary>
public sealed class FollowUpEdicaoDto
{
    public int Id { get; set; }
    public string? RespostaRecebida { get; set; }
    public DateTime? ProximaAcaoData { get; set; }
    public string? ProximaAcaoDescricao { get; set; }

    // null = não altera a conclusão (a edição comum do accordion não envia este campo)
    public bool? Concluido { get; set; }

    // Ignorado na entrada: a data de conclusão é sempre definida pelo servidor (UTC)
    public DateTime? DataConclusao { get; set; }
}

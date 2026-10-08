using System.ComponentModel.DataAnnotations;
using WebApiSmartClinic.Models.Abstractions;

namespace WebApiSmartClinic.Models;

public class FollowUpModel : IEntidadeEmpresa, IEntidadeAuditavel
{
    public int Id { get; set; }
    public int EmpresaId { get; set; }

    public int LeadId { get; set; }
    public LeadModel? Lead { get; set; }

    public int EtapaFunilId { get; set; }          // etapa em que o lead estava no momento do contato
    public EtapaFunilModel? EtapaFunil { get; set; }

    [MaxLength(50)]
    public string? Canal { get; set; }              // WhatsApp, Ligacao, Email, Presencial
    public string? MensagemEnviada { get; set; }    // texto colado do WhatsApp
    public string? RespostaRecebida { get; set; }
    public DateTime? ProximaAcaoData { get; set; }
    public string? ProximaAcaoDescricao { get; set; }
    public DateTime DataContato { get; set; }

    // Conclusão do follow-up (após concluído, não pode mais ser editado)
    public bool Concluido { get; set; } = false;
    public DateTime? DataConclusao { get; set; }

    public bool Ativo { get; set; } = true;

    // Auditoria
    public string? UsuarioCriacaoId { get; set; }
    public DateTime DataCriacao { get; set; }
    public string? UsuarioAlteracaoId { get; set; }
    public DateTime? DataAlteracao { get; set; }
}

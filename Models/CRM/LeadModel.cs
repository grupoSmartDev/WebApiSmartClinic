using System.ComponentModel.DataAnnotations;
using WebApiSmartClinic.Models.Abstractions;

namespace WebApiSmartClinic.Models;

public class LeadModel : IEntidadeEmpresa, IEntidadeAuditavel
{
    public int Id { get; set; }
    public int EmpresaId { get; set; }

    [Required]
    [MaxLength(150)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [MaxLength(30)]
    public string Telefone { get; set; } = string.Empty;

    [MaxLength(150)]
    public string? Email { get; set; }

    [MaxLength(50)]
    public string? CanalOrigem { get; set; } // WhatsApp, Instagram, Facebook Ads, Indicacao, Site, Outro

    public int? CampanhaId { get; set; }
    public CampanhaModel? Campanha { get; set; }

    [MaxLength(100)]
    public string? FacebookLeadId { get; set; } // integração futura Meta Ads

    public int EtapaFunilId { get; set; }
    public EtapaFunilModel? EtapaFunil { get; set; }

    public int? ProfissionalId { get; set; }
    public ProfissionalModel? Profissional { get; set; }

    public string? Observacao { get; set; }
    public string? MotivoPerdaId { get; set; } // preenchido quando etapa = Perdido
    public DateTime? DataUltimaInteracao { get; set; }

    // true quando o lead está na etapa "Ganho" — habilita a conversão em Paciente (ConverterEmPaciente)
    public bool IsGanho { get; set; } = false;
    public int? PacienteId { get; set; } // preenchido quando convertido em Paciente

    public bool Ativo { get; set; } = true;
    public ICollection<FollowUpModel>? FollowUps { get; set; }

    // Auditoria
    public string? UsuarioCriacaoId { get; set; }
    public DateTime DataCriacao { get; set; }
    public string? UsuarioAlteracaoId { get; set; }
    public DateTime? DataAlteracao { get; set; }
}

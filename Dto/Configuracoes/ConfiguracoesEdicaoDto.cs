namespace WebApiSmartClinic.Dto.Configuracoes;

public sealed class ConfiguracoesEdicaoDto
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Sobrenome { get; set; }                 // razão social
    public string? CnpjEmpresaMatriz { get; set; }
    public string? InscricaoEstadual { get; set; }
    public string? InscricaoMunicipal { get; set; }
    public string? Endereco { get; set; }
    public string? Cep { get; set; }
    public string? Cidade { get; set; }
    public string? Estado { get; set; }
    public string? TelefoneFixo { get; set; }
    public string? Email { get; set; }
    public string? SiteOuRedeSocial { get; set; }
    public bool CelularComWhatsApp { get; set; }
}
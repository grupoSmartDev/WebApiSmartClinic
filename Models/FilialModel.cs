using System.Text.Json.Serialization;

namespace WebApiSmartClinic.Models;

public class FilialModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string? CNPJ { get; set; } // Pode ser null se for só uma unidade
    public string? Endereco { get; set; }
    public string? Telefone { get; set; }
    public string? Email { get; set; }
    public bool Ativo { get; set; } = true;

    // Relacionamento com Empresa
    public int EmpresaId { get; set; }
    [JsonIgnore]
    public EmpresaModel Empresa { get; set; }

    // Relacionamentos
    [JsonIgnore]
    public ICollection<UsuarioModel> Usuarios { get; set; } = new List<UsuarioModel>();

    // Data de Criação
    private DateTime _DataCriacao = DateTime.UtcNow;
    public DateTime DataCriacao
    {
        get => _DataCriacao.ToLocalTime();
        set => _DataCriacao = DateTime.SpecifyKind(value.ToUniversalTime(), DateTimeKind.Utc);
    }
}
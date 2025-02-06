
using System.Text.Json.Serialization;

namespace WebApiSmartClinic.Models;

public class UsuarioModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public ICollection<Permissao> Permissao { get; set; }
    public string CPF { get; set; }
    private DateTime? _DataCriacao;
    public DateTime? DataCriacao 
    {
        get => _DataCriacao;
        set => _DataCriacao = value.HasValue ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc) : DateTime.UtcNow;
    }
    public bool Ativo { get; set; } = true;
    public int? ProfissionalId { get; set; } // Relacionamento com Profissional

    [JsonIgnore]
    public ProfissionalModel? Profissional { get; set; }
    public int  EmpresaId { get; set; }
    [JsonIgnore]
    public EmpresaModel? Empresa { get; set; }


}

public enum Permissao
{
    A,
    S,
    N
}
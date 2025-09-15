using System.Text.Json.Serialization;

namespace WebApiSmartClinic.Models;

/// <summary>
/// Define a quais empresas um usuário tem acesso e permissões por empresa.
/// </summary>
public class UsuarioEmpresaModel
{
    public int Id { get; set; }

    public string UsuarioId { get; set; } // IdentityUser.Id
    [JsonIgnore] public User? Usuario { get; set; } // use o seu modelo Identity "User"

    public int EmpresaId { get; set; }
    [JsonIgnore] public EmpresaModel? Empresa { get; set; }

    public bool PodeLer { get; set; } = true;
    public bool PodeEscrever { get; set; } = true;
    public bool PodeExcluir { get; set; } = false;

    public bool EmpresaPadrao { get; set; } = false;
}
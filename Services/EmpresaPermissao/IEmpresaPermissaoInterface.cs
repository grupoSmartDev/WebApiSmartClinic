using System.Threading.Tasks;

namespace WebApiSmartClinic.Services.EmpresaPermissao;

public interface IEmpresaPermissaoInterface
{
    Task<bool> UsuarioPodeVerTodasEmpresasAsync(string? usuarioId);
    Task<bool> UsuarioTemAcessoEmpresaAsync(string? usuarioId, int empresaId);
    Task<int> ObterEmpresaPadraoAsync(string? usuarioId);
    Task<bool> UsuarioPodeExcluirAsync(string usuarioId, int empresaId);
}
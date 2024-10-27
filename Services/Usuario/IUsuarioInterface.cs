
using WebApiSmartClinic.Dto.Usuario;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Usuario
{
    public interface IUsuarioInterface
    {
        Task<ResponseModel<List<UsuarioModel>>> Listar();
        Task<ResponseModel<List<UsuarioModel>>> Delete(int idUsuario);
        Task<ResponseModel<UsuarioModel>> BuscarPorId(int idUsuario);
        Task<ResponseModel<List<UsuarioModel>>> Criar(UsuarioCreateDto usuarioCreateDto);
        Task<ResponseModel<List<UsuarioModel>>> Editar(UsuarioEdicaoDto usuarioEdicaoDto);
    }
}
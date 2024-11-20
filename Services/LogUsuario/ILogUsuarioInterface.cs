
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.LogUsuario
{
    public interface ILogUsuarioInterface
    {
        Task<ResponseModel<List<LogUsuarioModel>>> Listar();
        Task<ResponseModel<LogUsuarioModel>> BuscarPorId(int idLogUsuario);
        Task<ResponseModel<List<LogUsuarioModel>>> Inserir(int id, string descricao, string rotina, int usuarioId);
    }
}
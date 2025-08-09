
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.LogUsuario
{
    public interface ILogUsuarioInterface
    {
        Task<ResponseModel<List<LogUsuarioModel>>> Listar(int pageNumber = 1, int pageSize = 10, int? codigoFiltro = null, string? descricaoFiltro = null, string? rotinaFiltro = null, int? idMovimentacaoFiltro = null,
            string? usuarioFiltro = null, DateTime? dataMovimentacaoFiltro = null, bool paginar = true);
        Task<ResponseModel<LogUsuarioModel>> BuscarPorId(int idLogUsuario);
        Task<ResponseModel<List<LogUsuarioModel>>> Inserir(int id, string descricao, string rotina, string usuarioId);
    }
}
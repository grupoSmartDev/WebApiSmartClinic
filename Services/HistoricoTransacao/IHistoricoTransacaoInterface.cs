
using WebApiSmartClinic.Dto.HistoricoTransacao;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.HistoricoTransacao
{
    public interface IHistoricoTransacaoInterface
    {
        Task<ResponseModel<List<HistoricoTransacaoModel>>> Listar(int pageNumber = 1, int pageSize = 10, int? codigoFiltro = null, string? bancoFiltro = null, DateTime? dataTransacaoFiltro = null, string? tipoTransacaoFiltro = null,
            string? descricaoFiltro = null, string? referenciaFiltro = null, string? usuarioFiltro = null, bool paginar = true);
        Task<ResponseModel<List<HistoricoTransacaoModel>>> Delete(int idHistoricoTransacao, int pageNumber = 1, int pageSize = 10, bool paginar = true);
        Task<ResponseModel<HistoricoTransacaoModel>> BuscarPorId(int idHistoricoTransacao);
        Task<ResponseModel<List<HistoricoTransacaoModel>>> Criar(HistoricoTransacaoCreateDto historicotransacaoCreateDto, int pageNumber = 1, int pageSize = 10, bool paginar = true);
        Task<ResponseModel<List<HistoricoTransacaoModel>>> Editar(HistoricoTransacaoEdicaoDto historicotransacaoEdicaoDto, int pageNumber = 1, int pageSize = 10, bool paginar = true);
    }
}
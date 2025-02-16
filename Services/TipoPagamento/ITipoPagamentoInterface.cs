
using WebApiSmartClinic.Dto.TipoPagamento;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.TipoPagamento
{
    public interface ITipoPagamentoInterface
    {
        Task<ResponseModel<List<TipoPagamentoModel>>> Listar(int pageNumber = 1, int pageSize = 10, int? codigoFiltro = null, string? descricaoFiltro = null, bool paginar = true);
        Task<ResponseModel<List<TipoPagamentoModel>>> Delete(int idTipoPagamento, int pageNumber = 1, int pageSize = 10, bool paginar = true);
        Task<ResponseModel<TipoPagamentoModel>> BuscarPorId(int idTipoPagamento);
        Task<ResponseModel<List<TipoPagamentoModel>>> Criar(TipoPagamentoCreateDto tipopagamentoCreateDto, int pageNumber = 1, int pageSize = 10, bool paginar = true);
        Task<ResponseModel<List<TipoPagamentoModel>>> Editar(TipoPagamentoEdicaoDto tipopagamentoEdicaoDto, int pageNumber = 1, int pageSize = 10, bool paginar = true);
    }
}

using WebApiSmartClinic.Dto.FormaPagamento;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.FormaPagamento
{
    public interface IFormaPagamentoInterface
    {
        Task<ResponseModel<List<FormaPagamentoModel>>> Listar(int pageNumber = 1, int pageSize = 10, int? codigoFiltro = null, int? parcelasFiltro = null, string? descricaoFiltro = null, bool paginar = true);
        Task<ResponseModel<List<FormaPagamentoModel>>> Delete(int idFormaPagamento, int pageNumber = 1, int pageSize = 10, bool paginar = true);
        Task<ResponseModel<FormaPagamentoModel>> BuscarPorId(int idFormaPagamento);
        Task<ResponseModel<List<FormaPagamentoModel>>> Criar(FormaPagamentoCreateDto formapagamentoCreateDto, int pageNumber = 1, int pageSize = 10, bool paginar = true);
        Task<ResponseModel<List<FormaPagamentoModel>>> Editar(FormaPagamentoEdicaoDto formapagamentoEdicaoDto, int pageNumber = 1, int pageSize = 10, bool paginar = true);
    }
}
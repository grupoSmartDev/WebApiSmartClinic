
using WebApiSmartClinic.Dto.CentroCusto;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.CentroCusto
{
    public interface ICentroCustoInterface
    {
        Task<ResponseModel<List<CentroCustoModel>>> ListarCentroCusto();
        Task<ResponseModel<List<CentroCustoModel>>> DeleteCentroCusto(int idCentroCusto);
        Task<ResponseModel<CentroCustoModel>> BuscarCentroCustoPorId(int idCentroCusto);
        Task<ResponseModel<List<CentroCustoModel>>> CriarCentroCusto(CentroCustoCreateDto centrocustoCreateDto);
        Task<ResponseModel<List<CentroCustoModel>>> EditarCentroCusto(CentroCustoEdicaoDto centrocustoEdicaoDto);
        Task<ResponseModel<List<CentroCustoModel>>> Listar(int pageNumber = 1, int pageSize = 10, string? idFiltro = null, string? descricaoFiltro = null, string? tipoFiltro = null, bool? inativoFiltro = null, bool paginar = true);
    }
}
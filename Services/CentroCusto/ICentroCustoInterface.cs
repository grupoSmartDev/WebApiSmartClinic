
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
    }
}
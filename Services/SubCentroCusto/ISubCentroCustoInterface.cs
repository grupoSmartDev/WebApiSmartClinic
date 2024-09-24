using WebApiSmartClinic.Dto.CentroCusto;
using WebApiSmartClinic.Dto.SubCentroCusto;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.SubCentroCusto
{
    public interface ISubCentroCustoInterface
    {
        Task<ResponseModel<List<SubCentroCustoModel>>> Listar();
        Task<ResponseModel<List<SubCentroCustoModel>>> Delete(int idCentroCusto);
        Task<ResponseModel<SubCentroCustoModel>> BuscarPorId(int idCentroCusto);
        Task<ResponseModel<List<SubCentroCustoModel>>> Criar(SubCentroCustoCreateDto centrocustoCreateDto);
        Task<ResponseModel<List<SubCentroCustoModel>>> Editar(SubCentroCustoEdicaoDto centrocustoEdicaoDto);
    }
}


using WebApiSmartClinic.Dto.PlanoConta;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.PlanoConta
{
    public interface IPlanoContaInterface
    {
        Task<ResponseModel<List<PlanoContaModel>>> Listar(int pageNumber = 1, int pageSize = 10, string? codigoFiltro = null, string? nomeFiltro = null, Tipo? tipoFiltro = null, bool? inativoFiltro = null, bool paginar = true);
        Task<ResponseModel<List<PlanoContaModel>>> Editar(PlanoContaEdicaoDto planocontaEdicaoDto, int pageNumber = 1, int pageSize = 10);
        Task<ResponseModel<List<PlanoContaModel>>> Criar(PlanoContaCreateDto planocontaCreateDto, int pageNumber = 1, int pageSize = 10);
        Task<ResponseModel<List<PlanoContaModel>>> Delete(int idPlanoConta, int pageNumber = 1, int pageSize = 10);
        Task<ResponseModel<PlanoContaModel>> BuscarPorId(int idPlanoConta);
    }
}
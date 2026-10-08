using WebApiSmartClinic.Dto.CRM;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.CRM;

public interface IEtapaFunilInterface
{
    Task<ResponseModel<List<EtapaFunilModel>>> Listar();
    Task<ResponseModel<EtapaFunilModel>> BuscarPorId(int id);
    Task<ResponseModel<List<EtapaFunilModel>>> Criar(EtapaFunilCreateDto dto);
    Task<ResponseModel<List<EtapaFunilModel>>> Editar(EtapaFunilEdicaoDto dto);
    Task<ResponseModel<List<EtapaFunilModel>>> Delete(int id);
    Task<ResponseModel<List<EtapaFunilModel>>> ReordenarEtapas(List<EtapaFunilOrdemDto> ordens);
}

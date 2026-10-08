using WebApiSmartClinic.Dto.CRM;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.CRM;

public interface ICampanhaInterface
{
    Task<ResponseModel<List<CampanhaModel>>> Listar();
    Task<ResponseModel<CampanhaModel>> BuscarPorId(int id);
    Task<ResponseModel<List<CampanhaModel>>> Criar(CampanhaCreateDto dto);
    Task<ResponseModel<List<CampanhaModel>>> Editar(CampanhaEdicaoDto dto);
    Task<ResponseModel<List<CampanhaModel>>> Delete(int id);
}

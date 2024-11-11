
using WebApiSmartClinic.Dto.Plano;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Plano
{
    public interface IPlanoInterface
    {
        Task<ResponseModel<List<PlanoModel>>> Listar();
        Task<ResponseModel<List<PlanoModel>>> Delete(int idPlano);
        Task<ResponseModel<PlanoModel>> BuscarPorId(int idPlano);
        Task<ResponseModel<List<PlanoModel>>> Criar(PlanoCreateDto planoCreateDto);
        Task<ResponseModel<List<PlanoModel>>> Editar(PlanoEdicaoDto planoEdicaoDto);
    }
}
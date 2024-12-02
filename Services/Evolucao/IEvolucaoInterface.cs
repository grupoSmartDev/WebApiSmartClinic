
using WebApiSmartClinic.Dto.Evolucao;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Evolucao
{
    public interface IEvolucaoInterface
    {
        Task<ResponseModel<List<EvolucaoModel>>> Listar();
        Task<ResponseModel<List<EvolucaoModel>>> Delete(int idEvolucao);
        Task<ResponseModel<EvolucaoModel>> BuscarPorId(int idEvolucao);
        Task<ResponseModel<List<EvolucaoModel>>> Criar(EvolucaoCreateDto evolucaoCreateDto);
        Task<ResponseModel<List<EvolucaoModel>>> Editar(EvolucaoEdicaoDto evolucaoEdicaoDto);
    }
}
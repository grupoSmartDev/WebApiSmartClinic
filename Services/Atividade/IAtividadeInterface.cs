
using WebApiSmartClinic.Dto.Atividade;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Atividade
{
    public interface IAtividadeInterface
    {
        Task<ResponseModel<List<AtividadeModel>>> Listar();
        Task<ResponseModel<List<AtividadeModel>>> Delete(int idAtividade);
        Task<ResponseModel<AtividadeModel>> BuscarPorId(int idAtividade);
        Task<ResponseModel<List<AtividadeModel>>> Criar(AtividadeCreateDto atividadeCreateDto);
        Task<ResponseModel<List<AtividadeModel>>> Editar(AtividadeEdicaoDto atividadeEdicaoDto);
    }
}
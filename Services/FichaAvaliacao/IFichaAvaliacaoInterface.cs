
using WebApiSmartClinic.Dto.FichaAvaliacao;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.FichaAvaliacao
{
    public interface IFichaAvaliacaoInterface
    {
        Task<ResponseModel<List<FichaAvaliacaoModel>>> Listar();
        Task<ResponseModel<List<FichaAvaliacaoModel>>> Delete(int idFichaAvaliacao);
        Task<ResponseModel<FichaAvaliacaoModel>> BuscarPorId(int idFichaAvaliacao);
        Task<ResponseModel<FichaAvaliacaoModel>> BuscarPorIdPaciente(int PacienteId);
        Task<ResponseModel<List<FichaAvaliacaoModel>>> Criar(FichaAvaliacaoCreateDto fichaavaliacaoCreateDto);
        Task<ResponseModel<List<FichaAvaliacaoModel>>> Editar(FichaAvaliacaoEdicaoDto fichaavaliacaoEdicaoDto);
    }
}

using WebApiSmartClinic.Dto.Conselho;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Conselho
{
    public interface IConselhoInterface
    {
        Task<ResponseModel<List<ConselhoModel>>> Listar();
        Task<ResponseModel<List<ConselhoModel>>> Delete(int idConselho);
        Task<ResponseModel<ConselhoModel>> BuscarPorId(int idConselho);
        Task<ResponseModel<List<ConselhoModel>>> Criar(ConselhoCreateDto conselhoCreateDto);
        Task<ResponseModel<List<ConselhoModel>>> Editar(ConselhoEdicaoDto conselhoEdicaoDto);
    }
}
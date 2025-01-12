
using WebApiSmartClinic.Dto.Conselho;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Conselho
{
    public interface IConselhoInterface
    {
        Task<ResponseModel<List<ConselhoModel>>> Listar(int pageNumber = 1, int pageSize = 10, int? codigoFiltro = null, string? nomeFiltro = null, string? siglaFiltro = null, bool paginar = true);
        Task<ResponseModel<List<ConselhoModel>>> Delete(int idConselho, int pageNumber = 1, int pageSize = 10);
        Task<ResponseModel<ConselhoModel>> BuscarPorId(int idConselho);
        Task<ResponseModel<List<ConselhoModel>>> Criar(ConselhoCreateDto conselhoCreateDto, int pageNumber = 1, int pageSize = 10);
        Task<ResponseModel<List<ConselhoModel>>> Editar(ConselhoEdicaoDto conselhoEdicaoDto, int pageNumber = 1, int pageSize = 10);
    }
}

using WebApiSmartClinic.Dto.Atividade;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Atividade
{
    public interface IAtividadeInterface
    {
        Task<ResponseModel<List<AtividadeModel>>> Listar(int pageNumber = 1, int pageSize = 10, int? codigoFiltro = null, string? tituloFiltro = null,  string? descricaoFiltro = null, bool paginar = true);
        Task<ResponseModel<AtividadeModel>> BuscarPorId(int idAtividade);
        Task<ResponseModel<List<AtividadeModel>>> Criar(AtividadeCreateDto atividadeCreateDto, int pageNumber = 1, int pageSize = 10);
        Task<ResponseModel<List<AtividadeModel>>> Delete(int idAtividade, int pageNumber = 1, int pageSize = 10);
        Task<ResponseModel<List<AtividadeModel>>> Editar(AtividadeEdicaoDto atividadeEdicaoDto, int pageNumber = 1, int pageSize = 10);
    }
}
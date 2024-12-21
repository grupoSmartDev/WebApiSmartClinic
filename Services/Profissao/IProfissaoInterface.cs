
using WebApiSmartClinic.Dto.Profissao;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Profissao
{
    public interface IProfissaoInterface
    {
        Task<ResponseModel<List<ProfissaoModel>>> Listar(int pageNumber = 1, int pageSize = 10, int? codigoFiltro = null, string? nomeFiltro = null, bool paginar = true);
        Task<ResponseModel<List<ProfissaoModel>>> Delete(int idProfissao, int pageNumber = 1, int pageSize = 10);
        Task<ResponseModel<ProfissaoModel>> BuscarPorId(int idProfissao);
        Task<ResponseModel<List<ProfissaoModel>>> Criar(ProfissaoCreateDto profissaoCreateDto, int pageNumber = 1, int pageSize = 10);
        Task<ResponseModel<List<ProfissaoModel>>> Editar(ProfissaoEdicaoDto profissaoEdicaoDto, int pageNumber = 1, int pageSize = 10);
    }
}
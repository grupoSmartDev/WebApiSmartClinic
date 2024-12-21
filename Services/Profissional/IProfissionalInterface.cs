
using WebApiSmartClinic.Dto.Profissional;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Profissional
{
    public interface IProfissionalInterface
    {
        Task<ResponseModel<List<ProfissionalModel>>> Listar(int pageNumber = 1, int pageSize = 10, int? codigoFiltro = null, string? nomeFiltro = null, string? cpfFiltro = null, int? profissaoIdFiltro = null, bool paginar = true);
        Task<ResponseModel<List<ProfissionalModel>>> Delete(int idProfissional, int pageNumber = 1, int pageSize = 10);
        Task<ResponseModel<ProfissionalModel>> BuscarPorId(int idProfissional);
        Task<ResponseModel<List<ProfissionalModel>>> Criar(ProfissionalCreateDto profissionalCreateDto, int pageNumber = 1, int pageSize = 10);
        Task<ResponseModel<List<ProfissionalModel>>> Editar(ProfissionalEdicaoDto profissionalEdicaoDto, int pageNumber = 1, int pageSize = 10);
    }
}
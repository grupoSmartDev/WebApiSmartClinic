
using WebApiSmartClinic.Dto.Convenio;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Convenio
{
    public interface IConvenioInterface
    {
        Task<ResponseModel<List<ConvenioModel>>> Listar(int pageNumber = 1, int pageSize = 10, int? codigoFiltro = null, string? nomeFiltro = null, string? telefoneFiltro = null, string? registroAvsFiltro = null, bool paginar = true);
        Task<ResponseModel<List<ConvenioModel>>> Delete(int idConvenio, int pageNumber = 1, int pageSize = 10);
        Task<ResponseModel<ConvenioModel>> BuscarPorId(int idConvenio);
        Task<ResponseModel<List<ConvenioModel>>> Criar(ConvenioCreateDto convenioCreateDto, int pageNumber = 1, int pageSize = 10);
        Task<ResponseModel<List<ConvenioModel>>> Editar(ConvenioEdicaoDto convenioEdicaoDto, int pageNumber = 1, int pageSize = 10);
    }
}
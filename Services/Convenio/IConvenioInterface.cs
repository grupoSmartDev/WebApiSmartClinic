
using WebApiSmartClinic.Dto.Convenio;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Convenio
{
    public interface IConvenioInterface
    {
        Task<ResponseModel<List<ConvenioModel>>> Listar();
        Task<ResponseModel<List<ConvenioModel>>> Delete(int idConvenio);
        Task<ResponseModel<ConvenioModel>> BuscarPorId(int idConvenio);
        Task<ResponseModel<List<ConvenioModel>>> Criar(ConvenioCreateDto convenioCreateDto);
        Task<ResponseModel<List<ConvenioModel>>> Editar(ConvenioEdicaoDto convenioEdicaoDto);
    }
}
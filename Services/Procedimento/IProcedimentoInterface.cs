
using WebApiSmartClinic.Dto.Procedimento;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Procedimento
{
    public interface IProcedimentoInterface
    {
        Task<ResponseModel<List<ProcedimentoModel>>> Listar(int pageNumber = 1, int pageSize = 10);
        Task<ResponseModel<List<ProcedimentoModel>>> Delete(int idProcedimento, int pageNumber = 1, int pageSize = 10);
        Task<ResponseModel<ProcedimentoModel>> BuscarPorId(int idProcedimento);
        Task<ResponseModel<List<ProcedimentoModel>>> Criar(ProcedimentoCreateDto procedimentoCreateDto, int pageNumber = 1, int pageSize = 10);
        Task<ResponseModel<List<ProcedimentoModel>>> Editar(ProcedimentoEdicaoDto procedimentoEdicaoDto, int pageNumber = 1, int pageSize = 10);
    }
}
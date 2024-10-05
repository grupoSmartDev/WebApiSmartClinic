
using WebApiSmartClinic.Dto.Procedimento;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Procedimento
{
    public interface IProcedimentoInterface
    {
        Task<ResponseModel<List<ProcedimentoModel>>> Listar();
        Task<ResponseModel<List<ProcedimentoModel>>> Delete(int idProcedimento);
        Task<ResponseModel<ProcedimentoModel>> BuscarPorId(int idProcedimento);
        Task<ResponseModel<List<ProcedimentoModel>>> Criar(ProcedimentoCreateDto procedimentoCreateDto);
        Task<ResponseModel<List<ProcedimentoModel>>> Editar(ProcedimentoEdicaoDto procedimentoEdicaoDto);
    }
}
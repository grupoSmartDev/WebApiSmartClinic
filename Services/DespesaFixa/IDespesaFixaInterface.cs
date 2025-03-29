using WebApiSmartClinic.Dto.DespesaFixa;
using WebApiSmartClinic.Dto.Evolucao;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.DespesaFixa;

public interface IDespesaFixaInterface
{
    Task<ResponseModel<List<DespesaFixaModel>>> Listar();
    Task<ResponseModel<List<DespesaFixaModel>>> Delete(int idEvolucao);
    Task<ResponseModel<DespesaFixaModel>> BuscarPorId(int idEvolucao);
    Task<ResponseModel<List<DespesaFixaModel>>> Criar(DespesaFixaCreateDto evolucaoCreateDto);
    Task<ResponseModel<List<DespesaFixaModel>>> Editar(DespesaFixaEdicaoDto evolucaoEdicaoDto);
}

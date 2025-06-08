using WebApiSmartClinic.Dto.DespesaFixa;
using WebApiSmartClinic.Dto.Evolucao;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.DespesaFixa;

public interface IDespesaFixaInterface
{
    Task<ResponseModel<List<DespesaFixaModel>>> Listar(int pageNumber = 1, int pageSize = 10, int? idFiltro = null, string? descricaoFiltro = null, string? vencimentoFiltro = null,
        string? centroCustoFiltro = null, string? planoContasFiltro = null, bool paginar = true);
    Task<ResponseModel<List<DespesaFixaModel>>> Delete(int idEvolucao);
    Task<ResponseModel<DespesaFixaModel>> BuscarPorId(int idEvolucao);
    Task<ResponseModel<List<DespesaFixaModel>>> Criar(DespesaFixaCreateDto evolucaoCreateDto);
    Task<ResponseModel<List<DespesaFixaModel>>> Editar(DespesaFixaEdicaoDto evolucaoEdicaoDto);
}

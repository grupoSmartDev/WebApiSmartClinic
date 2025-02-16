
using WebApiSmartClinic.Dto.Comissao;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Comissao
{
    public interface IComissaoInterface
    {
        Task<ResponseModel<List<ComissaoModel>>> Listar(int pageNumber = 1, int pageSize = 10, int? codigoFiltro = null, string? profissionalFiltro = null, bool paginar = true);
        Task<ResponseModel<List<ComissaoModel>>> Delete(int idComissao, int pageNumber = 1, int pageSize = 10, bool paginar = true);
        Task<ResponseModel<ComissaoModel>> BuscarPorId(int idComissao);
        Task<ResponseModel<List<ComissaoModel>>> Criar(ComissaoCreateDto comissaoCreateDto, int pageNumber = 1, int pageSize = 10, bool paginar = true);
        Task<ResponseModel<List<ComissaoModel>>> Editar(ComissaoEdicaoDto comissaoEdicaoDto, int pageNumber = 1, int pageSize = 10, bool paginar = true);
        Task<ResponseModel<ComissaoModel>> PagarComissao(int comissaoId);
        Task<ResponseModel<List<ComissaoModel>>> ObterComissoesPendentes(int profissionalId);
        Task<decimal> ObterPercentualComissao(int procedimentoId);
    }
}
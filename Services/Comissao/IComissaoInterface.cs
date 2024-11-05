
using WebApiSmartClinic.Dto.Comissao;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Comissao
{
    public interface IComissaoInterface
    {
        Task<ResponseModel<List<ComissaoModel>>> Listar();
        Task<ResponseModel<List<ComissaoModel>>> Delete(int idComissao);
        Task<ResponseModel<ComissaoModel>> BuscarPorId(int idComissao);
        Task<ResponseModel<List<ComissaoModel>>> Criar(ComissaoCreateDto comissaoCreateDto);
        Task<ResponseModel<List<ComissaoModel>>> Editar(ComissaoEdicaoDto comissaoEdicaoDto);
        Task<ResponseModel<ComissaoModel>> PagarComissao(int comissaoId);
        Task<ResponseModel<List<ComissaoModel>>> ObterComissoesPendentes(int profissionalId);
        Task<decimal> ObterPercentualComissao(int procedimentoId);
    }
}
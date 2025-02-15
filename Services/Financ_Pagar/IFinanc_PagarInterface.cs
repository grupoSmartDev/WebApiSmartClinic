
using WebApiSmartClinic.Dto.Financ_Pagar;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Financ_Pagar
{
    public interface IFinanc_PagarInterface
    {
        Task<ResponseModel<List<Financ_PagarModel>>> Listar(int pageNumber = 1, int pageSize = 10, int? codigoFiltro = null, string? descricaoFiltro = null, DateTime? dataEmissaoInicio = null, DateTime? dataEmissaoFim = null,
            decimal? valorMinimoFiltro = null, decimal? valorMaximoFiltro = null, int? parcelaNumeroFiltro = null, DateTime? vencimentoInicio = null, DateTime? vencimentoFim = null, bool paginar = true);
        Task<ResponseModel<List<Financ_PagarModel>>> Delete(int idFinanc_Pagar, int pageNumber = 1, int pageSize = 10);
        Task<ResponseModel<Financ_PagarModel>> BuscarPorId(int idFinanc_Pagar);
        Task<ResponseModel<List<Financ_PagarModel>>> Criar(Financ_PagarCreateDto financ_pagarCreateDto, int pageNumber = 1, int pageSize = 10);
        Task<ResponseModel<List<Financ_PagarModel>>> Editar(Financ_PagarEdicaoDto financ_pagarEdicaoDto, int pageNumber = 1, int pageSize = 10);
        Task<ResponseModel<List<Financ_PagarSubModel>>> ObterContasAbertas();
        Task<ResponseModel<string>> EstornarAgrupamento(List<int> parcelasIds);
        Task<ResponseModel<string>> EstornarParcela(int parcelaId);
        Task<ResponseModel<string>> AgruparParcelas(int idPai, List<int> parcelasFilhasIds, decimal valorPago);
        Task<ResponseModel<Financ_PagarModel>> BaixarParcela(int parcelaId, decimal valorPago);
        Task<ResponseModel<List<Financ_PagarModel>>> ListarAnalitico(int pageNumber = 1, int pageSize = 10, int? codigoFiltro = null, string? descricaoFiltro = null, DateTime? dataEmissaoInicio = null, DateTime? dataEmissaoFim = null,
            decimal? valorMinimoFiltro = null, decimal? valorMaximoFiltro = null, int? parcelaNumeroFiltro = null, DateTime? vencimentoInicio = null, DateTime? vencimentoFim = null, bool paginar = true);
        Task<ResponseModel<List<Financ_PagarSubModel>>> ListarSintetico(int pageNumber = 1, int pageSize = 10, int? idPaiFiltro = null, int? parcelaNumeroFiltro = null, DateTime? vencimentoInicio = null, DateTime? vencimentoFim = null, bool paginar = true);
    }
}
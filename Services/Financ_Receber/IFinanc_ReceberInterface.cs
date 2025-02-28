
using WebApiSmartClinic.Dto.Financ_Receber;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Financ_Receber
{
    public interface IFinanc_ReceberInterface
    {
        Task<ResponseModel<List<Financ_ReceberModel>>> Listar(int pageNumber = 1, int pageSize = 10, int? codigoFiltro = null, string? descricaoFiltro = null, DateTime? dataEmissaoInicio = null, DateTime? dataEmissaoFim = null,
            decimal? valorMinimoFiltro = null, decimal? valorMaximoFiltro = null, int? parcelaNumeroFiltro = null, DateTime? vencimentoInicio = null, DateTime? vencimentoFim = null, bool paginar = true);
        Task<ResponseModel<List<Financ_ReceberModel>>> Delete(int idFinanc_Receber, int pageNumber = 1, int pageSize = 10);
        Task<ResponseModel<Financ_ReceberModel>> BuscarPorId(int idFinanc_Receber);
        Task<ResponseModel<List<Financ_ReceberModel>>> Criar(Financ_ReceberCreateDto financ_receberCreateDto, int pageNumber = 1, int pageSize = 10);
        Task<ResponseModel<List<Financ_ReceberModel>>> Editar(Financ_ReceberEdicaoDto financ_receberEdicaoDto, int pageNumber = 1, int pageSize = 10);
        Task<ResponseModel<List<Financ_ReceberModel>>> BuscarContasEmAberto();
        Task<ResponseModel<Financ_ReceberSubModel>> BaixarParcela(Financ_ReceberSubEdicaoDto financ_receberSubEdicaoDto);
        Task<ResponseModel<decimal>> CalcularTotalRecebiveis(int cliente, DateTime? dataInicio = null, DateTime? dataFim = null);
        Task<ResponseModel<string>> EstornarParcela(int idParcela);
        Task<ResponseModel<List<Financ_ReceberSubModel>>> ListarSintetico(int pageNumber = 1, int pageSize = 10,
            int? idPaiFiltro = null, int? parcelaNumeroFiltro = null, string? dataBaseFiltro = "V",
            DateTime? dataFiltroInicio = null, DateTime? dataFiltroFim = null, bool parcelasVencidasFiltro = false, bool paginar = true);
        Task<ResponseModel<List<Financ_ReceberModel>>> ListarAnalitico(
        int pageNumber = 1,
        int pageSize = 10,
        int? idFiltro = null,
        string? descricaoFiltro = null,
        int? pacienteIdFiltro = null,
        string? dataBaseFiltro = "E",
        string? ccFiltro = null,
        DateTime? dataFiltroInicio = null,
        DateTime? dataFiltroFim = null,
        bool paginar = true
        );
    }
}
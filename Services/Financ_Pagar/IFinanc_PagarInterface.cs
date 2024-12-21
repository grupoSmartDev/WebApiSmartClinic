
using WebApiSmartClinic.Dto.Financ_Pagar;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Financ_Pagar
{
    public interface IFinanc_PagarInterface
    {
        Task<ResponseModel<List<Financ_PagarModel>>> Listar();
        Task<ResponseModel<List<Financ_PagarModel>>> Delete(int idFinanc_Pagar);
        Task<ResponseModel<Financ_PagarModel>> BuscarPorId(int idFinanc_Pagar);
        Task<ResponseModel<List<Financ_PagarModel>>> Criar(Financ_PagarCreateDto financ_pagarCreateDto);
        Task<ResponseModel<List<Financ_PagarModel>>> Editar(Financ_PagarEdicaoDto financ_pagarEdicaoDto);
        Task<ResponseModel<List<Financ_PagarSubModel>>> ObterContasAbertas();
        Task<ResponseModel<string>> EstornarAgrupamento(List<int> parcelasIds);
        Task<ResponseModel<string>> EstornarParcela(int parcelaId);
        Task<ResponseModel<string>> AgruparParcelas(int idPai, List<int> parcelasFilhasIds, decimal valorPago);
        Task<ResponseModel<Financ_PagarModel>> BaixarParcela(int parcelaId, decimal valorPago);

        //Task<ResponseModel<Financ_PagarModel>> BaixarPagamento(int idFinanc_Pagar, decimal valorPago, DateTime? dataPagamento = null);
        //Task<ResponseModel<Financ_PagarModel>> EstornarPagamento(int idFinanc_Pagar);
    }
}
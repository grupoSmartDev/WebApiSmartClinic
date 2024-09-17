
using WebApiSmartClinic.Dto.TipoPagamento;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.TipoPagamento
{
    public interface ITipoPagamentoInterface
    {
        Task<ResponseModel<List<TipoPagamentoModel>>> ListarTipoPagamento();
        Task<ResponseModel<List<TipoPagamentoModel>>> DeleteTipoPagamento(int idTipoPagamento);
        Task<ResponseModel<TipoPagamentoModel>> BuscarTipoPagamentoPorId(int idTipoPagamento);
        Task<ResponseModel<List<TipoPagamentoModel>>> CriarTipoPagamento(TipoPagamentoCreateDto tipopagamentoCreateDto);
        Task<ResponseModel<List<TipoPagamentoModel>>> EditarTipoPagamento(TipoPagamentoEdicaoDto tipopagamentoEdicaoDto);
    }
}
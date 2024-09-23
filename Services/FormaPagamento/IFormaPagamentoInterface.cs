
using WebApiSmartClinic.Dto.FormaPagamento;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.FormaPagamento
{
    public interface IFormaPagamentoInterface
    {
        Task<ResponseModel<List<FormaPagamentoModel>>> ListarFormaPagamento();
        Task<ResponseModel<List<FormaPagamentoModel>>> DeleteFormaPagamento(int idFormaPagamento);
        Task<ResponseModel<FormaPagamentoModel>> BuscarFormaPagamentoPorId(int idFormaPagamento);
        Task<ResponseModel<List<FormaPagamentoModel>>> CriarFormaPagamento(FormaPagamentoCreateDto formapagamentoCreateDto);
        Task<ResponseModel<List<FormaPagamentoModel>>> EditarFormaPagamento(FormaPagamentoEdicaoDto formapagamentoEdicaoDto);
    }
}
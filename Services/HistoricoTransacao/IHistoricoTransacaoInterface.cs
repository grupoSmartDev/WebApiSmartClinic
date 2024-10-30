
using WebApiSmartClinic.Dto.HistoricoTransacao;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.HistoricoTransacao
{
    public interface IHistoricoTransacaoInterface
    {
        Task<ResponseModel<List<HistoricoTransacaoModel>>> Listar();
        Task<ResponseModel<List<HistoricoTransacaoModel>>> Delete(int idHistoricoTransacao);
        Task<ResponseModel<HistoricoTransacaoModel>> BuscarPorId(int idHistoricoTransacao);
        Task<ResponseModel<List<HistoricoTransacaoModel>>> Criar(HistoricoTransacaoCreateDto historicotransacaoCreateDto);
        Task<ResponseModel<List<HistoricoTransacaoModel>>> Editar(HistoricoTransacaoEdicaoDto historicotransacaoEdicaoDto);
    }
}
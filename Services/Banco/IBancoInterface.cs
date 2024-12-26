
using WebApiSmartClinic.Dto.Banco;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Banco
{
    public interface IBancoInterface
    {
        Task<ResponseModel<List<BancoModel>>> Listar(int pageNumber = 1, int pageSize = 10, int? codigoFiltro = null, string? nomeBancoFiltro = null, string? nomeTitularFiltro = null, string? documentoTitularFiltro = null, bool paginar = true);
        Task<ResponseModel<List<BancoModel>>> Delete(int idBanco, int pageNumber = 1, int pageSize = 10);
        Task<ResponseModel<BancoModel>> BuscarPorId(int idBanco);
        Task<ResponseModel<List<BancoModel>>> Criar(BancoCreateDto bancoCreateDto, int pageNumber = 1, int pageSize = 10);
        Task<ResponseModel<List<BancoModel>>> Editar(BancoEdicaoDto bancoEdicaoDto, int pageNumber = 1, int pageSize = 10);
        Task<ResponseModel<BancoModel>> DebitarSaldo(int idBanco, decimal valor);
        Task<ResponseModel<BancoModel>> CreditarSaldo(int idBanco, decimal valor);
        Task<ResponseModel<List<HistoricoTransacaoModel>>> ObterHistoricoTransacoes(int bancoId);
        Task RegistrarHistoricoTransacao(int idBanco, decimal valor, string tipoTransacao);
    }
}
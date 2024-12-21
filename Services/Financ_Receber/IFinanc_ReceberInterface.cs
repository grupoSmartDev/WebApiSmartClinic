
using WebApiSmartClinic.Dto.Financ_Receber;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Financ_Receber
{
    public interface IFinanc_ReceberInterface
    {
        Task<ResponseModel<List<Financ_ReceberModel>>> Listar();
        Task<ResponseModel<List<Financ_ReceberModel>>> Delete(int idFinanc_Receber);
        Task<ResponseModel<Financ_ReceberModel>> BuscarPorId(int idFinanc_Receber);
        Task<ResponseModel<List<Financ_ReceberModel>>> Criar(Financ_ReceberCreateDto financ_receberCreateDto);
        Task<ResponseModel<List<Financ_ReceberModel>>> Editar(Financ_ReceberEdicaoDto financ_receberEdicaoDto);
        Task<ResponseModel<List<Financ_ReceberModel>>> BuscarContasEmAberto();
        Task<ResponseModel<Financ_ReceberSubModel>> QuitarParcela(int idParcela, decimal valorPago);
        Task<ResponseModel<decimal>> CalcularTotalRecebiveis(int cliente, DateTime? dataInicio = null, DateTime? dataFim = null);
        Task<ResponseModel<string>> EstornarParcela(int idParcela);
    }
}
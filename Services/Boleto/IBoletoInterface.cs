
using WebApiSmartClinic.Dto.Boleto;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Boleto
{
    public interface IBoletoInterface
    {
        Task<ResponseModel<List<BoletoModel>>> Listar();
        Task<ResponseModel<List<BoletoModel>>> Delete(int idBoleto);
        Task<ResponseModel<BoletoModel>> BuscarPorId(int idBoleto);
        Task<ResponseModel<List<BoletoModel>>> Criar(BoletoCreateDto boletoCreateDto);
        Task<ResponseModel<List<BoletoModel>>> Editar(BoletoEdicaoDto boletoEdicaoDto);
    }
}
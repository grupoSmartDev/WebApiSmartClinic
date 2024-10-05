
using WebApiSmartClinic.Dto.Banco;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Banco
{
    public interface IBancoInterface
    {
        Task<ResponseModel<List<BancoModel>>> Listar();
        Task<ResponseModel<List<BancoModel>>> Delete(int idBanco);
        Task<ResponseModel<BancoModel>> BuscarPorId(int idBanco);
        Task<ResponseModel<List<BancoModel>>> Criar(BancoCreateDto bancoCreateDto);
        Task<ResponseModel<List<BancoModel>>> Editar(BancoEdicaoDto bancoEdicaoDto);
    }
}
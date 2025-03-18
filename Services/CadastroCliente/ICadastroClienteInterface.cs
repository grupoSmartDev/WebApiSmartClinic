
using WebApiSmartClinic.Dto.CadastroCliente;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.CadastroCliente
{
    public interface ICadastroClienteInterface
    {
        Task<ResponseModel<List<CadastroClienteModel>>> Listar();
        Task<ResponseModel<List<CadastroClienteModel>>> Delete(int idCadastroCliente);
        Task<ResponseModel<CadastroClienteModel>> BuscarPorId(int idCadastroCliente);
        Task<ResponseModel<List<CadastroClienteModel>>> Criar(CadastroClienteCreateDto cadastroclienteCreateDto);
        Task<ResponseModel<List<CadastroClienteModel>>> Editar(CadastroClienteEdicaoDto cadastroclienteEdicaoDto);
    }
}
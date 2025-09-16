
using WebApiSmartClinic.Dto.CadastroCliente;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.CadastroCliente
{
    public interface ICadastroClienteInterface
    {
        Task<ResponseModel<List<EmpresaModel>>> Listar();
        Task<ResponseModel<List<EmpresaModel>>> Delete(int idCadastroCliente);
        Task<ResponseModel<EmpresaModel>> BuscarPorId(int idCadastroCliente);
        Task<ResponseModel<EmpresaModel>> Criar(CadastroClienteCreateDto dto);
        //Task<ResponseModel<List<CadastroClienteModel>>> Criar(CadastroClienteCreateDto cadastroclienteCreateDto);
        Task<ResponseModel<List<EmpresaModel>>> Editar(CadastroClienteEdicaoDto cadastroclienteEdicaoDto);
    }
}

using WebApiSmartClinic.Dto.Fornecedor;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Fornecedor
{
    public interface IFornecedorInterface
    {
        Task<ResponseModel<List<FornecedorModel>>> ListarFornecedor();
        Task<ResponseModel<List<FornecedorModel>>> DeleteFornecedor(int idFornecedor);
        Task<ResponseModel<FornecedorModel>> BuscarFornecedorPorId(int idFornecedor);
        Task<ResponseModel<List<FornecedorModel>>> CriarFornecedor(FornecedorCreateDto fornecedorCreateDto);
        Task<ResponseModel<List<FornecedorModel>>> EditarFornecedor(FornecedorEdicaoDto fornecedorEdicaoDto);
    }
}
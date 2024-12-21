
using WebApiSmartClinic.Dto.Fornecedor;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Fornecedor
{
    public interface IFornecedorInterface
    {
        Task<ResponseModel<FornecedorModel>> BuscarPorId(int idFornecedor);
        Task<ResponseModel<List<FornecedorModel>>> Criar(FornecedorCreateDto fornecedorCreateDto, int pageNumber = 1, int pageSize = 10);
        Task<ResponseModel<List<FornecedorModel>>> Delete(int idFornecedor, int pageNumber = 1, int pageSize = 10);
        Task<ResponseModel<List<FornecedorModel>>> Editar(FornecedorEdicaoDto fornecedorEdicaoDto, int pageNumber = 1, int pageSize = 10);
        Task<ResponseModel<List<FornecedorModel>>> Listar(int pageNumber = 1, int pageSize = 10, int? codigoFiltro = null, string? nomeFiltro = null, string? cpfFiltro = null, string? cnpjFiltro = null, string? celularFiltro = null, bool paginar = true);
    }
}
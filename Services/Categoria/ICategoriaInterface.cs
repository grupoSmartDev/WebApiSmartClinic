
using WebApiSmartClinic.Dto.Categoria;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Categoria
{
    public interface ICategoriaInterface
    {
        Task<ResponseModel<List<CategoriaModel>>> Listar(int pageNumber = 1, int pageSize = 10, int? codigoFiltro = null, string? nomeFiltro = null, bool paginar = true);
        Task<ResponseModel<List<CategoriaModel>>> Delete(int idCategoria, int pageNumber = 1, int pageSize = 10);
        Task<ResponseModel<CategoriaModel>> BuscarPorId(int idCategoria);
        Task<ResponseModel<List<CategoriaModel>>> Criar(CategoriaCreateDto categoriaCreateDto, int pageNumber = 1, int pageSize = 10);
        Task<ResponseModel<List<CategoriaModel>>> Editar(CategoriaEdicaoDto categoriaEdicaoDto, int pageNumber = 1, int pageSize = 10);
    }
}
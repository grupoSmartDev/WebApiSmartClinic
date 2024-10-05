
using WebApiSmartClinic.Dto.Categoria;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Categoria
{
    public interface ICategoriaInterface
    {
        Task<ResponseModel<List<CategoriaModel>>> Listar();
        Task<ResponseModel<List<CategoriaModel>>> Delete(int idCategoria);
        Task<ResponseModel<CategoriaModel>> BuscarPorId(int idCategoria);
        Task<ResponseModel<List<CategoriaModel>>> Criar(CategoriaCreateDto categoriaCreateDto);
        Task<ResponseModel<List<CategoriaModel>>> Editar(CategoriaEdicaoDto categoriaEdicaoDto);
    }
}
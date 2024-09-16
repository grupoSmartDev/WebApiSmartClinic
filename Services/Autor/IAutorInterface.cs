using WebApiSmartClinic.Dto.Autor;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Autor;

public interface IAutorInterface
{
    Task<ResponseModel<List<AutorModel>>> ListarAutores();
    Task<ResponseModel<AutorModel>> BuscarAutorPorId(int idAutor);
    Task<ResponseModel<AutorModel>> BuscarAutorPorIdLivro(int idLivro);
    Task<ResponseModel<List<AutorModel>>> CriarAutor(AutorCreateDto autorCreateDto);

    Task<ResponseModel<List<AutorModel>>> EditarAutor(AutorEdicaoDto autorEdicaoDto);
    Task<ResponseModel<List<AutorModel>>> ExcluirAutor(int idAutor);
}

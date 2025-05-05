
using WebApiSmartClinic.Dto.Exercicio;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Exercicio
{
    public interface IExercicioInterface
    {
        Task<ResponseModel<List<ExercicioModel>>> Listar(int pageNumber = 1, int pageSize = 10, int? codigoFiltro = null, string? descricaoFiltro = null, bool paginar = true);
        Task<ResponseModel<List<ExercicioModel>>> Delete(int idExercicio, int pageNumber = 1, int pageSize = 10);
        Task<ResponseModel<ExercicioModel>> BuscarPorId(int idExercicio);
        Task<ResponseModel<List<ExercicioModel>>> Criar(ExercicioCreateDto exercicioCreateDto, int pageNumber = 1, int pageSize = 10);
        Task<ResponseModel<List<ExercicioModel>>> Editar(ExercicioEdicaoDto exercicioEdicaoDto, int pageNumber = 1, int pageSize = 10);
    }
}
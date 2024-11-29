
using WebApiSmartClinic.Dto.Exercicio;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Exercicio
{
    public interface IExercicioInterface
    {
        Task<ResponseModel<List<ExercicioModel>>> Listar();
        Task<ResponseModel<List<ExercicioModel>>> Delete(int idExercicio);
        Task<ResponseModel<ExercicioModel>> BuscarPorId(int idExercicio);
        Task<ResponseModel<List<ExercicioModel>>> Criar(ExercicioCreateDto exercicioCreateDto);
        Task<ResponseModel<List<ExercicioModel>>> Editar(ExercicioEdicaoDto exercicioEdicaoDto);
    }
}
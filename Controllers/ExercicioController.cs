
using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.Exercicio;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.Exercicio;

namespace WebApiSmartClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExercicioController : ControllerBase
    {
        private readonly IExercicioInterface _exercicio;
        public ExercicioController(IExercicioInterface exercicio)
        {
            _exercicio = exercicio;
        }

        [HttpGet("Listar")]
        public async Task<ActionResult<ResponseModel<List<ExercicioModel>>>> Listar()
        {
            var exercicio = await _exercicio.Listar();
            return Ok(exercicio);
        }

        [HttpGet("BuscarPorId/{idExercicio}")]
        public async Task<ActionResult<ResponseModel<List<ExercicioModel>>>> BuscarPorId(int idExercicio)
        {
            var exercicio = await _exercicio.BuscarPorId(idExercicio);
            return Ok(exercicio);
        }

        [HttpPost("Criar")]
        public async Task<ActionResult<ResponseModel<List<ExercicioModel>>>> Criar(ExercicioCreateDto exercicioCreateDto)
        {
            var exercicio = await _exercicio.Criar(exercicioCreateDto);
            return Ok(exercicio);
        }

        [HttpPut("Editar")]
        public async Task<ActionResult<ResponseModel<List<ExercicioModel>>>> Editar(ExercicioEdicaoDto exercicioEdicaoDto)
        {
            var exercicio = await _exercicio.Editar(exercicioEdicaoDto);
            return Ok(exercicio);
        }

        [HttpDelete("Delete/{idExercicio}")]
        public async Task<ActionResult<ResponseModel<List<ExercicioModel>>>> Delete(int idExercicio)
        {
            var exercicio = await _exercicio.Delete(idExercicio);
            return Ok(exercicio);
        }
    }
}
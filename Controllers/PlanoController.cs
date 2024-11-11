
using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.Plano;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.Plano;

namespace WebApiSmartClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanoController : ControllerBase
    {
        private readonly IPlanoInterface _plano;
        public PlanoController(IPlanoInterface plano)
        {
            _plano = plano;
        }

        [HttpGet("Listar")]
        public async Task<ActionResult<ResponseModel<List<PlanoModel>>>> Listar()
        {
            var plano = await _plano.Listar();
            return Ok(plano);
        }

        [HttpGet("BuscarPorId/{idPlano}")]
        public async Task<ActionResult<ResponseModel<List<PlanoModel>>>> BuscarPorId(int idPlano)
        {
            var plano = await _plano.BuscarPorId(idPlano);
            return Ok(plano);
        }

        [HttpPost("Criar")]
        public async Task<ActionResult<ResponseModel<List<PlanoModel>>>> Criar(PlanoCreateDto planoCreateDto)
        {
            var plano = await _plano.Criar(planoCreateDto);
            return Ok(plano);
        }

        [HttpPut("Editar")]
        public async Task<ActionResult<ResponseModel<List<PlanoModel>>>> Editar(PlanoEdicaoDto planoEdicaoDto)
        {
            var plano = await _plano.Editar(planoEdicaoDto);
            return Ok(plano);
        }

        [HttpDelete("Delete/{idPlano}")]
        public async Task<ActionResult<ResponseModel<List<PlanoModel>>>> Delete(int idPlano)
        {
            var plano = await _plano.Delete(idPlano);
            return Ok(plano);
        }
    }
}
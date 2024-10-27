
using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.Profissional;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.Profissional;

namespace WebApiSmartClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfissionalController : ControllerBase
    {
        private readonly IProfissionalInterface _profissional;
        public ProfissionalController(IProfissionalInterface profissional)
        {
            _profissional = profissional;
        }

        [HttpGet("Listar")]
        public async Task<ActionResult<ResponseModel<List<ProfissionalModel>>>> Listar()
        {
            var profissional = await _profissional.Listar();
            return Ok(profissional);
        }

        [HttpGet("BuscarPorId/{idProfissional}")]
        public async Task<ActionResult<ResponseModel<List<ProfissionalModel>>>> BuscarPorId(int idProfissional)
        {
            var profissional = await _profissional.BuscarPorId(idProfissional);
            return Ok(profissional);
        }

        [HttpPost("Criar")]
        public async Task<ActionResult<ResponseModel<List<ProfissionalModel>>>> Criar(ProfissionalCreateDto profissionalCreateDto)
        {
            var profissional = await _profissional.Criar(profissionalCreateDto);
            return Ok(profissional);
        }

        [HttpPut("Editar")]
        public async Task<ActionResult<ResponseModel<List<ProfissionalModel>>>> Editar(ProfissionalEdicaoDto profissionalEdicaoDto)
        {
            var profissional = await _profissional.Editar(profissionalEdicaoDto);
            return Ok(profissional);
        }

        [HttpDelete("Delete/{idProfissional}")]
        public async Task<ActionResult<ResponseModel<List<ProfissionalModel>>>> Delete(int idProfissional)
        {
            var profissional = await _profissional.Delete(idProfissional);
            return Ok(profissional);
        }
    }
}
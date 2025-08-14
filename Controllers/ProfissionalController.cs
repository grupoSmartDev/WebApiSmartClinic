
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
        public async Task<ActionResult<ResponseModel<List<ProfissionalModel>>>> Listar([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            [FromQuery] int? idFiltro = null, [FromQuery] string? nomeFiltro = null,
            [FromQuery] string? cpfFiltro = null, [FromQuery] int? profissaoIdFiltro = null, [FromQuery] bool paginar = true)
        {
            var profissional = await _profissional.Listar(pageNumber, pageSize, idFiltro, nomeFiltro, cpfFiltro, profissaoIdFiltro, paginar);
            
            return Ok(profissional);
        }

        [HttpGet("BuscarPorId/{idProfissional}")]
        public async Task<ActionResult<ResponseModel<List<ProfissionalModel>>>> BuscarPorId(int idProfissional)
        {
            var profissional = await _profissional.BuscarPorId(idProfissional);
            
            return Ok(profissional);
        }

        [HttpPost("Criar")]
        public async Task<ActionResult<ResponseModel<List<ProfissionalModel>>>> Criar(ProfissionalCreateDto profissionalCreateDto, int pageNumber = 1, int pageSize = 10)
        {
            var profissional = await _profissional.Criar(profissionalCreateDto, pageNumber, pageSize);

            return Ok(profissional);
        }

        [HttpPut("Editar")]
        public async Task<ActionResult<ResponseModel<List<ProfissionalModel>>>> Editar(ProfissionalEdicaoDto profissionalEdicaoDto, int pageNumber = 1, int pageSize = 10)
        {
            var userKey = HttpContext.Request.Headers["UserKey"].FirstOrDefault();
            var profissional = await _profissional.Editar(profissionalEdicaoDto, pageNumber, pageSize, userKey);
            
            return Ok(profissional);
        }

        [HttpPut("Ativar")]
        public async Task<ActionResult<ResponseModel<List<ProfissionalModel>>>> Ativar(ProfissionalEdicaoDto profissionalEdicaoDto, int pageNumber = 1, int pageSize = 10)
        {
            var userKey = HttpContext.Request.Headers["UserKey"].FirstOrDefault();
            var profissional = await _profissional.AtivarProfissional(profissionalEdicaoDto, pageNumber, pageSize, userKey);

            return Ok(profissional);
        }

        [HttpDelete("Delete/{idProfissional}")]
        public async Task<ActionResult<ResponseModel<List<ProfissionalModel>>>> Delete(int idProfissional, int pageNumber = 1, int pageSize = 10)
        {
            var profissional = await _profissional.Delete(idProfissional, pageNumber, pageSize);
            
            return Ok(profissional);
        }
    }
}
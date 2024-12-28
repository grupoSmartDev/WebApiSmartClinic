
using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.Profissao;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.Profissao;

namespace WebApiSmartClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfissaoController : ControllerBase
    {
        private readonly IProfissaoInterface _profissao;
        public ProfissaoController(IProfissaoInterface profissao)
        {
            _profissao = profissao;
        }

        [HttpGet("Listar")]
        public async Task<ActionResult<ResponseModel<List<ProfissaoModel>>>> Listar([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] int? codigoFiltro = null, [FromQuery] string? nomeFiltro = null, [FromQuery] bool paginar = true)
        {
            var profissao = await _profissao.Listar(pageNumber, pageSize, codigoFiltro, nomeFiltro, paginar);
            return Ok(profissao);
        }

        [HttpGet("BuscarPorId/{idProfissao}")]
        public async Task<ActionResult<ResponseModel<List<ProfissaoModel>>>> BuscarPorId(int idProfissao)
        {
            var profissao = await _profissao.BuscarPorId(idProfissao);
            return Ok(profissao);
        }

        [HttpPost("Criar")]
        public async Task<ActionResult<ResponseModel<List<ProfissaoModel>>>> Criar(ProfissaoCreateDto profissaoCreateDto, int pageNumber = 1, int pageSize = 10)
        {
            var profissao = await _profissao.Criar(profissaoCreateDto, pageNumber, pageSize);
            return Ok(profissao);
        }

        [HttpPut("Editar")]
        public async Task<ActionResult<ResponseModel<List<ProfissaoModel>>>> Editar(ProfissaoEdicaoDto profissaoEdicaoDto, int pageNumber = 1, int pageSize = 10)
        {
            var profissao = await _profissao.Editar(profissaoEdicaoDto, pageNumber, pageSize);
            return Ok(profissao);
        }

        [HttpDelete("Delete/{idProfissao}")]
        public async Task<ActionResult<ResponseModel<List<ProfissaoModel>>>> Delete(int idProfissao, int pageNumber = 1, int pageSize = 10)
        {
            var profissao = await _profissao.Delete(idProfissao, pageNumber, pageSize);
            return Ok(profissao);
        }
    }
}
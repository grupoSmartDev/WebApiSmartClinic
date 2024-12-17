
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
        public async Task<ActionResult<ResponseModel<List<ProfissaoModel>>>> Listar()
        {
            var profissao = await _profissao.Listar();
            return Ok(profissao);
        }

        [HttpGet("BuscarPorId/{idProfissao}")]
        public async Task<ActionResult<ResponseModel<List<ProfissaoModel>>>> BuscarPorId(int idProfissao)
        {
            var profissao = await _profissao.BuscarPorId(idProfissao);
            return Ok(profissao);
        }

        [HttpPost("Criar")]
        public async Task<ActionResult<ResponseModel<List<ProfissaoModel>>>> Criar(ProfissaoCreateDto profissaoCreateDto)
        {
            var profissao = await _profissao.Criar(profissaoCreateDto);
            return Ok(profissao);
        }

        [HttpPut("Editar")]
        public async Task<ActionResult<ResponseModel<List<ProfissaoModel>>>> Editar(ProfissaoEdicaoDto profissaoEdicaoDto)
        {
            var profissao = await _profissao.Editar(profissaoEdicaoDto);
            return Ok(profissao);
        }

        [HttpDelete("Delete/{idProfissao}")]
        public async Task<ActionResult<ResponseModel<List<ProfissaoModel>>>> Delete(int idProfissao)
        {
            var profissao = await _profissao.Delete(idProfissao);
            return Ok(profissao);
        }
    }
}
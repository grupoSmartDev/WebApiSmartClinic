
using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.Atividade;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.Atividade;

namespace WebApiSmartClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtividadeController : ControllerBase
    {
        private readonly IAtividadeInterface _atividade;
        public AtividadeController(IAtividadeInterface atividade)
        {
            _atividade = atividade;
        }

        [HttpGet("Listar")]
        public async Task<ActionResult<ResponseModel<List<AtividadeModel>>>> Listar()
        {
            var atividade = await _atividade.Listar();
            return Ok(atividade);
        }

        [HttpGet("BuscarPorId/{idAtividade}")]
        public async Task<ActionResult<ResponseModel<List<AtividadeModel>>>> BuscarPorId(int idAtividade)
        {
            var atividade = await _atividade.BuscarPorId(idAtividade);
            return Ok(atividade);
        }

        [HttpPost("Criar")]
        public async Task<ActionResult<ResponseModel<List<AtividadeModel>>>> Criar(AtividadeCreateDto atividadeCreateDto)
        {
            var atividade = await _atividade.Criar(atividadeCreateDto);
            return Ok(atividade);
        }

        [HttpPut("Editar")]
        public async Task<ActionResult<ResponseModel<List<AtividadeModel>>>> Editar(AtividadeEdicaoDto atividadeEdicaoDto)
        {
            var atividade = await _atividade.Editar(atividadeEdicaoDto);
            return Ok(atividade);
        }

        [HttpDelete("Delete/{idAtividade}")]
        public async Task<ActionResult<ResponseModel<List<AtividadeModel>>>> Delete(int idAtividade)
        {
            var atividade = await _atividade.Delete(idAtividade);
            return Ok(atividade);
        }
    }
}
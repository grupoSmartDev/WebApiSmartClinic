
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
        public async Task<ActionResult<ResponseModel<List<AtividadeModel>>>> Listar([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 3, [FromQuery] int? codigoFiltro = null, [FromQuery] string? tituloFiltro = null, string? descricaoFiltro = null, [FromQuery] bool paginar = true)
        {
            var atividade = await _atividade.Listar(pageNumber, pageSize, codigoFiltro, tituloFiltro, descricaoFiltro, paginar);
            return Ok(atividade);
        }

        [HttpGet("BuscarPorId/{idAtividade}")]
        public async Task<ActionResult<ResponseModel<List<AtividadeModel>>>> BuscarPorId(int idAtividade)
        {
            var atividade = await _atividade.BuscarPorId(idAtividade);
            return Ok(atividade);
        }

        [HttpPost("Criar")]
        public async Task<ActionResult<ResponseModel<List<AtividadeModel>>>> Criar(AtividadeCreateDto atividadeCreateDto, int pageNumber = 1, int pageSize = 10)
        {
            var atividade = await _atividade.Criar(atividadeCreateDto, pageNumber, pageSize);
            return Ok(atividade);
        }

        [HttpPut("Editar")]
        public async Task<ActionResult<ResponseModel<List<AtividadeModel>>>> Editar(AtividadeEdicaoDto atividadeEdicaoDto, int pageNumber = 1, int pageSize = 10)
        {
            var atividade = await _atividade.Editar(atividadeEdicaoDto, pageNumber, pageSize);
            return Ok(atividade);
        }

        [HttpDelete("Delete/{idAtividade}")]
        public async Task<ActionResult<ResponseModel<List<AtividadeModel>>>> Delete(int idAtividade, int pageNumber = 1, int pageSize = 10)
        {
            var atividade = await _atividade.Delete(idAtividade, pageNumber, pageSize);
            return Ok(atividade);
        }
    }
}
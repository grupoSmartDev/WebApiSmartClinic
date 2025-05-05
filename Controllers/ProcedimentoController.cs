
using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.Procedimento;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.Procedimento;

namespace WebApiSmartClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcedimentoController : ControllerBase
    {
        private readonly IProcedimentoInterface _procedimento;
        public ProcedimentoController(IProcedimentoInterface procedimento)
        {
            _procedimento = procedimento;
        }

        [HttpGet("Listar")]
        public async Task<ActionResult> Listar([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] int? idFiltro = null, [FromQuery] string? nomeFiltro = null, [FromQuery] string? descricaoFiltro = null, [FromQuery] bool paginar = true)
        {
            var response = await _procedimento.Listar(pageNumber, pageSize, idFiltro, nomeFiltro, descricaoFiltro, paginar);

            if (!response.Status)
                return BadRequest(response);

            return Ok(response);
        }


        [HttpGet("BuscarPorId/{idProcedimento}")]
        public async Task<ActionResult<ResponseModel<List<ProcedimentoModel>>>> BuscarPorId(int idProcedimento)
        {
            var procedimento = await _procedimento.BuscarPorId(idProcedimento);
            return Ok(procedimento);
        }

        [HttpPost("Criar")]
        public async Task<ActionResult<ResponseModel<List<ProcedimentoModel>>>> Criar(ProcedimentoCreateDto procedimentoCreateDto, int pageNumber = 1, int pageSize = 10)
        {
            var response = await _procedimento.Criar(procedimentoCreateDto, pageNumber, pageSize);
            if (!response.Status)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("Editar")]
        public async Task<ActionResult<ResponseModel<List<ProcedimentoModel>>>> Editar(ProcedimentoEdicaoDto procedimentoEdicaoDto, int pageNumber = 1, int pageSize = 10)
        {
            var response = await _procedimento.Editar(procedimentoEdicaoDto, pageNumber, pageSize);
            if (!response.Status)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("Delete/{idProcedimento}")]
        public async Task<ActionResult<ResponseModel<List<ProcedimentoModel>>>> Delete(int idProcedimento, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var response = await _procedimento.Delete(idProcedimento, pageNumber, pageSize);
            if (!response.Status)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
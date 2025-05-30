
using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.Convenio;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.Convenio;

namespace WebApiSmartClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConvenioController : ControllerBase
    {
        private readonly IConvenioInterface _convenio;
        public ConvenioController(IConvenioInterface convenio)
        {
            _convenio = convenio;
        }

        [HttpGet("Listar")]
        public async Task<ActionResult<ResponseModel<List<ConvenioModel>>>> Listar([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] int? idFiltro = null, [FromQuery] string? nomeFiltro = null, [FromQuery] string? telefoneFiltro = null, [FromQuery] string? registroAvsFiltro = null, [FromQuery] bool paginar = true)
        {
            var convenio = await _convenio.Listar(pageNumber, pageSize, idFiltro, nomeFiltro, telefoneFiltro, registroAvsFiltro, paginar);
            return Ok(convenio);
        }

        [HttpGet("BuscarPorId/{idConvenio}")]
        public async Task<ActionResult<ResponseModel<List<ConvenioModel>>>> BuscarPorId(int idConvenio)
        {
            var convenio = await _convenio.BuscarPorId(idConvenio);
            return Ok(convenio);
        }

        [HttpPost("Criar")]
        public async Task<ActionResult<ResponseModel<List<ConvenioModel>>>> Criar(ConvenioCreateDto convenioCreateDto, int pageNumber = 1, int pageSize = 10)
        {
            var convenio = await _convenio.Criar(convenioCreateDto, pageNumber, pageSize);
            return Ok(convenio);
        }

        [HttpPut("Editar")]
        public async Task<ActionResult<ResponseModel<List<ConvenioModel>>>> Editar(ConvenioEdicaoDto convenioEdicaoDto, int pageNumber = 1, int pageSize = 10)
        {
            var convenio = await _convenio.Editar(convenioEdicaoDto, pageNumber, pageSize);
            return Ok(convenio);
        }

        [HttpDelete("Delete/{idConvenio}")]
        public async Task<ActionResult<ResponseModel<List<ConvenioModel>>>> Delete(int idConvenio, int pageNumber = 1, int pageSize = 10)
        {
            var convenio = await _convenio.Delete(idConvenio, pageNumber, pageSize);
            return Ok(convenio);
        }
    }
}
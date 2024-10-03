
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
        public async Task<ActionResult<ResponseModel<List<ConvenioModel>>>> Listar()
        {
            var convenio = await _convenio.Listar();
            return Ok(convenio);
        }

        [HttpGet("BuscarPorId/{idConvenio}")]
        public async Task<ActionResult<ResponseModel<List<ConvenioModel>>>> BuscarPorId(int idConvenio)
        {
            var convenio = await _convenio.BuscarPorId(idConvenio);
            return Ok(convenio);
        }

        [HttpPost("Criar")]
        public async Task<ActionResult<ResponseModel<List<ConvenioModel>>>> Criar(ConvenioCreateDto convenioCreateDto)
        {
            var convenio = await _convenio.Criar(convenioCreateDto);
            return Ok(convenio);
        }

        [HttpPut("Editar")]
        public async Task<ActionResult<ResponseModel<List<ConvenioModel>>>> Editar(ConvenioEdicaoDto convenioEdicaoDto)
        {
            var convenio = await _convenio.Editar(convenioEdicaoDto);
            return Ok(convenio);
        }

        [HttpDelete("Delete/{idConvenio}")]
        public async Task<ActionResult<ResponseModel<List<ConvenioModel>>>> Delete(int idConvenio)
        {
            var convenio = await _convenio.Delete(idConvenio);
            return Ok(convenio);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.Conselho;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.Conselho;

namespace WebApiSmartClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConselhoController : ControllerBase
    {
        private readonly IConselhoInterface _conselho;
        public ConselhoController(IConselhoInterface conselho)
        {
            _conselho = conselho;
        }

        [HttpGet("Listar")]
        public async Task<ActionResult<ResponseModel<List<ConselhoModel>>>> Listar([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] int? codigoFiltro = null, [FromQuery] string? nomeFiltro = null, [FromQuery] string? siglaFiltro = null, [FromQuery] bool paginar = true)
        {
            var conselho = await _conselho.Listar(pageNumber, pageSize, codigoFiltro, nomeFiltro, siglaFiltro, paginar);
            return Ok(conselho);
        }

        [HttpGet("BuscarPorId/{idConselho}")]
        public async Task<ActionResult<ResponseModel<List<ConselhoModel>>>> BuscarPorId(int idConselho)
        {
            var conselho = await _conselho.BuscarPorId(idConselho);
            return Ok(conselho);
        }

        [HttpPost("Criar")]
        public async Task<ActionResult<ResponseModel<List<ConselhoModel>>>> Criar(ConselhoCreateDto conselhoCreateDto, int pageNumber = 1, int pageSize = 10)
        {
            var conselho = await _conselho.Criar(conselhoCreateDto, pageNumber, pageSize);
            return Ok(conselho);
        }

        [HttpPut("Editar")]
        public async Task<ActionResult<ResponseModel<List<ConselhoModel>>>> Editar(ConselhoEdicaoDto conselhoEdicaoDto, int pageNumber = 1, int pageSize = 10)
        {
            var conselho = await _conselho.Editar(conselhoEdicaoDto, pageNumber, pageSize);
            return Ok(conselho);
        }

        [HttpDelete("Delete/{idConselho}")]
        public async Task<ActionResult<ResponseModel<List<ConselhoModel>>>> Delete(int idConselho, int pageNumber = 1, int pageSize = 10)
        {
            var conselho = await _conselho.Delete(idConselho, pageNumber, pageSize);
            return Ok(conselho);
        }
    }
}
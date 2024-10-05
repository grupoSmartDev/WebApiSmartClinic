
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
        public async Task<ActionResult<ResponseModel<List<ConselhoModel>>>> Listar()
        {
            var conselho = await _conselho.Listar();
            return Ok(conselho);
        }

        [HttpGet("BuscarPorId/{idConselho}")]
        public async Task<ActionResult<ResponseModel<List<ConselhoModel>>>> BuscarPorId(int idConselho)
        {
            var conselho = await _conselho.BuscarPorId(idConselho);
            return Ok(conselho);
        }

        [HttpPost("Criar")]
        public async Task<ActionResult<ResponseModel<List<ConselhoModel>>>> Criar(ConselhoCreateDto conselhoCreateDto)
        {
            var conselho = await _conselho.Criar(conselhoCreateDto);
            return Ok(conselho);
        }

        [HttpPut("Editar")]
        public async Task<ActionResult<ResponseModel<List<ConselhoModel>>>> Editar(ConselhoEdicaoDto conselhoEdicaoDto)
        {
            var conselho = await _conselho.Editar(conselhoEdicaoDto);
            return Ok(conselho);
        }

        [HttpDelete("Delete/{idConselho}")]
        public async Task<ActionResult<ResponseModel<List<ConselhoModel>>>> Delete(int idConselho)
        {
            var conselho = await _conselho.Delete(idConselho);
            return Ok(conselho);
        }
    }
}
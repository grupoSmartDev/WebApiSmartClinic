
using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.FichaAvaliacao;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.FichaAvaliacao;

namespace WebApiSmartClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FichaAvaliacaoController : ControllerBase
    {
        private readonly IFichaAvaliacaoInterface _fichaavaliacao;
        public FichaAvaliacaoController(IFichaAvaliacaoInterface fichaavaliacao)
        {
            _fichaavaliacao = fichaavaliacao;
        }

        [HttpGet("Listar")]
        public async Task<ActionResult<ResponseModel<List<FichaAvaliacaoModel>>>> Listar()
        {
            var fichaavaliacao = await _fichaavaliacao.Listar();
            return Ok(fichaavaliacao);
        }

        [HttpGet("BuscarPorId/{idFichaAvaliacao}")]
        public async Task<ActionResult<ResponseModel<List<FichaAvaliacaoModel>>>> BuscarPorId(int idFichaAvaliacao)
        {
            var fichaavaliacao = await _fichaavaliacao.BuscarPorId(idFichaAvaliacao);
            return Ok(fichaavaliacao);
        }
        [HttpGet("BuscarPorIdPaciente")]
        public async Task<ActionResult<ResponseModel<List<FichaAvaliacaoModel>>>> BuscarPorIdPaciente([FromQuery] int pacienteId)
        {
            var fichaavaliacao = await _fichaavaliacao.BuscarPorIdPaciente(pacienteId);
            return Ok(fichaavaliacao);
        }

        [HttpGet("BuscarPorIdPaciente")]
        public async Task<ActionResult<ResponseModel<List<FichaAvaliacaoModel>>>> BuscarPorIdPaciente([FromQuery] int pacienteId)
        {
            var fichaavaliacao = await _fichaavaliacao.BuscarPorIdPaciente(pacienteId);
            return Ok(fichaavaliacao);
        }

        [HttpPost("Criar")]
        public async Task<ActionResult<ResponseModel<List<FichaAvaliacaoModel>>>> Criar(FichaAvaliacaoCreateDto fichaavaliacaoCreateDto)
        {
            var fichaavaliacao = await _fichaavaliacao.Criar(fichaavaliacaoCreateDto);
            return Ok(fichaavaliacao);
        }

        [HttpPut("Editar")]
        public async Task<ActionResult<ResponseModel<List<FichaAvaliacaoModel>>>> Editar(FichaAvaliacaoEdicaoDto fichaavaliacaoEdicaoDto)
        {
            var fichaavaliacao = await _fichaavaliacao.Editar(fichaavaliacaoEdicaoDto);
            return Ok(fichaavaliacao);
        }

        [HttpDelete("Delete/{idFichaAvaliacao}")]
        public async Task<ActionResult<ResponseModel<List<FichaAvaliacaoModel>>>> Delete(int idFichaAvaliacao)
        {
            var fichaavaliacao = await _fichaavaliacao.Delete(idFichaAvaliacao);
            return Ok(fichaavaliacao);
        }
    }
}
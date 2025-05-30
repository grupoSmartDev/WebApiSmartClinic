
using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.Agenda;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.Agenda;

namespace WebApiSmartClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendaController : ControllerBase
    {
        private readonly IAgendaInterface _agenda;
        public AgendaController(IAgendaInterface agenda)
        {
            _agenda = agenda;
        }

        [HttpGet("Listar")]
        public async Task<ActionResult<ResponseModel<List<AgendaModel>>>> Listar()
        {
            var agenda = await _agenda.Listar();
            return Ok(agenda);
        }

        [HttpGet("BuscarPorId/{idAgenda}")]
        public async Task<ActionResult<ResponseModel<List<AgendaModel>>>> BuscarPorId(int idAgenda)
        {
            var agenda = await _agenda.BuscarPorId(idAgenda);
            return Ok(agenda);
        }

        [HttpPost("Criar")]
        public async Task<ActionResult<ResponseModel<List<AgendaModel>>>> Criar(AgendaCreateDto agendaCreateDto)
        {
            var agenda = await _agenda.Criar(agendaCreateDto);
            return Ok(agenda);
        }

        [HttpPut("Editar")]
        public async Task<ActionResult<ResponseModel<List<AgendaModel>>>> Editar(AgendaEdicaoDto agendaEdicaoDto)
        {
            var agenda = await _agenda.Editar(agendaEdicaoDto);
            return Ok(agenda);
        }

        [HttpPut("AtualizarStatus")]
        public async Task<ActionResult<ResponseModel<List<AgendaModel>>>> AtualizarStatus([FromQuery] int id, [FromQuery] int statusNovo)
        {
            var agenda = await _agenda.AtualizarStatus(id, statusNovo);
            return Ok(agenda);
        } 
        
        [HttpPut("Reagendar")]
        public async Task<ActionResult<ResponseModel<List<AgendaModel>>>> Reagendar([FromQuery] int id, [FromQuery] int statusNovo, [FromQuery] DateTime dataNova, [FromQuery] string horaInicioNovo, [FromQuery] string horaFimNovo)
        {
            var agenda = await _agenda.Reagendar(id, statusNovo, dataNova, horaInicioNovo, horaFimNovo);
            return Ok(agenda);
        }

        [HttpDelete("Delete/{idAgenda}")]
        public async Task<ActionResult<ResponseModel<List<AgendaModel>>>> Delete(int idAgenda)
        {
            var agenda = await _agenda.Delete(idAgenda);
            return Ok(agenda);
        }

        [HttpGet("ObterContadoresDashboard")]
        public async Task<IActionResult> ObterContadoresDashboard([FromQuery] int? profissionalId, [FromQuery] DateTime? dataInicio, [FromQuery] DateTime? dataFim)
        {
            try
            {
                var resultado = await _agenda.ObterContadoresDashboard(profissionalId, dataInicio, dataFim);

                if (!resultado.Status)
                    return BadRequest(new { mensagem = resultado.Mensagem });

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro interno no servidor.", detalhes = ex.Message });
            }
        }

        [HttpGet("ListarGeral")]
        public async Task<ActionResult<ResponseModel<List<AgendaModel>>>> ListarGeral([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] int? idFiltro = null, [FromQuery] string? pacienteIdFiltro = null, [FromQuery] string? profissionalIdFiltro = null, [FromQuery] string? statusIdFiltro = null, [FromQuery] DateTime? dataFiltroInicio = null, [FromQuery] DateTime? dataFiltroFim = null,[FromQuery] bool paginar = true)
        {
            var agenda = await _agenda.ListarGeral(pageNumber,pageSize,idFiltro,pacienteIdFiltro,profissionalIdFiltro,statusIdFiltro,dataFiltroInicio,dataFiltroFim,paginar);
            return Ok(agenda);
        }
    }
}

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

        [HttpDelete("Delete/{idAgenda}")]
        public async Task<ActionResult<ResponseModel<List<AgendaModel>>>> Delete(int idAgenda)
        {
            var agenda = await _agenda.Delete(idAgenda);
            return Ok(agenda);
        }
    }
}
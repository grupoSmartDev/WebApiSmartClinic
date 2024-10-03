
using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.Paciente;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.Paciente;

namespace WebApiSmartClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteInterface _paciente;
        public PacienteController(IPacienteInterface paciente)
        {
            _paciente = paciente;
        }

        [HttpGet("Listar")]
        public async Task<ActionResult<ResponseModel<List<PacienteModel>>>> Listar()
        {
            var paciente = await _paciente.Listar();
            return Ok(paciente);
        }

        [HttpGet("BuscarPorId/{idPaciente}")]
        public async Task<ActionResult<ResponseModel<List<PacienteModel>>>> BuscarPorId(int idPaciente)
        {
            var paciente = await _paciente.BuscarPorId(idPaciente);
            return Ok(paciente);
        }

        [HttpPost("Criar")]
        public async Task<ActionResult<ResponseModel<List<PacienteModel>>>> Criar(PacienteCreateDto pacienteCreateDto)
        {
            var paciente = await _paciente.Criar(pacienteCreateDto);
            return Ok(paciente);
        }

        [HttpPut("Editar")]
        public async Task<ActionResult<ResponseModel<List<PacienteModel>>>> Editar(PacienteEdicaoDto pacienteEdicaoDto)
        {
            var paciente = await _paciente.Editar(pacienteEdicaoDto);
            return Ok(paciente);
        }

        [HttpDelete("Delete/{idPaciente}")]
        public async Task<ActionResult<ResponseModel<List<PacienteModel>>>> Delete(int idPaciente)
        {
            var paciente = await _paciente.Delete(idPaciente);
            return Ok(paciente);
        }
    }
}
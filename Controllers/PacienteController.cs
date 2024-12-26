
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
        public async Task<ActionResult<ResponseModel<List<PacienteModel>>>> Listar(int pageNumber = 1, int pageSize = 10, int? codigoFiltro = null, string? nomeFiltro = null, string? cpfFiltro = null, string? celularFiltro = null, bool paginar = true)
        {
            var paciente = await _paciente.Listar(pageNumber, pageSize, codigoFiltro, nomeFiltro, cpfFiltro, celularFiltro, paginar);
            return Ok(paciente);
        }

        [HttpGet("BuscarPorId/{idPaciente}")]
        public async Task<ActionResult<ResponseModel<List<PacienteModel>>>> BuscarPorId(int idPaciente)
        {
            var paciente = await _paciente.BuscarPorId(idPaciente);
            return Ok(paciente);
        }

        [HttpPost("Criar")]
        public async Task<ActionResult<ResponseModel<List<PacienteModel>>>> Criar(PacienteCreateDto pacienteCreateDto, int pageNumber = 1, int pageSize = 10)
        {
            var paciente = await _paciente.Criar(pacienteCreateDto, pageNumber, pageSize);
            return Ok(paciente);
        }

        [HttpPut("Editar")]
        public async Task<ActionResult<ResponseModel<List<PacienteModel>>>> Editar(PacienteEdicaoDto pacienteEdicaoDto, int pageNumber = 1, int pageSize = 10)
        {
            var paciente = await _paciente.Editar(pacienteEdicaoDto, pageNumber, pageSize);
            return Ok(paciente);
        }

        [HttpDelete("Delete/{idPaciente}")]
        public async Task<ActionResult<ResponseModel<List<PacienteModel>>>> Delete(int idPaciente, int pageNumber = 1, int pageSize = 10)
        {
            var paciente = await _paciente.Delete(idPaciente, pageNumber, pageSize);
            return Ok(paciente);
        }
    }
}
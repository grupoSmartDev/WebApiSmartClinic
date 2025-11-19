using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.Pacote;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.Pacote;

namespace WebApiSmartClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacoteController : ControllerBase
    {
        private readonly IPacoteInterface _pacoteInterface;

        public PacoteController(IPacoteInterface pacoteInterface)
        {
            _pacoteInterface = pacoteInterface;
        }

        // ==================== PACOTE (TEMPLATE) ====================

        [HttpGet("Listar")]
        public async Task<ActionResult<ResponseModel<List<PacoteModel>>>> Listar(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? descricaoFiltro = null,
            [FromQuery] bool paginar = true)
        {
            var resposta = await _pacoteInterface.Listar(pageNumber, pageSize, descricaoFiltro, paginar);
            return Ok(resposta);
        }

        [HttpGet("BuscarPorId/{id}")]
        public async Task<ActionResult<ResponseModel<PacoteModel>>> BuscarPorId(int id)
        {
            var resposta = await _pacoteInterface.BuscarPorId(id);
            return Ok(resposta);
        }

        [HttpPost("Criar")]
        public async Task<ActionResult<ResponseModel<List<PacoteModel>>>> Criar(
            [FromBody] PacoteCreateDto dto,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var resposta = await _pacoteInterface.Criar(dto, pageNumber, pageSize);
            return Ok(resposta);
        }

        [HttpPut("Editar")]
        public async Task<ActionResult<ResponseModel<List<PacoteModel>>>> Editar(
            [FromBody] PacoteEdicaoDto dto,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var resposta = await _pacoteInterface.Editar(dto, pageNumber, pageSize);
            return Ok(resposta);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<ResponseModel<List<PacoteModel>>>> Delete(
            int id,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var resposta = await _pacoteInterface.Delete(id, pageNumber, pageSize);
            return Ok(resposta);
        }

        // ==================== VENDA DE PACOTE ====================

        [HttpPost("VenderPacote")]
        public async Task<ActionResult<ResponseModel<PacotePacienteModel>>> VenderPacote([FromBody] PacoteVendaDto dto)
        {
            var resposta = await _pacoteInterface.VenderPacote(dto);
            return Ok(resposta);
        }

        // ==================== PACOTE DO PACIENTE ====================

        [HttpGet("ListarPacotesPaciente/{pacienteId}")]
        public async Task<ActionResult<ResponseModel<List<PacotePacienteModel>>>> ListarPacotesPaciente(int pacienteId)
        {
            var resposta = await _pacoteInterface.ListarPacotesPaciente(pacienteId);
            return Ok(resposta);
        }

        [HttpGet("BuscarPacotePacientePorId/{id}")]
        public async Task<ActionResult<ResponseModel<PacotePacienteModel>>> BuscarPacotePacientePorId(int id)
        {
            var resposta = await _pacoteInterface.BuscarPacotePacientePorId(id);
            return Ok(resposta);
        }

        // ==================== USO DO PACOTE ====================

        [HttpPost("ConsumirSessao")]
        public async Task<ActionResult<ResponseModel<PacoteUsoModel>>> ConsumirSessao([FromBody] PacoteUsoDto dto)
        {
            var resposta = await _pacoteInterface.ConsumirSessao(dto);
            return Ok(resposta);
        }

        [HttpGet("ListarHistoricoUso/{pacotePacienteId}")]
        public async Task<ActionResult<ResponseModel<List<PacoteUsoModel>>>> ListarHistoricoUso(int pacotePacienteId)
        {
            var resposta = await _pacoteInterface.ListarHistoricoUso(pacotePacienteId);
            return Ok(resposta);
        }

        [HttpPost("EstornarUso/{idUso}")]
        public async Task<ActionResult<ResponseModel<string>>> EstornarUso(int idUso)
        {
            var resposta = await _pacoteInterface.EstornarUso(idUso);
            return Ok(resposta);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.Procedimento;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.Procedimento;

namespace WebApiSmartClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcedimentoController : ControllerBase
    {
        private readonly IProcedimentoInterface _procedimento;
        public ProcedimentoController(IProcedimentoInterface procedimento)
        {
            _procedimento = procedimento;
        }

        [HttpGet("Listar")]
        public async Task<ActionResult<ResponseModel<List<ProcedimentoModel>>>> Listar()
        {
            var procedimento = await _procedimento.Listar();
            return Ok(procedimento);
        }

        [HttpGet("BuscarPorId/{idProcedimento}")]
        public async Task<ActionResult<ResponseModel<List<ProcedimentoModel>>>> BuscarPorId(int idProcedimento)
        {
            var procedimento = await _procedimento.BuscarPorId(idProcedimento);
            return Ok(procedimento);
        }

        [HttpPost("Criar")]
        public async Task<ActionResult<ResponseModel<List<ProcedimentoModel>>>> Criar(ProcedimentoCreateDto procedimentoCreateDto)
        {
            var procedimento = await _procedimento.Criar(procedimentoCreateDto);
            return Ok(procedimento);
        }

        [HttpPut("Editar")]
        public async Task<ActionResult<ResponseModel<List<ProcedimentoModel>>>> Editar(ProcedimentoEdicaoDto procedimentoEdicaoDto)
        {
            var procedimento = await _procedimento.Editar(procedimentoEdicaoDto);
            return Ok(procedimento);
        }

        [HttpDelete("Delete/{idProcedimento}")]
        public async Task<ActionResult<ResponseModel<List<ProcedimentoModel>>>> Delete(int idProcedimento)
        {
            var procedimento = await _procedimento.Delete(idProcedimento);
            return Ok(procedimento);
        }
    }
}
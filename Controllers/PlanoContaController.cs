
using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.PlanoConta;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.PlanoConta;

namespace WebApiSmartClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanoContaController : ControllerBase
    {
        private readonly IPlanoContaInterface _planoconta;
        public PlanoContaController(IPlanoContaInterface planoconta)
        {
            _planoconta = planoconta;
        }

        [HttpGet("Listar")]
        public async Task<ActionResult<ResponseModel<List<PlanoContaModel>>>> Listar(int pageNumber = 1, int pageSize = 10, string? codigoFiltro = null, string? nomeFiltro = null, Tipo? tipoFiltro = null, bool? inativoFiltro = null, bool paginar = true)
        {
            var planoconta = await _planoconta.Listar(pageNumber, pageSize, codigoFiltro, nomeFiltro, tipoFiltro, inativoFiltro, paginar);
            return Ok(planoconta);
        }

        [HttpGet("BuscarPorId/{idPlanoConta}")]
        public async Task<ActionResult<ResponseModel<List<PlanoContaModel>>>> BuscarPorId(int idPlanoConta)
        {
            var planoconta = await _planoconta.BuscarPorId(idPlanoConta);
            return Ok(planoconta);
        }

        [HttpPost("Criar")]
        public async Task<ActionResult<ResponseModel<List<PlanoContaModel>>>> Criar(PlanoContaCreateDto planocontaCreateDto, int pageNumber = 1, int pageSize = 10)
        {
            var planoconta = await _planoconta.Criar(planocontaCreateDto, pageNumber, pageSize);
            return Ok(planoconta);
        }

        [HttpPut("Editar")]
        public async Task<ActionResult<ResponseModel<List<PlanoContaModel>>>> Editar(PlanoContaEdicaoDto planocontaEdicaoDto, int pageNumber = 1, int pageSize = 10)
        {
            var planoconta = await _planoconta.Editar(planocontaEdicaoDto, pageNumber, pageSize);
            return Ok(planoconta);
        }

        [HttpDelete("Delete/{idPlanoConta}")]
        public async Task<ActionResult<ResponseModel<List<PlanoContaModel>>>> Delete(int idPlanoConta, int pageNumber = 1, int pageSize = 10)
        {
            var planoconta = await _planoconta.Delete(idPlanoConta, pageNumber, pageSize);
            return Ok(planoconta);
        }
    }
}
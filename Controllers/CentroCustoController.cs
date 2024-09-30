
using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.CentroCusto;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.CentroCusto;

namespace WebApiSmartClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CentroCustoController : ControllerBase
    {
        private readonly ICentroCustoInterface _centrocusto;
        public CentroCustoController(ICentroCustoInterface centrocusto)
        {
            _centrocusto = centrocusto;
        }

        [HttpGet("Listar")]
        public async Task<ActionResult<ResponseModel<List<CentroCustoModel>>>> ListarCentroCusto()
        {
            var centrocusto = await _centrocusto.ListarCentroCusto();
            return Ok(centrocusto);
        }

        [HttpGet("BuscarPorId/{idCentroCusto}")]
        public async Task<ActionResult<ResponseModel<List<CentroCustoModel>>>> BuscarCentroCustoPorId(int idCentroCusto)
        {
            var centrocusto = await _centrocusto.BuscarCentroCustoPorId(idCentroCusto);
            return Ok(centrocusto);
        }

        [HttpPost("Criar")]
        public async Task<ActionResult<ResponseModel<List<CentroCustoModel>>>> CriarCentroCusto(CentroCustoCreateDto centrocustoCreateDto)
        {
            var centrocusto = await _centrocusto.CriarCentroCusto(centrocustoCreateDto);
            return Ok(centrocusto);
        }

        [HttpPut("Editar")]
        public async Task<ActionResult<ResponseModel<List<CentroCustoModel>>>> EditarCentroCusto(CentroCustoEdicaoDto centrocustoEdicaoDto)
        {
            var centrocusto = await _centrocusto.EditarCentroCusto(centrocustoEdicaoDto);
            return Ok(centrocusto);
        }

        [HttpDelete("Delete/{idCentroCusto}")]
        public async Task<ActionResult<ResponseModel<List<CentroCustoModel>>>> DeleteCentroCusto(int idCentroCusto)
        {
            var centrocusto = await _centrocusto.DeleteCentroCusto(idCentroCusto);
            return Ok(centrocusto);
        }
    }
}
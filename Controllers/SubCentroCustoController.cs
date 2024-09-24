using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Dto.SubCentroCusto;
using WebApiSmartClinic.Services.SubCentroCusto;

namespace WebApiSmartClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCentroCustoController : ControllerBase
    {
        private readonly ISubCentroCustoInterface _subCentroCustoInterface;

        public SubCentroCustoController(ISubCentroCustoInterface subCentroCusto)
        {
            _subCentroCustoInterface = subCentroCusto;
        }

        [HttpPost("Criar")]
        public async Task<ActionResult<ResponseModel<SubCentroCustoModel>>> Criar(SubCentroCustoCreateDto dto)
        {
            var resposta = await _subCentroCustoInterface.Criar(dto);
            return Ok(resposta);
        }

        [HttpPut("Editar")]
        public async Task<ActionResult<ResponseModel<SubCentroCustoModel>>> Editar(SubCentroCustoEdicaoDto dto)
        {
            var resposta = await _subCentroCustoInterface.Editar(dto);
            return Ok(resposta);
        }

        [HttpGet("Listar")]
        public async Task<ActionResult<ResponseModel<List<SubCentroCustoModel>>>> Listar()
        {
            var resposta = await _subCentroCustoInterface.Listar();
            return Ok(resposta);
        }

        [HttpGet("BuscarPorId/{id}")]
        public async Task<ActionResult<ResponseModel<SubCentroCustoModel>>> BuscarPorId(int id)
        {
            var resposta = await _subCentroCustoInterface.BuscarPorId(id);
            return Ok(resposta);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<ResponseModel<List<SubCentroCustoModel>>>> Delete(int id)
        {
            var resposta = await _subCentroCustoInterface.Delete(id);
            return Ok(resposta);
        }
    }
}

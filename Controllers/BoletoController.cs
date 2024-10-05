
using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.Boleto;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.Boleto;

namespace WebApiSmartClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoletoController : ControllerBase
    {
        private readonly IBoletoInterface _boleto;
        public BoletoController(IBoletoInterface boleto)
        {
            _boleto = boleto;
        }

        [HttpGet("Listar")]
        public async Task<ActionResult<ResponseModel<List<BoletoModel>>>> Listar()
        {
            var boleto = await _boleto.Listar();
            return Ok(boleto);
        }

        [HttpGet("BuscarPorId/{idBoleto}")]
        public async Task<ActionResult<ResponseModel<List<BoletoModel>>>> BuscarPorId(int idBoleto)
        {
            var boleto = await _boleto.BuscarPorId(idBoleto);
            return Ok(boleto);
        }

        [HttpPost("Criar")]
        public async Task<ActionResult<ResponseModel<List<BoletoModel>>>> Criar(BoletoCreateDto boletoCreateDto)
        {
            var boleto = await _boleto.Criar(boletoCreateDto);
            return Ok(boleto);
        }

        [HttpPut("Editar")]
        public async Task<ActionResult<ResponseModel<List<BoletoModel>>>> Editar(BoletoEdicaoDto boletoEdicaoDto)
        {
            var boleto = await _boleto.Editar(boletoEdicaoDto);
            return Ok(boleto);
        }

        [HttpDelete("Delete/{idBoleto}")]
        public async Task<ActionResult<ResponseModel<List<BoletoModel>>>> Delete(int idBoleto)
        {
            var boleto = await _boleto.Delete(idBoleto);
            return Ok(boleto);
        }
    }
}
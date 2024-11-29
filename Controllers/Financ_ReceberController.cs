
using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.Financ_Receber;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.Financ_Receber;

namespace WebApiSmartClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Financ_ReceberController : ControllerBase
    {
        private readonly IFinanc_ReceberInterface _financ_receber;
        public Financ_ReceberController(IFinanc_ReceberInterface financ_receber)
        {
            _financ_receber = financ_receber;
        }

        [HttpGet("Listar")]
        public async Task<ActionResult<ResponseModel<List<Financ_ReceberModel>>>> Listar()
        {
            var financ_receber = await _financ_receber.Listar();
            return Ok(financ_receber);
        }

        [HttpGet("BuscarPorId/{idFinanc_Receber}")]
        public async Task<ActionResult<ResponseModel<List<Financ_ReceberModel>>>> BuscarPorId(int idFinanc_Receber)
        {
            var financ_receber = await _financ_receber.BuscarPorId(idFinanc_Receber);
            return Ok(financ_receber);
        }

        [HttpPost("Criar")]
        public async Task<ActionResult<ResponseModel<List<Financ_ReceberModel>>>> Criar(Financ_ReceberCreateDto financ_receberCreateDto)
        {
            var financ_receber = await _financ_receber.Criar(financ_receberCreateDto);
            return Ok(financ_receber);
        }

        [HttpPut("Editar")]
        public async Task<ActionResult<ResponseModel<List<Financ_ReceberModel>>>> Editar(Financ_ReceberEdicaoDto financ_receberEdicaoDto)
        {
            var financ_receber = await _financ_receber.Editar(financ_receberEdicaoDto);
            return Ok(financ_receber);
        }

        [HttpDelete("Delete/{idFinanc_Receber}")]
        public async Task<ActionResult<ResponseModel<List<Financ_ReceberModel>>>> Delete(int idFinanc_Receber)
        {
            var financ_receber = await _financ_receber.Delete(idFinanc_Receber);
            return Ok(financ_receber);
        }

        [HttpGet("BuscarContasEmAberto")]
        public async Task<ActionResult<ResponseModel<List<Financ_ReceberModel>>>> BuscarContasEmAberto()
        {
            var contasEmAberto = await _financ_receber.BuscarContasEmAberto();
            return Ok(contasEmAberto);
        }

        [HttpPut("QuitarParcela/{idParcela}")]
        public async Task<ActionResult<ResponseModel<Financ_ReceberSubModel>>> QuitarParcela(
            int idParcela,
            [FromQuery] decimal valorPago,
            [FromQuery] DateTime dataPagamento)
        {
            var parcelaQuitada = await _financ_receber.QuitarParcela(idParcela, valorPago, dataPagamento);
            return Ok(parcelaQuitada);
        }

        [HttpGet("CalcularTotalRecebiveis")]
        public async Task<ActionResult<ResponseModel<decimal>>> CalcularTotalRecebiveis()
        {
            var totalRecebiveis = await _financ_receber.CalcularTotalRecebiveis();
            return Ok(totalRecebiveis);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.TipoPagamento;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.TipoPagamento;

namespace WebApiSmartClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoPagamentoController : ControllerBase
    {
        private readonly ITipoPagamentoInterface _tipopagamento;
        public TipoPagamentoController(ITipoPagamentoInterface tipopagamento)
        {
            _tipopagamento = tipopagamento;
        }

        [HttpGet("ListarTipoPagamento")]
        public async Task<ActionResult<ResponseModel<List<TipoPagamentoModel>>>> ListarTipoPagamento()
        {
            var tipopagamento = await _tipopagamento.ListarTipoPagamento();
            return Ok(tipopagamento);
        }

        [HttpGet("BuscarTipoPagamentoPorId/{idTipoPagamento}")]
        public async Task<ActionResult<ResponseModel<List<TipoPagamentoModel>>>> BuscarTipoPagamentoPorId(int idTipoPagamento)
        {
            var tipopagamento = await _tipopagamento.BuscarTipoPagamentoPorId(idTipoPagamento);
            return Ok(tipopagamento);
        }

        [HttpPost("CriarTipoPagamento")]
        public async Task<ActionResult<ResponseModel<List<TipoPagamentoModel>>>> CriarTipoPagamento(TipoPagamentoCreateDto tipopagamentoCreateDto)
        {
            var tipopagamento = await _tipopagamento.CriarTipoPagamento(tipopagamentoCreateDto);
            return Ok(tipopagamento);
        }

        [HttpPut("EditarTipoPagamento")]
        public async Task<ActionResult<ResponseModel<List<TipoPagamentoModel>>>> EditarTipoPagamento(TipoPagamentoEdicaoDto tipopagamentoEdicaoDto)
        {
            var tipopagamento = await _tipopagamento.EditarTipoPagamento(tipopagamentoEdicaoDto);
            return Ok(tipopagamento);
        }

        [HttpDelete("DeleteTipoPagamento/{idTipoPagamento}")]
        public async Task<ActionResult<ResponseModel<List<TipoPagamentoModel>>>> DeleteTipoPagamento(int idTipoPagamento)
        {
            var tipopagamento = await _tipopagamento.DeleteTipoPagamento(idTipoPagamento);
            return Ok(tipopagamento);
        }
    }
}
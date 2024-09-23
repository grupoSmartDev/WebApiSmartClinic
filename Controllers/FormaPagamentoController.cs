
using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.FormaPagamento;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.FormaPagamento;

namespace WebApiSmartClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormaPagamentoController : ControllerBase
    {
        private readonly IFormaPagamentoInterface _formapagamento;
        public FormaPagamentoController(IFormaPagamentoInterface formapagamento)
        {
            _formapagamento = formapagamento;
        }

        [HttpGet("ListarFormaPagamento")]
        public async Task<ActionResult<ResponseModel<List<FormaPagamentoModel>>>> ListarFormaPagamento()
        {
            var formapagamento = await _formapagamento.ListarFormaPagamento();
            return Ok(formapagamento);
        }

        [HttpGet("BuscarFormaPagamentoPorId/{idFormaPagamento}")]
        public async Task<ActionResult<ResponseModel<List<FormaPagamentoModel>>>> BuscarFormaPagamentoPorId(int idFormaPagamento)
        {
            var formapagamento = await _formapagamento.BuscarFormaPagamentoPorId(idFormaPagamento);
            return Ok(formapagamento);
        }

        [HttpPost("CriarFormaPagamento")]
        public async Task<ActionResult<ResponseModel<List<FormaPagamentoModel>>>> CriarFormaPagamento(FormaPagamentoCreateDto formapagamentoCreateDto)
        {
            var formapagamento = await _formapagamento.CriarFormaPagamento(formapagamentoCreateDto);
            return Ok(formapagamento);
        }

        [HttpPut("EditarFormaPagamento")]
        public async Task<ActionResult<ResponseModel<List<FormaPagamentoModel>>>> EditarFormaPagamento(FormaPagamentoEdicaoDto formapagamentoEdicaoDto)
        {
            var formapagamento = await _formapagamento.EditarFormaPagamento(formapagamentoEdicaoDto);
            return Ok(formapagamento);
        }

        [HttpDelete("DeleteFormaPagamento/{idFormaPagamento}")]
        public async Task<ActionResult<ResponseModel<List<FormaPagamentoModel>>>> DeleteFormaPagamento(int idFormaPagamento)
        {
            var formapagamento = await _formapagamento.DeleteFormaPagamento(idFormaPagamento);
            return Ok(formapagamento);
        }
    }
}
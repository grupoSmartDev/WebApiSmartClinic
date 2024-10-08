
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

        [HttpGet("Listar")]
        public async Task<ActionResult<ResponseModel<List<FormaPagamentoModel>>>> ListarFormaPagamento()
        {
            var formapagamento = await _formapagamento.ListarFormaPagamento();
            return Ok(formapagamento);
        }

        [HttpGet("BuscarPorId/{idFormaPagamento}")]
        public async Task<ActionResult<ResponseModel<List<FormaPagamentoModel>>>> BuscarFormaPagamentoPorId(int idFormaPagamento)
        {
            var formapagamento = await _formapagamento.BuscarFormaPagamentoPorId(idFormaPagamento);
            return Ok(formapagamento);
        }

        [HttpPost("Criar")]
        public async Task<ActionResult<ResponseModel<List<FormaPagamentoModel>>>> CriarFormaPagamento(FormaPagamentoCreateDto formapagamentoCreateDto)
        {
            var formapagamento = await _formapagamento.CriarFormaPagamento(formapagamentoCreateDto);
            return Ok(formapagamento);
        }

        [HttpPut("Editar")]
        public async Task<ActionResult<ResponseModel<List<FormaPagamentoModel>>>> EditarFormaPagamento(FormaPagamentoEdicaoDto formapagamentoEdicaoDto)
        {
            var formapagamento = await _formapagamento.EditarFormaPagamento(formapagamentoEdicaoDto);
            return Ok(formapagamento);
        }

        [HttpDelete("Delete/{idFormaPagamento}")]
        public async Task<ActionResult<ResponseModel<List<FormaPagamentoModel>>>> DeleteFormaPagamento(int idFormaPagamento)
        {
            var formapagamento = await _formapagamento.DeleteFormaPagamento(idFormaPagamento);
            return Ok(formapagamento);
        }
    }
}
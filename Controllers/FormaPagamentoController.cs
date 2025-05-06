
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
        public async Task<ActionResult<ResponseModel<List<FormaPagamentoModel>>>> Listar(int pageNumber = 1, int pageSize = 10, int? idFiltro = null, int? parcelasFiltro = null, string? descricaoFiltro = null, bool paginar = true)
        {
            var formapagamento = await _formapagamento.Listar(pageNumber, pageSize, idFiltro, parcelasFiltro, descricaoFiltro, paginar);
            return Ok(formapagamento);
        }

        [HttpGet("BuscarPorId/{idFormaPagamento}")]
        public async Task<ActionResult<ResponseModel<List<FormaPagamentoModel>>>> BuscarPorId(int idFormaPagamento)
        {
            var formapagamento = await _formapagamento.BuscarPorId(idFormaPagamento);
            return Ok(formapagamento);
        }

        [HttpPost("Criar")]
        public async Task<ActionResult<ResponseModel<List<FormaPagamentoModel>>>> Criar(FormaPagamentoCreateDto formapagamentoCreateDto, int pageNumber = 1, int pageSize = 10, bool paginar = true)
        {
            var formapagamento = await _formapagamento.Criar(formapagamentoCreateDto, pageNumber, pageSize, paginar);
            return Ok(formapagamento);
        }

        [HttpPut("Editar")]
        public async Task<ActionResult<ResponseModel<List<FormaPagamentoModel>>>> Editar(FormaPagamentoEdicaoDto formapagamentoEdicaoDto, int pageNumber = 1, int pageSize = 10, bool paginar = true)
        {
            var formapagamento = await _formapagamento.Editar(formapagamentoEdicaoDto, pageNumber, pageSize, paginar);
            return Ok(formapagamento);
        }

        [HttpDelete("Delete/{idFormaPagamento}")]
        public async Task<ActionResult<ResponseModel<List<FormaPagamentoModel>>>> Delete(int idFormaPagamento, int pageNumber = 1, int pageSize = 10, bool paginar = true)
        {
            var formapagamento = await _formapagamento.Delete(idFormaPagamento, pageNumber, pageSize, paginar);
            return Ok(formapagamento);
        }
    }
}
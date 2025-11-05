using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.TipoPagamento;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.TipoPagamento;

namespace WebApiSmartClinic.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class TipoPagamentoController : ControllerBase
{
    private readonly ITipoPagamentoInterface _tipopagamento;
    public TipoPagamentoController(ITipoPagamentoInterface tipopagamento)
    {
        _tipopagamento = tipopagamento;
    }

    [HttpGet("Listar")]
    public async Task<ActionResult<ResponseModel<List<TipoPagamentoModel>>>> Listar(int pageNumber = 1, int pageSize = 10, int? idFiltro = null, string? descricaoFiltro = null, bool paginar = true)
    {
        var tipopagamento = await _tipopagamento.Listar(pageNumber, pageSize, idFiltro, descricaoFiltro, paginar);
        return Ok(tipopagamento);
    }

    [HttpGet("BuscarPorId/{idTipoPagamento}")]
    public async Task<ActionResult<ResponseModel<List<TipoPagamentoModel>>>> BuscarPorId(int idTipoPagamento, int pageNumber = 1, int pageSize = 10, bool paginar = true)
    {
        var tipopagamento = await _tipopagamento.BuscarPorId(idTipoPagamento);
        return Ok(tipopagamento);
    }

    [HttpPost("Criar")]
    public async Task<ActionResult<ResponseModel<List<TipoPagamentoModel>>>> Criar(TipoPagamentoCreateDto tipopagamentoCreateDto, int pageNumber = 1, int pageSize = 10, bool paginar = true)
    {
        var tipopagamento = await _tipopagamento.Criar(tipopagamentoCreateDto, pageNumber, pageSize, paginar);
        return Ok(tipopagamento);
    }

    [HttpPut("Editar")]
    public async Task<ActionResult<ResponseModel<List<TipoPagamentoModel>>>> Editar(TipoPagamentoEdicaoDto tipopagamentoEdicaoDto, int pageNumber = 1, int pageSize = 10, bool paginar = true)
    {
        var tipopagamento = await _tipopagamento.Editar(tipopagamentoEdicaoDto, pageNumber, pageSize, paginar);
        return Ok(tipopagamento);
    }

    [HttpDelete("Delete/{idTipoPagamento}")]
    public async Task<ActionResult<ResponseModel<List<TipoPagamentoModel>>>> Delete(int idTipoPagamento, int pageNumber = 1, int pageSize = 10, bool paginar = true)
    {
        var tipopagamento = await _tipopagamento.Delete(idTipoPagamento, pageNumber, pageSize, paginar);
        return Ok(tipopagamento);
    }
}
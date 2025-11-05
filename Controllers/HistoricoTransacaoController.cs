using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.HistoricoTransacao;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.HistoricoTransacao;

namespace WebApiSmartClinic.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class HistoricoTransacaoController : ControllerBase
{
    private readonly IHistoricoTransacaoInterface _historicotransacao;
    public HistoricoTransacaoController(IHistoricoTransacaoInterface historicotransacao)
    {
        _historicotransacao = historicotransacao;
    }

    [HttpGet("Listar")]
    public async Task<ActionResult<ResponseModel<List<HistoricoTransacaoModel>>>> Listar(int pageNumber = 1, int pageSize = 10, int? codigoFiltro = null, string? bancoFiltro = null, DateTime? dataTransacaoFiltro = null, string? tipoTransacaoFiltro = null,
        string? descricaoFiltro = null, string? referenciaFiltro = null, string? usuarioFiltro = null, bool paginar = true)
    {
        var historicotransacao = await _historicotransacao.Listar(pageNumber, pageSize, codigoFiltro, bancoFiltro, dataTransacaoFiltro, tipoTransacaoFiltro, descricaoFiltro, referenciaFiltro, usuarioFiltro, paginar);
        return Ok(historicotransacao);
    }

    [HttpGet("BuscarPorId/{idHistoricoTransacao}")]
    public async Task<ActionResult<ResponseModel<List<HistoricoTransacaoModel>>>> BuscarPorId(int idHistoricoTransacao)
    {
        var historicotransacao = await _historicotransacao.BuscarPorId(idHistoricoTransacao);
        return Ok(historicotransacao);
    }

    [HttpPost("Criar")]
    public async Task<ActionResult<ResponseModel<List<HistoricoTransacaoModel>>>> Criar(HistoricoTransacaoCreateDto historicotransacaoCreateDto, int pageNumber = 1, int pageSize = 10, bool paginar = true)
    {
        var historicotransacao = await _historicotransacao.Criar(historicotransacaoCreateDto, pageNumber, pageSize, paginar);
        return Ok(historicotransacao);
    }

    [HttpPut("Editar")]
    public async Task<ActionResult<ResponseModel<List<HistoricoTransacaoModel>>>> Editar(HistoricoTransacaoEdicaoDto historicotransacaoEdicaoDto, int pageNumber = 1, int pageSize = 10, bool paginar = true)
    {
        var historicotransacao = await _historicotransacao.Editar(historicotransacaoEdicaoDto, pageNumber, pageSize, paginar);
        return Ok(historicotransacao);
    }

    [HttpDelete("Delete/{idHistoricoTransacao}")]
    public async Task<ActionResult<ResponseModel<List<HistoricoTransacaoModel>>>> Delete(int idHistoricoTransacao, int pageNumber = 1, int pageSize = 10, bool paginar = true)
    {
        var historicotransacao = await _historicotransacao.Delete(idHistoricoTransacao, pageNumber, pageSize, paginar);
        return Ok(historicotransacao);
    }
}
using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.Banco;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.Banco;

namespace WebApiSmartClinic.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class BancoController : ControllerBase
{
    private readonly IBancoInterface _banco;
    public BancoController(IBancoInterface banco)
    {
        _banco = banco;
    }

    [HttpGet("Listar")]
    public async Task<ActionResult<ResponseModel<List<BancoModel>>>> Listar([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] int? codigoFiltro = null, [FromQuery] string? nomeBancoFiltro = null, [FromQuery] string? nomeTitularFiltro = null, [FromQuery] string? documentoTitularFiltro = null, [FromQuery] bool paginar = true)
    {
        var banco = await _banco.Listar(pageNumber, pageSize, codigoFiltro, nomeBancoFiltro, nomeTitularFiltro, documentoTitularFiltro, paginar);
        return Ok(banco);
    }

    [HttpGet("BuscarPorId/{idBanco}")]
    public async Task<ActionResult<ResponseModel<List<BancoModel>>>> BuscarPorId(int idBanco)
    {
        var banco = await _banco.BuscarPorId(idBanco);
        return Ok(banco);
    }

    [HttpPost("Criar")]
    public async Task<ActionResult<ResponseModel<List<BancoModel>>>> Criar(BancoCreateDto bancoCreateDto, int pageNumber = 1, int pageSize = 10)
    {
        var banco = await _banco.Criar(bancoCreateDto, pageNumber, pageSize);
        return Ok(banco);
    }

    [HttpPut("Editar")]
    public async Task<ActionResult<ResponseModel<List<BancoModel>>>> Editar(BancoEdicaoDto bancoEdicaoDto, int pageNumber = 1, int pageSize = 10)
    {
        var banco = await _banco.Editar(bancoEdicaoDto, pageNumber, pageSize);
        return Ok(banco);
    }

    [HttpDelete("Delete/{idBanco}")]
    public async Task<ActionResult<ResponseModel<List<BancoModel>>>> Delete(int idBanco, int pageNumber = 1, int pageSize = 10)
    {
        var banco = await _banco.Delete(idBanco, pageNumber, pageSize);
        return Ok(banco);
    }

    [HttpPost("DebitarSaldo")]
    public async Task<ActionResult> DebitarSaldo(int bancoId, decimal valor)
    {
        var resposta = await _banco.DebitarSaldo(bancoId, valor);
        if (!resposta.Status)
        {
            return BadRequest(resposta);
        }
        return Ok(resposta);
    }

    [HttpPost("CreditarSaldo")]
    public async Task<ActionResult> CreditarSaldo(int bancoId, decimal valor)
    {
        var resposta = await _banco.CreditarSaldo(bancoId, valor);
        if (!resposta.Status)
        {
            return BadRequest(resposta);
        }
        return Ok(resposta);
    }

    [HttpGet("ObterHistoricoTransacoes/{bancoId}")]
    public async Task<IActionResult> ObterHistoricoTransacoes(int bancoId)
    {
        var resposta = await _banco.ObterHistoricoTransacoes(bancoId);
        return Ok(resposta);
    }
}
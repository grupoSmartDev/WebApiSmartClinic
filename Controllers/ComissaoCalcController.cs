using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services;

namespace WebApiSmartClinic.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ComissaoCalcController : Controller
{
    private readonly IComissaoService _comissaoService;

    public ComissaoCalcController(IComissaoService comissaoService)
    {
        _comissaoService = comissaoService;
    }

    [HttpPost("Calcular")]
    public async Task<ActionResult<ResponseModel<List<ComissaoCalculadaModel>>>> CalcularComissoes([FromBody] CalcularComissaoDto dto)
    {
        var resultado = await _comissaoService.CalcularComissoes(dto.DataInicio, dto.DataFim, dto.ProfissionalId);

        if (resultado.Status)
            return Ok(resultado);
        else
            return BadRequest(resultado);
    }

    [HttpGet("Listar")]
    public async Task<ActionResult<ResponseModel<List<ComissaoCalculadaModel>>>> ListarComissoes(
        [FromQuery] DateTime dataInicio,
        [FromQuery] DateTime dataFim,
        [FromQuery] StatusComissao? status = null,
        [FromQuery] int? profissionalId = null)
    {
        var resultado = await _comissaoService.ListarComissoes(dataInicio, dataFim, status, profissionalId);

        if (resultado.Status)
            return Ok(resultado);
        else
            return BadRequest(resultado);
    }

    [HttpPost("DarBaixa")]
    public async Task<ActionResult<ResponseModel<string>>> DarBaixaComissoes([FromBody] List<int> idsComissoes)
    {
        var resultado = await _comissaoService.DarBaixaComissoes(idsComissoes);

        if (resultado.Status)
            return Ok(resultado);
        else
            return BadRequest(resultado);
    }

    [HttpGet("Resumo")]
    public async Task<ActionResult<ResponseModel<ComissaoResumoDto>>> ObterResumo(
        [FromQuery] DateTime dataInicio,
        [FromQuery] DateTime dataFim,
        [FromQuery] int? profissionalId = null)
    {
        var resultado = await _comissaoService.ObterResumoComissoes(dataInicio, dataFim, profissionalId);

        if (resultado.Status)
            return Ok(resultado);
        else
            return BadRequest(resultado);
    }
}

// DTO para calcular comissões
public class CalcularComissaoDto
{
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public int? ProfissionalId { get; set; }
}
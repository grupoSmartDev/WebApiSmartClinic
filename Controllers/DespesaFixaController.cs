using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.DespesaFixa;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.DespesaFixa;

namespace WebApiSmartClinic.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class DespesaFixaController : ControllerBase
{
    private readonly IDespesaFixaInterface _despesaFixa;

    public DespesaFixaController(IDespesaFixaInterface despesaFixa)
    {
        _despesaFixa = despesaFixa;
    }

    [HttpGet("Listar")]
    public async Task<ActionResult<ResponseModel<List<DespesaFixaModel>>>> Listar([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] int? idFiltro = null, [FromQuery] string? descricaoFiltro = null,
        [FromQuery] string? vencimentoFiltro = null, [FromQuery] string? centroCustoFiltro = null, [FromQuery] string? planoContasFiltro = null, [FromQuery] bool paginar = true)
    {
        var despesa = await _despesaFixa.Listar(pageNumber, pageSize, idFiltro, descricaoFiltro, vencimentoFiltro, centroCustoFiltro, planoContasFiltro, paginar);
        return Ok(despesa);
    }

    [HttpGet("BuscarPorId/{idDespesa}")]
    public async Task<ActionResult<ResponseModel<List<DespesaFixaModel>>>> BuscarPorId(int idDespesa)
    {
        var despesa = await _despesaFixa.BuscarPorId(idDespesa);
        return Ok(despesa);
    }

    [HttpPost("Criar")]
    public async Task<ActionResult<ResponseModel<List<DespesaFixaModel>>>> Criar(DespesaFixaCreateDto despesaCreateDto)
    {
        var despesa = await _despesaFixa.Criar(despesaCreateDto);
        return Ok(despesa);
    }

    [HttpPut("Editar")]
    public async Task<ActionResult<ResponseModel<List<DespesaFixaModel>>>> Editar(DespesaFixaEdicaoDto despesaEdicaoDto)
    {
        var despesa = await _despesaFixa.Editar(despesaEdicaoDto);
        return Ok(despesa);
    }

    [HttpDelete("Delete/{idDespesa}")]
    public async Task<ActionResult<ResponseModel<List<DespesaFixaModel>>>> Delete(int idDespesa)
    {
        var despesa = await _despesaFixa.Delete(idDespesa);
        return Ok(despesa);
    }
}

using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.Financ_Receber;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.Financ_Receber;

namespace WebApiSmartClinic.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class Financ_ReceberController : ControllerBase
{
    private readonly IFinanc_ReceberInterface _financ_receber;
    public Financ_ReceberController(IFinanc_ReceberInterface financ_receber)
    {
        _financ_receber = financ_receber;
    }

    [HttpGet("Listar")]
    public async Task<ActionResult<ResponseModel<List<Financ_ReceberModel>>>> Listar([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] int? codigoFiltro = null, [FromQuery] string? descricaoFiltro = null, [FromQuery] DateTime? dataEmissaoInicio = null, [FromQuery] DateTime? dataEmissaoFim = null,
         [FromQuery] decimal? valorMinimoFiltro = null, [FromQuery] decimal? valorMaximoFiltro = null, [FromQuery] int? parcelaNumeroFiltro = null, [FromQuery] DateTime? vencimentoInicio = null, [FromQuery] DateTime? vencimentoFim = null, [FromQuery] bool paginar = true)
    {
        var financ_receber = await _financ_receber.Listar(pageNumber, pageSize, codigoFiltro, descricaoFiltro, dataEmissaoInicio, dataEmissaoFim, valorMinimoFiltro, valorMaximoFiltro, parcelaNumeroFiltro, vencimentoInicio, vencimentoFim, paginar);
        return Ok(financ_receber);
    }

    [HttpGet("ListarAnalitico")]
    public async Task<ActionResult<ResponseModel<List<Financ_ReceberModel>>>> ListarAnalitico([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] int? idFiltro = null, [FromQuery] string? descricaoFiltro = null, [FromQuery] int? pacienteIdFiltro = null, [FromQuery] string? dataBaseFiltro = "E",
         [FromQuery] string? ccFiltro = null, [FromQuery] DateTime? dataFiltroInicio = null, [FromQuery] DateTime? dataFiltroFim = null,[FromQuery] bool paginar = true)
    {
        var financ_receber = await _financ_receber.ListarAnalitico(pageNumber, pageSize, idFiltro, descricaoFiltro, pacienteIdFiltro, dataBaseFiltro, ccFiltro, dataFiltroInicio, dataFiltroFim, paginar);
        return Ok(financ_receber);
    }

    [HttpGet("ListarSintetico")]
    public async Task<ActionResult<ResponseModel<List<Financ_ReceberModel>>>> ListarSintetico([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] int? idPaiFiltro = null, [FromQuery] int? parcelaNumeroFiltro = null, [FromQuery] string? dataBaseFiltro = "V", [FromQuery] DateTime? dataFiltroInicio = null, [FromQuery] DateTime? dataFiltroFim = null, [FromQuery] bool parcelasVencidasFiltro = false, [FromQuery] bool paginar = true)
    {
        var financ_receber = await _financ_receber.ListarSintetico(pageNumber, pageSize, idPaiFiltro, parcelaNumeroFiltro, dataBaseFiltro, dataFiltroInicio, dataFiltroFim, parcelasVencidasFiltro, paginar);
        return Ok(financ_receber);
    }

    [HttpGet("BuscarPorId/{idFinanc_Receber}")]
    public async Task<ActionResult<ResponseModel<List<Financ_ReceberModel>>>> BuscarPorId(int idFinanc_Receber)
    {
        var financ_receber = await _financ_receber.BuscarPorId(idFinanc_Receber);
        return Ok(financ_receber);
    }

    [HttpPost("Criar")]
    public async Task<ActionResult<ResponseModel<List<Financ_ReceberModel>>>> Criar(Financ_ReceberCreateDto financ_receberCreateDto, int pageNumber = 1, int pageSize = 10)
    {
        var financ_receber = await _financ_receber.Criar(financ_receberCreateDto, pageNumber, pageSize);
        return Ok(financ_receber);
    }

    [HttpPut("Editar")]
    public async Task<ActionResult<ResponseModel<List<Financ_ReceberModel>>>> Editar(Financ_ReceberEdicaoDto financ_receberEdicaoDto, int pageNumber = 1, int pageSize = 10)
    {
        var financ_receber = await _financ_receber.Editar(financ_receberEdicaoDto, pageNumber, pageSize);
        return Ok(financ_receber);
    }

    [HttpDelete("Delete/{idFinanc_Receber}")]
    public async Task<ActionResult<ResponseModel<List<Financ_ReceberModel>>>> Delete(int idFinanc_Receber, int pageNumber = 1, int pageSize = 10)
    {
        var financ_receber = await _financ_receber.Delete(idFinanc_Receber);
        return Ok(financ_receber);
    }

    [HttpPut("BaixarParcela")]
    public async Task<ActionResult<ResponseModel<Financ_ReceberSubModel>>> BaixarParcela(Financ_ReceberSubEdicaoDto financ_receberSubEdicaoDto)
    {
        var resultado = await _financ_receber.BaixarParcela(financ_receberSubEdicaoDto);
        return Ok(resultado);
    }

    [HttpPost("EstornarParcela/{idParcela}")]
    public async Task<ActionResult<ResponseModel<string>>> EstornarParcela(int idParcela)
    {
        var resultado = await _financ_receber.EstornarParcela(idParcela);
        return Ok(resultado);
    }

    [HttpGet("CalcularTotalRecebiveis")]
    public async Task<ActionResult<ResponseModel<decimal>>> CalcularTotalRecebiveis([FromQuery] int cliente = 0, [FromQuery] DateTime? dataInicio = null, [FromQuery] DateTime? dataFim = null)
    {
        var resultado = await _financ_receber.CalcularTotalRecebiveis(cliente, dataInicio, dataFim);
        return Ok(resultado);
    }

    [HttpGet("BuscarContasEmAberto")]
    public async Task<ActionResult<ResponseModel<List<Financ_ReceberModel>>>> BuscarContasEmAberto()
    {
        var resultado = await _financ_receber.BuscarContasEmAberto();
        return Ok(resultado);
    }
}
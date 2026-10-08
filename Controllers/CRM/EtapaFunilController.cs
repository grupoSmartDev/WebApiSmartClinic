using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.CRM;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.CRM;

namespace WebApiSmartClinic.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public sealed class EtapaFunilController : ControllerBase
{
    private readonly IEtapaFunilInterface _etapaFunil;
    public EtapaFunilController(IEtapaFunilInterface etapaFunil)
    {
        _etapaFunil = etapaFunil;
    }

    [HttpGet("Listar")]
    public async Task<ActionResult<ResponseModel<List<EtapaFunilModel>>>> Listar()
    {
        var resposta = await _etapaFunil.Listar();
        return Ok(resposta);
    }

    [HttpGet("BuscarPorId/{id}")]
    public async Task<ActionResult<ResponseModel<EtapaFunilModel>>> BuscarPorId(int id)
    {
        var resposta = await _etapaFunil.BuscarPorId(id);
        return Ok(resposta);
    }

    [HttpPost("Criar")]
    public async Task<ActionResult<ResponseModel<List<EtapaFunilModel>>>> Criar(EtapaFunilCreateDto dto)
    {
        var resposta = await _etapaFunil.Criar(dto);
        return Ok(resposta);
    }

    [HttpPut("Editar")]
    public async Task<ActionResult<ResponseModel<List<EtapaFunilModel>>>> Editar(EtapaFunilEdicaoDto dto)
    {
        var resposta = await _etapaFunil.Editar(dto);
        return Ok(resposta);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult<ResponseModel<List<EtapaFunilModel>>>> Delete(int id)
    {
        var resposta = await _etapaFunil.Delete(id);
        return Ok(resposta);
    }

    [HttpPut("Reordenar")]
    public async Task<ActionResult<ResponseModel<List<EtapaFunilModel>>>> Reordenar(List<EtapaFunilOrdemDto> ordens)
    {
        var resposta = await _etapaFunil.ReordenarEtapas(ordens);
        return Ok(resposta);
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.CRM;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.CRM;

namespace WebApiSmartClinic.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public sealed class CampanhaController : ControllerBase
{
    private readonly ICampanhaInterface _campanha;
    public CampanhaController(ICampanhaInterface campanha)
    {
        _campanha = campanha;
    }

    [HttpGet("Listar")]
    public async Task<ActionResult<ResponseModel<List<CampanhaModel>>>> Listar()
    {
        var resposta = await _campanha.Listar();
        return Ok(resposta);
    }

    [HttpGet("BuscarPorId/{id}")]
    public async Task<ActionResult<ResponseModel<CampanhaModel>>> BuscarPorId(int id)
    {
        var resposta = await _campanha.BuscarPorId(id);
        return Ok(resposta);
    }

    [HttpPost("Criar")]
    public async Task<ActionResult<ResponseModel<List<CampanhaModel>>>> Criar(CampanhaCreateDto dto)
    {
        var resposta = await _campanha.Criar(dto);
        return Ok(resposta);
    }

    [HttpPut("Editar")]
    public async Task<ActionResult<ResponseModel<List<CampanhaModel>>>> Editar(CampanhaEdicaoDto dto)
    {
        var resposta = await _campanha.Editar(dto);
        return Ok(resposta);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult<ResponseModel<List<CampanhaModel>>>> Delete(int id)
    {
        var resposta = await _campanha.Delete(id);
        return Ok(resposta);
    }
}

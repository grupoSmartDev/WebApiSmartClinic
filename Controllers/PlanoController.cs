using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.Plano;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.Plano;

namespace WebApiSmartClinic.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class PlanoController : ControllerBase
{
    private readonly IPlanoInterface _plano;
    public PlanoController(IPlanoInterface plano)
    {
        _plano = plano;
    }

    [HttpGet("Listar")]
    public async Task<ActionResult<ResponseModel<List<PlanoModel>>>> Listar([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] int? codigoFiltro = null, [FromQuery] string? descricaoFiltro = null, [FromQuery] bool paginar = true)
    {
        var plano = await _plano.Listar(pageNumber, pageSize, codigoFiltro, descricaoFiltro, paginar);
        return Ok(plano);
    }

    [HttpGet("BuscarPorId/{idPlano}")]
    public async Task<ActionResult<ResponseModel<List<PlanoModel>>>> BuscarPorId(int idPlano)
    {
        var plano = await _plano.BuscarPorId(idPlano);
        return Ok(plano);
    }

    [HttpPost("Criar")]
    public async Task<ActionResult<ResponseModel<List<PlanoModel>>>> Criar(PlanoCreateDto planoCreateDto, int pageNumber = 1, int pageSize = 10)
    {
        var plano = await _plano.Criar(planoCreateDto, pageNumber, pageSize);
        return Ok(plano);
    }

    [HttpPost("PlanoParaPaciente")]
    public async Task<ActionResult<ResponseModel<List<PlanoModel>>>> PlanoParaPaciente(PlanoCreateDto planoCreateDto)
    {
        var plano = await _plano.PlanoParaPaciente(planoCreateDto);
        return Ok(plano);
    }

    [HttpPut("Editar")]
    public async Task<ActionResult<ResponseModel<List<PlanoModel>>>> Editar(PlanoEdicaoDto planoEdicaoDto, int pageNumber = 1, int pageSize = 10)
    {
        var plano = await _plano.Editar(planoEdicaoDto, pageNumber, pageSize);
        return Ok(plano);
    }

    [HttpPut("InativarPlanoPaciente")]
    public async Task<ActionResult<ResponseModel<List<PlanoModel>>>> InativarPlanoPaciente(PlanoEdicaoDto planoEdicaoDto, int pageNumber = 1, int pageSize = 10)
    {
        var plano = await _plano.InativarPlanoPaciente(planoEdicaoDto, pageNumber, pageSize);
        return Ok(plano);
    }

    [HttpDelete("Delete/{idPlano}")]
    public async Task<ActionResult<ResponseModel<List<PlanoModel>>>> Delete(int idPlano, int pageNumber = 1, int pageSize = 10)
    {
        var plano = await _plano.Delete(idPlano, pageNumber, pageSize);
        return Ok(plano);
    }

    [HttpPost("RenovarPlano")]
    public async Task<ActionResult<ResponseModel<List<PlanoModel>>>> RenovarPlano([FromBody] PlanoRenovacaoDto renovacaoDto)
    {
        var resultado = await _plano.RenovarPlano(renovacaoDto);
        return Ok(resultado);
    }
}
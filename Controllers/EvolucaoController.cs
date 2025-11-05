using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.Evolucao;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.Evolucao;

namespace WebApiSmartClinic.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class EvolucaoController : ControllerBase
{
    private readonly IEvolucaoInterface _evolucao;
    public EvolucaoController(IEvolucaoInterface evolucao)
    {
        _evolucao = evolucao;
    }

    [HttpGet("Listar")]
    public async Task<ActionResult<ResponseModel<List<EvolucaoModel>>>> Listar()
    {
        var evolucao = await _evolucao.Listar();
        return Ok(evolucao);
    }

    [HttpGet("BuscarPorId/{idEvolucao}")]
    public async Task<ActionResult<ResponseModel<List<EvolucaoModel>>>> BuscarPorId(int idEvolucao)
    {
        var evolucao = await _evolucao.BuscarPorId(idEvolucao);
        return Ok(evolucao);
    }

    [HttpPost("Criar")]
    public async Task<ActionResult<ResponseModel<List<EvolucaoModel>>>> Criar(EvolucaoCreateDto evolucaoCreateDto)
    {
        var evolucao = await _evolucao.Criar(evolucaoCreateDto);
        return Ok(evolucao);
    }
    [HttpPost("CriarEvolucaoPaciente")]
    public async Task<ActionResult<ResponseModel<EvolucaoModel>>> CriarEvolucaoPaciente(EvolucaoCreateDto evolucaoCreateDto)
    {
        var evolucao = await _evolucao.CriarEvolucaoPaciente(evolucaoCreateDto);
        return Ok(evolucao);
    }

    [HttpPut("Editar")]
    public async Task<ActionResult<ResponseModel<List<EvolucaoModel>>>> Editar(EvolucaoEdicaoDto evolucaoEdicaoDto)
    {
        var evolucao = await _evolucao.Editar(evolucaoEdicaoDto);
        return Ok(evolucao);
    }

    [HttpDelete("Delete/{idEvolucao}")]
    public async Task<ActionResult<ResponseModel<List<EvolucaoModel>>>> Delete(int idEvolucao)
    {
        var evolucao = await _evolucao.Delete(idEvolucao);
        return Ok(evolucao);
    }
}
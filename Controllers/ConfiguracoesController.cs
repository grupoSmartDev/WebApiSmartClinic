using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.Configuracoes;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.Configuracoes;

namespace WebApiSmartClinic.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class ConfiguracoesController : ControllerBase
{
    private readonly IConfiguracoesInterface _configuracoes;
    public ConfiguracoesController(IConfiguracoesInterface configuracoes)
    {
        _configuracoes = configuracoes;
    }

    [HttpGet("BuscarPorId")]
    public async Task<ActionResult<ResponseModel<List<EmpresaModel>>>> BuscarPorId([FromQuery] int idEmpresa)
    {
        var config = await _configuracoes.BuscarPorId(idEmpresa);
        return Ok(config);
    }

    [HttpPut("Editar")]
    public async Task<ActionResult<ResponseModel<List<EmpresaModel>>>> Editar(ConfiguracoesEdicaoDto configuracoesEdicaoDto)
    {
        await _configuracoes.Editar(configuracoesEdicaoDto);
        return Ok(true);
    }
}
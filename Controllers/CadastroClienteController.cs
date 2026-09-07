using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.CadastroCliente;
using WebApiSmartClinic.Helpers;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.CadastroCliente;

namespace WebApiSmartClinic.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class CadastroClienteController : ControllerBase
{
    private readonly ICadastroClienteInterface _cadastrocliente;
    public CadastroClienteController(ICadastroClienteInterface cadastrocliente)
    {
        _cadastrocliente = cadastrocliente;
    }

    [AllowAnonymous]
    [HttpPost("Criar")]
    public async Task<ActionResult<ResponseModel<List<EmpresaModel>>>> Criar([FromBody] CadastroClienteCreateDto cadastroclienteCreateDto)
    {
        var cadastrocliente = await _cadastrocliente.Criar(cadastroclienteCreateDto);
        return Ok(cadastrocliente);
    }
    [HttpGet("Listar")]
    public async Task<ActionResult<ResponseModel<List<EmpresaModel>>>> Listar()
    {
        var cadastrocliente = await _cadastrocliente.Listar();
        return Ok(cadastrocliente);
    }

    [HttpDelete("Delete/{idCadastroCliente}")]
    public async Task<ActionResult<ResponseModel<List<EmpresaModel>>>> Delete(int idCadastroCliente)
    {
        var cadastrocliente = await _cadastrocliente.Delete(idCadastroCliente);
        return Ok(cadastrocliente);
    }

    [Authorize(Roles = Perfis.Admin)]
    [HttpPost("ReprocessarAsaas/{empresaId}")]
    public async Task<ActionResult<ResponseModel<EmpresaModel>>> ReprocessarAsaas(int empresaId)
    {
        var resultado = await _cadastrocliente.ReprocessarAsaas(empresaId);
        return Ok(resultado);
    }

    // Restrito a Admin (não [Authorize] genérico como no rascunho original) — esse endpoint
    // altera plano/cobrança da empresa, um usuário "User"/"Support" do mesmo tenant não deveria
    // conseguir se auto-promover de plano via chamada direta à API.
    [Authorize(Roles = Perfis.Admin)]
    [HttpPost("UpgradePlano")]
    public async Task<ActionResult<ResponseModel<EmpresaModel>>> UpgradePlano([FromBody] UpgradePlanoDto dto)
    {
        var resultado = await _cadastrocliente.UpgradePlano(dto);
        return Ok(resultado);
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.CRM;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.CRM;

namespace WebApiSmartClinic.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public sealed class FollowUpController : ControllerBase
{
    private readonly IFollowUpInterface _followUp;
    public FollowUpController(IFollowUpInterface followUp)
    {
        _followUp = followUp;
    }

    [HttpGet("Listar/{leadId}")]
    public async Task<ActionResult<ResponseModel<List<FollowUpModel>>>> Listar(int leadId)
    {
        var resposta = await _followUp.Listar(leadId);
        return Ok(resposta);
    }

    [HttpPost("Criar")]
    public async Task<ActionResult<ResponseModel<List<FollowUpModel>>>> Criar(FollowUpCreateDto dto)
    {
        var resposta = await _followUp.Criar(dto);
        return Ok(resposta);
    }

    [HttpPut("Editar")]
    public async Task<ActionResult<ResponseModel<List<FollowUpModel>>>> Editar(FollowUpEdicaoDto dto)
    {
        var resposta = await _followUp.Editar(dto);
        return Ok(resposta);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult<ResponseModel<List<FollowUpModel>>>> Delete(int id)
    {
        var resposta = await _followUp.Delete(id);
        return Ok(resposta);
    }
}

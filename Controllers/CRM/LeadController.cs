using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.CRM;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.CRM;

namespace WebApiSmartClinic.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public sealed class LeadController : ControllerBase
{
    private readonly ILeadInterface _lead;
    public LeadController(ILeadInterface lead)
    {
        _lead = lead;
    }

    [HttpGet("Listar")]
    public async Task<ActionResult<ResponseModel<List<LeadModel>>>> Listar([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] int? etapaId = null, [FromQuery] int? profissionalId = null, [FromQuery] string? canalOrigem = null, [FromQuery] string? search = null, [FromQuery] bool paginar = true)
    {
        var resposta = await _lead.Listar(pageNumber, pageSize, etapaId, profissionalId, canalOrigem, search, paginar);
        return Ok(resposta);
    }

    [HttpGet("ListarKanban")]
    public async Task<ActionResult<ResponseModel<List<KanbanColunaDto>>>> ListarKanban([FromQuery] int? profissionalId = null, [FromQuery] string? canalOrigem = null, [FromQuery] string? search = null)
    {
        var resposta = await _lead.ListarKanban(profissionalId, canalOrigem, search);
        return Ok(resposta);
    }

    [HttpGet("BuscarPorId/{id}")]
    public async Task<ActionResult<ResponseModel<LeadModel>>> BuscarPorId(int id)
    {
        var resposta = await _lead.BuscarPorId(id);
        return Ok(resposta);
    }

    [HttpPost("Criar")]
    public async Task<ActionResult<ResponseModel<List<LeadModel>>>> Criar(LeadCreateDto dto, int pageNumber = 1, int pageSize = 10)
    {
        var resposta = await _lead.Criar(dto, pageNumber, pageSize);
        return Ok(resposta);
    }

    [HttpPut("Editar")]
    public async Task<ActionResult<ResponseModel<List<LeadModel>>>> Editar(LeadEdicaoDto dto, int pageNumber = 1, int pageSize = 10)
    {
        var resposta = await _lead.Editar(dto, pageNumber, pageSize);
        return Ok(resposta);
    }

    [HttpPut("MoverEtapa")]
    public async Task<ActionResult<ResponseModel<LeadModel>>> MoverEtapa(LeadMoverEtapaDto dto)
    {
        var resposta = await _lead.MoverEtapa(dto);
        return Ok(resposta);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult<ResponseModel<List<LeadModel>>>> Delete(int id, int pageNumber = 1, int pageSize = 10)
    {
        var resposta = await _lead.Delete(id, pageNumber, pageSize);
        return Ok(resposta);
    }

    [HttpPost("ConverterEmPaciente/{leadId}")]
    public async Task<ActionResult<ResponseModel<LeadModel>>> ConverterEmPaciente(int leadId)
    {
        var resposta = await _lead.ConverterEmPaciente(leadId);
        return Ok(resposta);
    }
}

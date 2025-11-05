using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.Status;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.Status;

namespace WebApiSmartClinic.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class StatusController : ControllerBase
{
    private readonly IStatusInterface _status;
    public StatusController(IStatusInterface status)
    {
        _status = status;
    }

    [HttpGet("Listar")]
    public async Task<ActionResult<ResponseModel<List<StatusModel>>>> ListarStatus([FromQuery] string? statusFiltro = null, [FromQuery] string? cor = null, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var statusS = await _status.Listar(statusFiltro, cor, page, pageSize);
        return Ok(statusS);
    }

    [HttpGet("BuscarPorId/{idStatus}")]
    public async Task<ActionResult<ResponseModel<List<StatusModel>>>> BuscarStatusPorId(int idStatus)
    {
        var status = await _status.BuscarStatusPorId(idStatus);
        return Ok(status);
    }

    [HttpPost("Criar")]
    public async Task<ActionResult<ResponseModel<List<StatusModel>>>> CriarStatus(StatusCreateDto statusCreateDto)
    {
        var status = await _status.CriarStatus(statusCreateDto);
        return Ok(status);
    }

    [HttpPut("Editar")]
    public async Task<ActionResult<ResponseModel<List<StatusModel>>>> EditarStatus(StatusEdicaoDto statusEdicaoDto)
    {
        var status = await _status.EditarStatus(statusEdicaoDto);
        return Ok(status);
    }

    [HttpDelete("Delete/{idStatus}")]
    public async Task<ActionResult<ResponseModel<List<StatusModel>>>> DeleteStatus(int idStatus)
    {
        var status = await _status.DeleteStatus(idStatus);
        return Ok(status);
    }

}

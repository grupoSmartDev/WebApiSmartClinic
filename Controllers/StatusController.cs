using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.Status;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.Status;

namespace WebApiSmartClinic.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StatusController : ControllerBase
{
    private readonly IStatusInterface _status;
    public StatusController(IStatusInterface status)
    {
        _status = status;
    }

    [HttpGet("ListarStatus")]
    public async Task<ActionResult<ResponseModel<List<StatusModel>>>> ListarStatus()
    {
        var status = await _status.ListarStatus();
        return Ok(status);
    }

    [HttpGet("BuscarStatusPorId/{idStatus}")]
    public async Task<ActionResult<ResponseModel<List<StatusModel>>>> BuscarStatusPorId(int idStatus)
    {
        var status = await _status.BuscarStatusPorId(idStatus);
        return Ok(status);
    }

    [HttpPost("CriarStatus")]
    public async Task<ActionResult<ResponseModel<List<StatusModel>>>> CriarStatus(StatusCreateDto statusCreateDto)
    {
        var status = await _status.CriarStatus(statusCreateDto);
        return Ok(status);
    }

    [HttpPut("EditarStatus")]
    public async Task<ActionResult<ResponseModel<List<StatusModel>>>> EditarStatus(StatusEdicaoDto statusEdicaoDto)
    {
        var status = await _status.EditarStatus(statusEdicaoDto);
        return Ok(status);
    }

    [HttpDelete("DeleteStatus/{idStatus}")]
    public async Task<ActionResult<ResponseModel<List<StatusModel>>>> DeleteStatus(int idStatus)
    {
        var status = await _status.DeleteStatus(idStatus);
        return Ok(status);
    }

}

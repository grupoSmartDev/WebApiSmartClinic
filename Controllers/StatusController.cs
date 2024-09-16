using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
}

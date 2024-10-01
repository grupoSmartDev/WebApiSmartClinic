using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.Sala;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.Sala;

namespace WebApiSmartClinic.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SalaController : ControllerBase
{
    private readonly ISalaInterface _context;
    public SalaController(ISalaInterface sala)
    {
        _context = sala;
    }

    [HttpGet("Listar")]
    public async Task<ActionResult<ResponseModel<List<SalaModel>>>> ListarStatus()
    {
        var sala = await _context.ListarSala();
        return Ok(sala);
    }

    [HttpGet("BuscarPorId/{idStatus}")]
    public async Task<ActionResult<ResponseModel<List<SalaModel>>>> BuscarStatusPorId(int idStatus)
    {
        var sala = await _context.BuscarSalaPorId(idStatus);
        return Ok(sala);
    }

    [HttpPost("Criar")]
    public async Task<ActionResult<ResponseModel<List<SalaModel>>>> CriarStatus(SalaCreateDto statusCreateDto)
    {
        var sala = await _context.CriarSala(statusCreateDto);
        return Ok(sala);
    }

    [HttpPut("Editar")]
    public async Task<ActionResult<ResponseModel<List<SalaModel>>>> EditarSala(SalaEdicaoDto salaEdicaoDto)
    {
        var sala = await _context.EditarSala(salaEdicaoDto);
        return Ok(sala);
    }

    [HttpDelete("Delete/{idStatus}")]
    public async Task<ActionResult<ResponseModel<List<SalaModel>>>> DeleteSala(int idSala)
    {
        var sala = await _context.DeleteSala(idSala);
        return Ok(sala);
    }

}

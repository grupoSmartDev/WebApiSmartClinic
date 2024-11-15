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

    [HttpGet("BuscarPorId/{id}")]
    public async Task<ActionResult<ResponseModel<List<SalaModel>>>> BuscarStatusPorId(int id)
    {
        var sala = await _context.BuscarSalaPorId(id);
        return Ok(sala);
    }

    [HttpPost("Criar")]
    public async Task<ActionResult<ResponseModel<List<SalaModel>>>> CriarStatus(SalaCreateDto salaCreateDto)
    {
        var sala = await _context.CriarSala(salaCreateDto);
        return Ok(sala);
    }

    [HttpPut("Editar")]
    public async Task<ActionResult<ResponseModel<List<SalaModel>>>> EditarSala(SalaEdicaoDto salaEdicaoDto)
    {
        var sala = await _context.EditarSala(salaEdicaoDto);
        return Ok(sala);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult<ResponseModel<List<SalaModel>>>> DeleteSala(int id)
    {
        var sala = await _context.DeleteSala(id);
        return Ok(sala);
    }

}

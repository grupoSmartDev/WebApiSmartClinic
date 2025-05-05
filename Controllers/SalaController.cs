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
    public async Task<ActionResult<ResponseModel<List<SalaModel>>>> Listar([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] int? idFiltro = null, [FromQuery] string? nomeFiltro = null, [FromQuery] string? localFiltro = null, [FromQuery] int? capacidadeFiltro = null, [FromQuery] bool paginar = true)
    {
        var sala = await _context.Listar(pageNumber, pageSize, idFiltro, nomeFiltro, localFiltro, capacidadeFiltro, paginar);
        return Ok(sala);
    }

    [HttpGet("BuscarPorId/{id}")]
    public async Task<ActionResult<ResponseModel<List<SalaModel>>>> BuscarPorId(int id)
    {
        var sala = await _context.BuscarPorId(id);
        return Ok(sala);
    }

    [HttpPost("Criar")]
    public async Task<ActionResult<ResponseModel<List<SalaModel>>>> Criar(SalaCreateDto salaCreateDto, int pageNumber = 1, int pageSize = 10)
    {
        var sala = await _context.Criar(salaCreateDto, pageNumber, pageSize);
        return Ok(sala);
    }

    [HttpPut("Editar")]
    public async Task<ActionResult<ResponseModel<List<SalaModel>>>> Editar(SalaEdicaoDto salaEdicaoDto, int pageNumber = 1, int pageSize = 10)
    {
        var sala = await _context.Editar(salaEdicaoDto, pageNumber, pageSize);
        return Ok(sala);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult<ResponseModel<List<SalaModel>>>> Delete(int id, int pageNumber = 1, int pageSize = 10)
    {
        var sala = await _context.Delete(id, pageNumber, pageSize);
        return Ok(sala);
    }

}

using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.LogUsuario;

namespace WebApiSmartClinic.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class LogUsuarioController : ControllerBase
{
    private readonly ILogUsuarioInterface _logusuario;
    public LogUsuarioController(ILogUsuarioInterface logusuario)
    {
        _logusuario = logusuario;
    }

    [HttpGet("Listar")]
    public async Task<ActionResult<ResponseModel<List<LogUsuarioModel>>>> Listar(int pageNumber = 1, int pageSize = 10, int? codigoFiltro = null, string? descricaoFiltro = null, string? rotinaFiltro = null, int? idMovimentacaoFiltro = null,
        string? usuarioFiltro = null, DateTime? dataMovimentacaoFiltro = null, bool paginar = true)
    {
        var logusuario = await _logusuario.Listar(pageNumber, pageSize, codigoFiltro, descricaoFiltro, rotinaFiltro, idMovimentacaoFiltro, usuarioFiltro, dataMovimentacaoFiltro, paginar);
        return Ok(logusuario);
    }

    [HttpGet("BuscarPorId/{idLogUsuario}")]
    public async Task<ActionResult<ResponseModel<List<LogUsuarioModel>>>> BuscarPorId(int idLogUsuario)
    {
        var logusuario = await _logusuario.BuscarPorId(idLogUsuario);
        return Ok(logusuario);
    }

    [HttpPost("Inserir")]
    public async Task<ActionResult<ResponseModel<List<LogUsuarioModel>>>> Inserir(int id, string descricao, string rotina, string usuarioId)
    {
        var logusuario = await _logusuario.Inserir(id, descricao, rotina, usuarioId);
        return Ok(logusuario);
    }
}
using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.Usuario;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.Usuario;

namespace WebApiSmartClinic.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class UsuarioController : ControllerBase
{
    private readonly IUsuarioInterface _usuario;
    public UsuarioController(IUsuarioInterface usuario)
    {
        _usuario = usuario;
    }

    [HttpGet("Listar")]
    public async Task<ActionResult<ResponseModel<List<UsuarioModel>>>> Listar()
    {
        var usuario = await _usuario.Listar();
        return Ok(usuario);
    }

    [HttpGet("BuscarPorId/{idUsuario}")]
    public async Task<ActionResult<ResponseModel<List<UsuarioModel>>>> BuscarPorId(int idUsuario)
    {
        var usuario = await _usuario.BuscarPorId(idUsuario);
        return Ok(usuario);
    }

    [HttpPost("Criar")]
    public async Task<ActionResult<ResponseModel<List<UsuarioModel>>>> Criar(UsuarioCreateDto usuarioCreateDto)
    {
        var usuario = await _usuario.Criar(usuarioCreateDto);
        return Ok(usuario);
    }

    [HttpPut("Editar")]
    public async Task<ActionResult<ResponseModel<List<UsuarioModel>>>> Editar(UsuarioEdicaoDto usuarioEdicaoDto)
    {
        var usuario = await _usuario.Editar(usuarioEdicaoDto);
        return Ok(usuario);
    }

    [HttpDelete("Delete/{idUsuario}")]
    public async Task<ActionResult<ResponseModel<List<UsuarioModel>>>> Delete(int idUsuario)
    {
        var usuario = await _usuario.Delete(idUsuario);
        return Ok(usuario);
    }
}
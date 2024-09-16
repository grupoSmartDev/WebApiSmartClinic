using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.Autor;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.Autor;

namespace WebApiSmartClinic.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AutorController : ControllerBase
{
    private readonly IAutorInterface _autorInterface;
    public AutorController(IAutorInterface autorInterface)
    {
        _autorInterface = autorInterface;
    }

    [HttpGet("ListarAutores")]
    public async Task<ActionResult<ResponseModel<List<AutorModel>>>> ListarAutores()
    {
        var autores = await _autorInterface.ListarAutores();

        return Ok(autores);
    }

    [HttpGet("BuscarAutorPorId/{idAutor}")]
    public async Task<ActionResult<ResponseModel<AutorModel>>> BuscarAutorPorId(int idAutor)
    {
        var autor = await _autorInterface.BuscarAutorPorId(idAutor);
        return Ok(autor);
    }

    [HttpGet("BuscarAutorPorLivroId/{idLivro}")]
    public async Task<ActionResult<ResponseModel<AutorModel>>> BuscarAutorPorLivroId(int idLivro)
    {
        var autor = await _autorInterface.BuscarAutorPorIdLivro(idLivro);
        return Ok(autor);
    }

    [HttpPost("CriarAutor")]
    public async Task<ActionResult<ResponseModel<List<AutorModel>>>> CriarAutor(AutorCreateDto autorCreateDto)
    {
        var autores = await _autorInterface.CriarAutor(autorCreateDto);
        return Ok(autores);
    }

    [HttpPut("EditarAutor")]
    public async Task<ActionResult<ResponseModel<List<AutorModel>>>> EditarAutor(AutorEdicaoDto autorEdicaoDto)
    {
        var autores = await _autorInterface.EditarAutor(autorEdicaoDto);
        return Ok(autores);
    }

    [HttpDelete("ExcluirAutor")]
    public async Task<ActionResult<ResponseModel<List<AutorModel>>>> DeleteAutor(AutorEdicaoDto autorEdicaoDto)
    {
        var autores = await _autorInterface.EditarAutor(autorEdicaoDto);
        return Ok(autores);
    }
}

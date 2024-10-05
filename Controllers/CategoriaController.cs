
using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.Categoria;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.Categoria;

namespace WebApiSmartClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaInterface _categoria;
        public CategoriaController(ICategoriaInterface categoria)
        {
            _categoria = categoria;
        }

        [HttpGet("Listar")]
        public async Task<ActionResult<ResponseModel<List<CategoriaModel>>>> Listar()
        {
            var categoria = await _categoria.Listar();
            return Ok(categoria);
        }

        [HttpGet("BuscarPorId/{idCategoria}")]
        public async Task<ActionResult<ResponseModel<List<CategoriaModel>>>> BuscarPorId(int idCategoria)
        {
            var categoria = await _categoria.BuscarPorId(idCategoria);
            return Ok(categoria);
        }

        [HttpPost("Criar")]
        public async Task<ActionResult<ResponseModel<List<CategoriaModel>>>> Criar(CategoriaCreateDto categoriaCreateDto)
        {
            var categoria = await _categoria.Criar(categoriaCreateDto);
            return Ok(categoria);
        }

        [HttpPut("Editar")]
        public async Task<ActionResult<ResponseModel<List<CategoriaModel>>>> Editar(CategoriaEdicaoDto categoriaEdicaoDto)
        {
            var categoria = await _categoria.Editar(categoriaEdicaoDto);
            return Ok(categoria);
        }

        [HttpDelete("Delete/{idCategoria}")]
        public async Task<ActionResult<ResponseModel<List<CategoriaModel>>>> Delete(int idCategoria)
        {
            var categoria = await _categoria.Delete(idCategoria);
            return Ok(categoria);
        }
    }
}
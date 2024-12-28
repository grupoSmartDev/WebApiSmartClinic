
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
        public async Task<ActionResult<ResponseModel<List<CategoriaModel>>>> Listar([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] int? codigoFiltro = null, [FromQuery] string? nomeFiltro = null, [FromQuery] bool paginar = true)
        {
            var categoria = await _categoria.Listar(pageNumber, pageSize, codigoFiltro, nomeFiltro, paginar);
            return Ok(categoria);
        }

        [HttpGet("BuscarPorId/{idCategoria}")]
        public async Task<ActionResult<ResponseModel<List<CategoriaModel>>>> BuscarPorId(int idCategoria)
        {
            var categoria = await _categoria.BuscarPorId(idCategoria);
            return Ok(categoria);
        }

        [HttpPost("Criar")]
        public async Task<ActionResult<ResponseModel<List<CategoriaModel>>>> Criar(CategoriaCreateDto categoriaCreateDto, int pageNumber = 1, int pageSize = 10)
        {
            var categoria = await _categoria.Criar(categoriaCreateDto, pageNumber, pageSize);
            return Ok(categoria);
        }

        [HttpPut("Editar")]
        public async Task<ActionResult<ResponseModel<List<CategoriaModel>>>> Editar(CategoriaEdicaoDto categoriaEdicaoDto, int pageNumber = 1, int pageSize = 10)
        {
            var categoria = await _categoria.Editar(categoriaEdicaoDto, pageNumber, pageSize);
            return Ok(categoria);
        }

        [HttpDelete("Delete/{idCategoria}")]
        public async Task<ActionResult<ResponseModel<List<CategoriaModel>>>> Delete(int idCategoria, int pageNumber = 1, int pageSize = 10)
        {
            var categoria = await _categoria.Delete(idCategoria, pageNumber, pageSize);
            return Ok(categoria);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.Plano;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.Plano;

namespace WebApiSmartClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanoController : ControllerBase
    {
        private readonly IPlanoInterface _plano;
        public PlanoController(IPlanoInterface plano)
        {
            _plano = plano;
        }

        [HttpGet("Listar")]
        public async Task<ActionResult<ResponseModel<List<PlanoModel>>>> Listar(int pageNumber = 1, int pageSize = 10, int? codigoFiltro = null, string? descricaoFiltro = null, bool paginar = true)
        {
            var plano = await _plano.Listar(pageNumber, pageSize, codigoFiltro, descricaoFiltro, paginar);
            return Ok(plano);
        }

        [HttpGet("BuscarPorId/{idPlano}")]
        public async Task<ActionResult<ResponseModel<List<PlanoModel>>>> BuscarPorId(int idPlano)
        {
            var plano = await _plano.BuscarPorId(idPlano);
            return Ok(plano);
        }

        [HttpPost("Criar")]
        public async Task<ActionResult<ResponseModel<List<PlanoModel>>>> Criar(PlanoCreateDto planoCreateDto, int pageNumber = 1, int pageSize = 10)
        {
            var plano = await _plano.Criar(planoCreateDto, pageNumber, pageSize);
            return Ok(plano);
        }

        [HttpPut("Editar")]
        public async Task<ActionResult<ResponseModel<List<PlanoModel>>>> Editar(PlanoEdicaoDto planoEdicaoDto, int pageNumber = 1, int pageSize = 10)
        {
            var plano = await _plano.Editar(planoEdicaoDto, pageNumber, pageSize);
            return Ok(plano);
        }

        [HttpDelete("Delete/{idPlano}")]
        public async Task<ActionResult<ResponseModel<List<PlanoModel>>>> Delete(int idPlano, int pageNumber = 1, int pageSize = 10)
        {
            var plano = await _plano.Delete(idPlano, pageNumber, pageSize);
            return Ok(plano);
        }
    }
}
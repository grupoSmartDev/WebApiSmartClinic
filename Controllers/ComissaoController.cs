
using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.Comissao;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.Comissao;

namespace WebApiSmartClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComissaoController : ControllerBase
    {
        private readonly IComissaoInterface _comissao;
        public ComissaoController(IComissaoInterface comissao)
        {
            _comissao = comissao;
        }

        [HttpGet("Listar")]
        public async Task<ActionResult<ResponseModel<List<ComissaoModel>>>> Listar(int pageNumber = 1, int pageSize = 10, int? codigoFiltro = null, string? profissionalFiltro = null, bool paginar = true)
        {
            var comissao = await _comissao.Listar(pageNumber, pageSize, codigoFiltro, profissionalFiltro, paginar);
            return Ok(comissao);
        }

        [HttpGet("BuscarPorId/{idComissao}")]
        public async Task<ActionResult<ResponseModel<List<ComissaoModel>>>> BuscarPorId(int idComissao)
        {
            var comissao = await _comissao.BuscarPorId(idComissao);
            return Ok(comissao);
        }

        [HttpPost("Criar")]
        public async Task<ActionResult<ResponseModel<List<ComissaoModel>>>> Criar(ComissaoCreateDto comissaoCreateDto, int pageNumber = 1, int pageSize = 10, bool paginar = true)
        {
            var comissao = await _comissao.Criar(comissaoCreateDto, pageNumber, pageSize, paginar);
            return Ok(comissao);
        }

        [HttpPut("Editar")]
        public async Task<ActionResult<ResponseModel<List<ComissaoModel>>>> Editar(ComissaoEdicaoDto comissaoEdicaoDto, int pageNumber = 1, int pageSize = 10, bool paginar = true)
        {
            var comissao = await _comissao.Editar(comissaoEdicaoDto, pageNumber, pageSize, paginar);
            return Ok(comissao);
        }

        [HttpDelete("Delete/{idComissao}")]
        public async Task<ActionResult<ResponseModel<List<ComissaoModel>>>> Delete(int idComissao, int pageNumber = 1, int pageSize = 10, bool paginar = true)
        {
            var comissao = await _comissao.Delete(idComissao, pageNumber, pageSize, paginar);
            return Ok(comissao);
        }

        [HttpGet("ObterPercentualComissao/{procedimentoId}")]
        public async Task<ActionResult<ResponseModel<decimal>>> ObterPercentualComissao(int procedimentoId)
        {
            var comissao = await _comissao.ObterPercentualComissao(procedimentoId);
            return Ok(comissao);
        }

        [HttpGet("ObterComissoesPendentes/{profissionalId}")]
        public async Task<ActionResult<ResponseModel<List<ComissaoModel>>>> ObterComissoesPendentes(int profissionalId)
        {
            var comissao = await _comissao.ObterComissoesPendentes(profissionalId);
            return Ok(comissao);
        }

        [HttpPost("PagarComissao")]
        public async Task<ActionResult<ResponseModel<ComissaoModel>>> PagarComissao(int comissaoId)
        {
            var comissao = await _comissao.PagarComissao(comissaoId);
            return Ok(comissao);
        }
    }
}
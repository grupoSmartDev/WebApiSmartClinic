
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
        public async Task<ActionResult<ResponseModel<List<ComissaoModel>>>> Listar()
        {
            var comissao = await _comissao.Listar();
            return Ok(comissao);
        }

        [HttpGet("BuscarPorId/{idComissao}")]
        public async Task<ActionResult<ResponseModel<List<ComissaoModel>>>> BuscarPorId(int idComissao)
        {
            var comissao = await _comissao.BuscarPorId(idComissao);
            return Ok(comissao);
        }

        [HttpPost("Criar")]
        public async Task<ActionResult<ResponseModel<List<ComissaoModel>>>> Criar(ComissaoCreateDto comissaoCreateDto)
        {
            var comissao = await _comissao.Criar(comissaoCreateDto);
            return Ok(comissao);
        }

        [HttpPut("Editar")]
        public async Task<ActionResult<ResponseModel<List<ComissaoModel>>>> Editar(ComissaoEdicaoDto comissaoEdicaoDto)
        {
            var comissao = await _comissao.Editar(comissaoEdicaoDto);
            return Ok(comissao);
        }

        [HttpDelete("Delete/{idComissao}")]
        public async Task<ActionResult<ResponseModel<List<ComissaoModel>>>> Delete(int idComissao)
        {
            var comissao = await _comissao.Delete(idComissao);
            return Ok(comissao);
        }

        [HttpGet("ObterPercentualComissao/{procedimentoId}")]
        public async Task<ActionResult<ResponseModel<decimal>>> ObterPercentualComissao(int procedimentoId)
        {
            var comissao = await _comissao.ObterPercentualComissao(procedimentoId);
            return Ok(comissao);
        }

        public async Task<ActionResult<ResponseModel<List<ComissaoModel>>>> ObterComissoesPendentes(int profissionalId)
        {
            var comissao = await _comissao.ObterComissoesPendentes(profissionalId);
            return Ok(comissao);
        }

        public async Task<ActionResult<ResponseModel<ComissaoModel>>> PagarComissao(int comissaoId)
        {
            var comissao = await _comissao.PagarComissao(comissaoId);
            return Ok(comissao);
        }
    }
}
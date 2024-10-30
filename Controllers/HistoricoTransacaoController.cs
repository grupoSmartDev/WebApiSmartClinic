
using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.HistoricoTransacao;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.HistoricoTransacao;

namespace WebApiSmartClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoricoTransacaoController : ControllerBase
    {
        private readonly IHistoricoTransacaoInterface _historicotransacao;
        public HistoricoTransacaoController(IHistoricoTransacaoInterface historicotransacao)
        {
            _historicotransacao = historicotransacao;
        }

        [HttpGet("Listar")]
        public async Task<ActionResult<ResponseModel<List<HistoricoTransacaoModel>>>> Listar()
        {
            var historicotransacao = await _historicotransacao.Listar();
            return Ok(historicotransacao);
        }

        [HttpGet("BuscarPorId/{idHistoricoTransacao}")]
        public async Task<ActionResult<ResponseModel<List<HistoricoTransacaoModel>>>> BuscarPorId(int idHistoricoTransacao)
        {
            var historicotransacao = await _historicotransacao.BuscarPorId(idHistoricoTransacao);
            return Ok(historicotransacao);
        }

        [HttpPost("Criar")]
        public async Task<ActionResult<ResponseModel<List<HistoricoTransacaoModel>>>> Criar(HistoricoTransacaoCreateDto historicotransacaoCreateDto)
        {
            var historicotransacao = await _historicotransacao.Criar(historicotransacaoCreateDto);
            return Ok(historicotransacao);
        }

        [HttpPut("Editar")]
        public async Task<ActionResult<ResponseModel<List<HistoricoTransacaoModel>>>> Editar(HistoricoTransacaoEdicaoDto historicotransacaoEdicaoDto)
        {
            var historicotransacao = await _historicotransacao.Editar(historicotransacaoEdicaoDto);
            return Ok(historicotransacao);
        }

        [HttpDelete("Delete/{idHistoricoTransacao}")]
        public async Task<ActionResult<ResponseModel<List<HistoricoTransacaoModel>>>> Delete(int idHistoricoTransacao)
        {
            var historicotransacao = await _historicotransacao.Delete(idHistoricoTransacao);
            return Ok(historicotransacao);
        }
    }
}
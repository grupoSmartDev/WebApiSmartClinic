
using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.Financ_Receber;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.Financ_Receber;

namespace WebApiSmartClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Financ_ReceberController : ControllerBase
    {
        private readonly IFinanc_ReceberInterface _financ_receber;
        public Financ_ReceberController(IFinanc_ReceberInterface financ_receber)
        {
            _financ_receber = financ_receber;
        }

        [HttpGet("Listar")]
        public async Task<ActionResult<ResponseModel<List<Financ_ReceberModel>>>> Listar(int pageNumber = 1, int pageSize = 10, int? codigoFiltro = null, string? descricaoFiltro = null, DateTime? dataEmissaoInicio = null, DateTime? dataEmissaoFim = null,
            decimal? valorMinimoFiltro = null, decimal? valorMaximoFiltro = null, int? parcelaNumeroFiltro = null, DateTime? vencimentoInicio = null, DateTime? vencimentoFim = null, bool paginar = true)
        {
            var financ_receber = await _financ_receber.Listar(pageNumber, pageSize, codigoFiltro, descricaoFiltro, dataEmissaoInicio, dataEmissaoFim, valorMinimoFiltro, valorMaximoFiltro, parcelaNumeroFiltro, vencimentoInicio, vencimentoFim, paginar);
            return Ok(financ_receber);
        }

        [HttpGet("BuscarPorId/{idFinanc_Receber}")]
        public async Task<ActionResult<ResponseModel<List<Financ_ReceberModel>>>> BuscarPorId(int idFinanc_Receber)
        {
            var financ_receber = await _financ_receber.BuscarPorId(idFinanc_Receber);
            return Ok(financ_receber);
        }

        [HttpPost("Criar")]
        public async Task<ActionResult<ResponseModel<List<Financ_ReceberModel>>>> Criar(Financ_ReceberCreateDto financ_receberCreateDto, int pageNumber = 1, int pageSize = 10)
        {
            var financ_receber = await _financ_receber.Criar(financ_receberCreateDto, pageNumber, pageSize);
            return Ok(financ_receber);
        }

        [HttpPut("Editar")]
        public async Task<ActionResult<ResponseModel<List<Financ_ReceberModel>>>> Editar(Financ_ReceberEdicaoDto financ_receberEdicaoDto, int pageNumber = 1, int pageSize = 10)
        {
            var financ_receber = await _financ_receber.Editar(financ_receberEdicaoDto, pageNumber, pageSize);
            return Ok(financ_receber);
        }

        [HttpDelete("Delete/{idFinanc_Receber}")]
        public async Task<ActionResult<ResponseModel<List<Financ_ReceberModel>>>> Delete(int idFinanc_Receber, int pageNumber = 1, int pageSize = 10)
        {
            var financ_receber = await _financ_receber.Delete(idFinanc_Receber);
            return Ok(financ_receber);
        }

        [HttpPost("QuitarParcela/{idParcela}")]
        public async Task<ActionResult<ResponseModel<Financ_ReceberSubModel>>> QuitarParcela(int idParcela, decimal valorPago)
        {
            var resultado = await _financ_receber.QuitarParcela(idParcela, valorPago);
            return Ok(resultado);
        }

        [HttpPost("EstornarParcela/{idParcela}")]
        public async Task<ActionResult<ResponseModel<string>>> EstornarParcela(int idParcela)
        {
            var resultado = await _financ_receber.EstornarParcela(idParcela);
            return Ok(resultado);
        }

        [HttpGet("CalcularTotalRecebiveis")]
        public async Task<ActionResult<ResponseModel<decimal>>> CalcularTotalRecebiveis([FromQuery] int cliente = 0, [FromQuery] DateTime? dataInicio = null, [FromQuery] DateTime? dataFim = null)
        {
            var resultado = await _financ_receber.CalcularTotalRecebiveis(cliente, dataInicio, dataFim);
            return Ok(resultado);
        }

        [HttpGet("BuscarContasEmAberto")]
        public async Task<ActionResult<ResponseModel<List<Financ_ReceberModel>>>> BuscarContasEmAberto()
        {
            var resultado = await _financ_receber.BuscarContasEmAberto();
            return Ok(resultado);
        }
    }
}
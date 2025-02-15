using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebApiSmartClinic.Dto.Financ_Pagar;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.Financ_Pagar;

namespace WebApiSmartClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Financ_PagarController : ControllerBase
    {
        private readonly IFinanc_PagarInterface _financ_pagar;
        public Financ_PagarController(IFinanc_PagarInterface financ_pagar)
        {
            _financ_pagar = financ_pagar;
        }

        [HttpGet("Listar")]
        public async Task<ActionResult<ResponseModel<List<Financ_PagarModel>>>> Listar([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] int? codigoFiltro = null, [FromQuery] string? descricaoFiltro = null, [FromQuery] DateTime? dataEmissaoInicio = null, [FromQuery] DateTime? dataEmissaoFim = null,
             [FromQuery] decimal? valorMinimoFiltro = null, [FromQuery] decimal? valorMaximoFiltro = null, [FromQuery] int? parcelaNumeroFiltro = null, [FromQuery] DateTime? vencimentoInicio = null, [FromQuery] DateTime? vencimentoFim = null, [FromQuery] bool paginar = true)
        {
            var financ_pagar = await _financ_pagar.Listar(pageNumber, pageSize, codigoFiltro, descricaoFiltro, dataEmissaoInicio = null, dataEmissaoFim, valorMinimoFiltro, valorMaximoFiltro, parcelaNumeroFiltro, vencimentoInicio, vencimentoFim, paginar);
            return Ok(financ_pagar);
        }

        [HttpGet("ListarAnalitico")]
        public async Task<ActionResult<ResponseModel<List<Financ_PagarModel>>>> ListarAnalitico([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] int? codigoFiltro = null, [FromQuery] string? descricaoFiltro = null, [FromQuery] DateTime? dataEmissaoInicio = null, [FromQuery] DateTime? dataEmissaoFim = null,
             [FromQuery] decimal? valorMinimoFiltro = null, [FromQuery] decimal? valorMaximoFiltro = null, [FromQuery] int? parcelaNumeroFiltro = null, [FromQuery] DateTime? vencimentoInicio = null, [FromQuery] DateTime? vencimentoFim = null, [FromQuery] bool paginar = true)
        {
            var financ_pagar = await _financ_pagar.ListarAnalitico(pageNumber, pageSize, codigoFiltro, descricaoFiltro, dataEmissaoInicio = null, dataEmissaoFim, valorMinimoFiltro, valorMaximoFiltro, parcelaNumeroFiltro, vencimentoInicio, vencimentoFim, paginar);
            return Ok(financ_pagar);
        }

        [HttpGet("ListarSintetico")]
        public async Task<ActionResult<ResponseModel<List<Financ_PagarModel>>>> ListarSintetico([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] int? idPaiFiltro = null, [FromQuery] int? parcelaNumeroFiltro = null, [FromQuery] DateTime? vencimentoInicio = null, [FromQuery] DateTime? vencimentoFim = null, [FromQuery] bool paginar = true)
        {
            var financ_pagar = await _financ_pagar.ListarSintetico(pageNumber, pageSize, idPaiFiltro, parcelaNumeroFiltro, vencimentoInicio, vencimentoFim, paginar);
            return Ok(financ_pagar);
        }

        [HttpGet("BuscarPorId/{idFinanc_Pagar}")]
        public async Task<ActionResult<ResponseModel<List<Financ_PagarModel>>>> BuscarPorId(int idFinanc_Pagar)
        {
            var financ_pagar = await _financ_pagar.BuscarPorId(idFinanc_Pagar);
            return Ok(financ_pagar);
        }

        [HttpPost("Criar")]
        public async Task<ActionResult<ResponseModel<List<Financ_PagarModel>>>> Criar(Financ_PagarCreateDto financ_pagarCreateDto, int pageNumber = 1, int pageSize = 10)
        {
            var financ_pagar = await _financ_pagar.Criar(financ_pagarCreateDto, pageNumber, pageSize);
            return Ok(financ_pagar);
        }

        [HttpPut("Editar")]
        public async Task<ActionResult<ResponseModel<List<Financ_PagarModel>>>> Editar(Financ_PagarEdicaoDto financ_pagarEdicaoDto, int pageNumber = 1, int pageSize = 10)
        {
            var financ_pagar = await _financ_pagar.Editar(financ_pagarEdicaoDto, pageNumber, pageSize);
            return Ok(financ_pagar);
        }

        [HttpDelete("Delete/{idFinanc_Pagar}")]
        public async Task<ActionResult<ResponseModel<List<Financ_PagarModel>>>> Delete(int idFinanc_Pagar, int pageNumber = 1, int pageSize = 10)
        {
            var financ_pagar = await _financ_pagar.Delete(idFinanc_Pagar, pageNumber, pageSize);
            return Ok(financ_pagar);
        }

        [HttpPost("BaixarParcela/{parcelaId}")]
        public async Task<ActionResult<ResponseModel<Financ_PagarModel>>> BaixarParcela(int parcelaId, [FromBody] decimal valorPago)
        {
            var resultado = await _financ_pagar.BaixarParcela(parcelaId, valorPago);
            return Ok(resultado);
        }

        [HttpPost("AgruparParcelas/{idPai}")]
        public async Task<ActionResult<ResponseModel<string>>> AgruparParcelas(int idPai, [FromBody] dynamic body)
        {
            List<int> parcelasFilhasIds = body.ParcelasFilhasIds.ToObject<List<int>>();
            decimal valorPago = body.ValorPago;

            var resultado = await _financ_pagar.AgruparParcelas(idPai, parcelasFilhasIds, valorPago);
            return Ok(resultado);
        }

        [HttpPost("EstornarParcela/{parcelaId}")]
        public async Task<ActionResult<ResponseModel<string>>> EstornarParcela(int parcelaId)
        {
            var resultado = await _financ_pagar.EstornarParcela(parcelaId);
            return Ok(resultado);
        }

        [HttpPost("EstornarAgrupamento")]
        public async Task<ActionResult<ResponseModel<string>>> EstornarAgrupamento([FromBody] List<int> parcelasIds)
        {
            var resultado = await _financ_pagar.EstornarAgrupamento(parcelasIds);
            return Ok(resultado);
        }

        [HttpGet("ObterContasAbertas")]
        public async Task<ActionResult<ResponseModel<List<Financ_PagarSubModel>>>> ObterContasAbertas()
        {
            var resultado = await _financ_pagar.ObterContasAbertas();
            return Ok(resultado);
        }
    }
}


//[HttpPost("BaixarPagamento/{idFinanc_Pagar}")]
//[SwaggerOperation(
//    Summary = "Baixa um pagamento especifico",
//    Description = "Realiza a baixa de um pagamento, alterando o status para 'Pago' e definindo a data de pagamento como a data atual."
//)]
//[SwaggerResponse(200, "Pagamento baixado com sucesso", typeof(ResponseModel<Financ_PagarModel>))]
//[SwaggerResponse(404, "Pagamento não encontrado")]
//[SwaggerResponse(500, "Erro interno")]
//public async Task<IActionResult> BaixarPagamento(int idFinanc_Pagar, decimal valorPago, DateTime? dataPagamento = null)
//{
//    var resposta = await _financ_pagar.BaixarPagamento(idFinanc_Pagar, valorPago, dataPagamento);
//    if (!resposta.Status)
//    {
//        return BadRequest(resposta);
//    }
//    return Ok(resposta);
//}

//[HttpPost("EstornarPagamento/{idFinanc_Pagar}")]
//public async Task<IActionResult> EstornarPagamento(int idFinanc_Pagar)
//{
//    var resposta = await _financ_pagar.EstornarPagamento(idFinanc_Pagar);
//    if (!resposta.Status)
//    {
//        return BadRequest(resposta);
//    }
//    return Ok(resposta);
//}

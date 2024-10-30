
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
        public async Task<ActionResult<ResponseModel<List<Financ_PagarModel>>>> Listar()
        {
            var financ_pagar = await _financ_pagar.Listar();
            return Ok(financ_pagar);
        }

        [HttpGet("BuscarPorId/{idFinanc_Pagar}")]
        public async Task<ActionResult<ResponseModel<List<Financ_PagarModel>>>> BuscarPorId(int idFinanc_Pagar)
        {
            var financ_pagar = await _financ_pagar.BuscarPorId(idFinanc_Pagar);
            return Ok(financ_pagar);
        }

        [HttpPost("Criar")]
        public async Task<ActionResult<ResponseModel<List<Financ_PagarModel>>>> Criar(Financ_PagarCreateDto financ_pagarCreateDto)
        {
            var financ_pagar = await _financ_pagar.Criar(financ_pagarCreateDto);
            return Ok(financ_pagar);
        }

        [HttpPut("Editar")]
        public async Task<ActionResult<ResponseModel<List<Financ_PagarModel>>>> Editar(Financ_PagarEdicaoDto financ_pagarEdicaoDto)
        {
            var financ_pagar = await _financ_pagar.Editar(financ_pagarEdicaoDto);
            return Ok(financ_pagar);
        }

        [HttpDelete("Delete/{idFinanc_Pagar}")]
        public async Task<ActionResult<ResponseModel<List<Financ_PagarModel>>>> Delete(int idFinanc_Pagar)
        {
            var financ_pagar = await _financ_pagar.Delete(idFinanc_Pagar);
            return Ok(financ_pagar);
        }

        [HttpPost("BaixarPagamento/{idFinanc_Pagar}")]
        [SwaggerOperation(Summary = "Baixa um pagamento especifico",
                  Description = "Realiza a baixa de um pagamento, alterando o status para 'Pago' e definindo a data de pagamento como a data atual.")]
        [SwaggerResponse(200, "Pagamento baixado com sucesso", typeof(ResponseModel<Financ_PagarModel>))]
        [SwaggerResponse(404, "Pagamento não encontrado")]
        [SwaggerResponse(500, "Erro interno")]

        public async Task<ActionResult<ResponseModel<Financ_PagarModel>>> BaixarPagamento(int idFinanc_Pagar, decimal valorPago, DateTime? dataPagamento = null)
        {
            var resultado = await _financ_pagar.BaixarPagamento(idFinanc_Pagar, valorPago, dataPagamento);
            if (resultado.Status == false)
            {
                return NotFound(resultado);
            }
            return Ok(resultado);
        }

    }
}
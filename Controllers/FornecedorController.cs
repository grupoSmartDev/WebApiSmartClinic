
using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.Fornecedor;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.Fornecedor;

namespace WebApiSmartClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FornecedorController : ControllerBase
    {
        private readonly IFornecedorInterface _fornecedor;
        public FornecedorController(IFornecedorInterface fornecedor)
        {
            _fornecedor = fornecedor;
        }

        [HttpGet("Listar")]
        public async Task<ActionResult<ResponseModel<List<FornecedorModel>>>> ListarFornecedor([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] int? codigoFiltro = null, [FromQuery] string? nomeFiltro = null, [FromQuery] string? cpfFiltro = null, [FromQuery] string? cnpjFiltro = null, [FromQuery] string? celularFiltro = null, [FromQuery] bool paginar = true)
        {
            var fornecedor = await _fornecedor.Listar(pageNumber, pageSize, codigoFiltro, nomeFiltro, cpfFiltro, cnpjFiltro, celularFiltro, paginar);
            return Ok(fornecedor);
        }

        [HttpGet("BuscarPorId/{idFornecedor}")]
        public async Task<ActionResult<ResponseModel<List<FornecedorModel>>>> BuscarFornecedorPorId(int idFornecedor)
        {
            var fornecedor = await _fornecedor.BuscarPorId(idFornecedor);
            return Ok(fornecedor);
        }

        [HttpPost("Criar")]
        public async Task<ActionResult<ResponseModel<List<FornecedorModel>>>> CriarFornecedor(FornecedorCreateDto fornecedorCreateDto, int pageNumber = 1, int pageSize = 10)
        {
            var fornecedor = await _fornecedor.Criar(fornecedorCreateDto, pageNumber, pageSize);
            return Ok(fornecedor);
        }

        [HttpPut("Editar")]
        public async Task<ActionResult<ResponseModel<List<FornecedorModel>>>> EditarFornecedor(FornecedorEdicaoDto fornecedorEdicaoDto, int pageNumber = 1, int pageSize = 10)
        {
            var fornecedor = await _fornecedor.Editar(fornecedorEdicaoDto, pageNumber, pageSize);
            return Ok(fornecedor);
        }

        [HttpDelete("Delete/{idFornecedor}")]
        public async Task<ActionResult<ResponseModel<List<FornecedorModel>>>> DeleteFornecedor(int idFornecedor, int pageNumber = 1, int pageSize = 10)
        {
            var fornecedor = await _fornecedor.Delete(idFornecedor, pageNumber, pageSize);
            return Ok(fornecedor);
        }
    }
}
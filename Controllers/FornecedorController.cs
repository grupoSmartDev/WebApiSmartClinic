
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
        public async Task<ActionResult<ResponseModel<List<FornecedorModel>>>> ListarFornecedor()
        {
            var fornecedor = await _fornecedor.ListarFornecedor();
            return Ok(fornecedor);
        }

        [HttpGet("BuscarPorId/{idFornecedor}")]
        public async Task<ActionResult<ResponseModel<List<FornecedorModel>>>> BuscarFornecedorPorId(int idFornecedor)
        {
            var fornecedor = await _fornecedor.BuscarFornecedorPorId(idFornecedor);
            return Ok(fornecedor);
        }

        [HttpPost("Criar")]
        public async Task<ActionResult<ResponseModel<List<FornecedorModel>>>> CriarFornecedor(FornecedorCreateDto fornecedorCreateDto)
        {
            var fornecedor = await _fornecedor.CriarFornecedor(fornecedorCreateDto);
            return Ok(fornecedor);
        }

        [HttpPut("Editar")]
        public async Task<ActionResult<ResponseModel<List<FornecedorModel>>>> EditarFornecedor(FornecedorEdicaoDto fornecedorEdicaoDto)
        {
            var fornecedor = await _fornecedor.EditarFornecedor(fornecedorEdicaoDto);
            return Ok(fornecedor);
        }

        [HttpDelete("Delete/{idFornecedor}")]
        public async Task<ActionResult<ResponseModel<List<FornecedorModel>>>> DeleteFornecedor(int idFornecedor)
        {
            var fornecedor = await _fornecedor.DeleteFornecedor(idFornecedor);
            return Ok(fornecedor);
        }
    }
}
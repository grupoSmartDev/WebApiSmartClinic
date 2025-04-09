using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.CadastroCliente;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.CadastroCliente;

namespace WebApiSmartClinic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CadastroClienteController : ControllerBase
    {
        private readonly ICadastroClienteInterface _cadastrocliente;
        public CadastroClienteController(ICadastroClienteInterface cadastrocliente)
        {
            _cadastrocliente = cadastrocliente;
        }

        [AllowAnonymous]
        [HttpPost("Criar")]
        public async Task<ActionResult<ResponseModel<List<CadastroClienteModel>>>> Criar([FromBody] CadastroClienteCreateDto cadastroclienteCreateDto)
        {
            var cadastrocliente = await _cadastrocliente.Criar(cadastroclienteCreateDto);
            return Ok(cadastrocliente);
        }
        [HttpGet("Listar")]
        public async Task<ActionResult<ResponseModel<List<CadastroClienteModel>>>> Listar()
        {
            var cadastrocliente = await _cadastrocliente.Listar();
            return Ok(cadastrocliente);
        }

        [HttpGet("BuscarPorId/{idCadastroCliente}")]
        public async Task<ActionResult<ResponseModel<List<CadastroClienteModel>>>> BuscarPorId(int idCadastroCliente)
        {
            var cadastrocliente = await _cadastrocliente.BuscarPorId(idCadastroCliente);
            return Ok(cadastrocliente);
        }

        [HttpPut("Editar")]
        public async Task<ActionResult<ResponseModel<List<CadastroClienteModel>>>> Editar(CadastroClienteEdicaoDto cadastroclienteEdicaoDto)
        {
            var cadastrocliente = await _cadastrocliente.Editar(cadastroclienteEdicaoDto);
            return Ok(cadastrocliente);
        }

        [HttpDelete("Delete/{idCadastroCliente}")]
        public async Task<ActionResult<ResponseModel<List<CadastroClienteModel>>>> Delete(int idCadastroCliente)
        {
            var cadastrocliente = await _cadastrocliente.Delete(idCadastroCliente);
            return Ok(cadastrocliente);
        }
    }
}
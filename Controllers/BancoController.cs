
using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.Banco;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.Banco;

namespace WebApiSmartClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BancoController : ControllerBase
    {
        private readonly IBancoInterface _banco;
        public BancoController(IBancoInterface banco)
        {
            _banco = banco;
        }

        [HttpGet("Listar")]
        public async Task<ActionResult<ResponseModel<List<BancoModel>>>> Listar()
        {
            var banco = await _banco.Listar();
            return Ok(banco);
        }

        [HttpGet("BuscarPorId/{idBanco}")]
        public async Task<ActionResult<ResponseModel<List<BancoModel>>>> BuscarPorId(int idBanco)
        {
            var banco = await _banco.BuscarPorId(idBanco);
            return Ok(banco);
        }

        [HttpPost("Criar")]
        public async Task<ActionResult<ResponseModel<List<BancoModel>>>> Criar(BancoCreateDto bancoCreateDto)
        {
            var banco = await _banco.Criar(bancoCreateDto);
            return Ok(banco);
        }

        [HttpPut("Editar")]
        public async Task<ActionResult<ResponseModel<List<BancoModel>>>> Editar(BancoEdicaoDto bancoEdicaoDto)
        {
            var banco = await _banco.Editar(bancoEdicaoDto);
            return Ok(banco);
        }

        [HttpDelete("Delete/{idBanco}")]
        public async Task<ActionResult<ResponseModel<List<BancoModel>>>> Delete(int idBanco)
        {
            var banco = await _banco.Delete(idBanco);
            return Ok(banco);
        }
    }
}
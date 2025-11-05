using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Services.Empresa;

namespace WebApiSmartClinic.Controllers;

public sealed class EmpresaController : Controller
{

    private readonly EmpresaService _companyService;

    public EmpresaController(EmpresaService companyService)
    {
        _companyService = companyService;
    }
    [HttpPost("create")]
    [Authorize(Policy = "AdminPolicy")] // Apenas administradores podem criar empresas
    public async Task<IActionResult> CreateCompany([FromBody] EmpresaCreateDto dto)
    {


        var result = await _companyService.CreateNewDatabaseForCompany(dto.Nome);

        if (result)
            return Ok("Empresa criada com sucesso.");

        return BadRequest("Falha ao criar empresa.");
    }
}

public sealed class EmpresaCreateDto
{
    public string Nome { get; set; }
}

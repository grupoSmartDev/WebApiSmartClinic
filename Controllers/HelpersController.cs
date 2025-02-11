using Microsoft.AspNetCore.Mvc;

namespace WebApiSmartClinic.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HelpersController : Controller
{
    private readonly HttpClient _httpClient;

    public HelpersController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet("BuscarCep/{cep}")]
    public async Task<IActionResult> BuscarCep(string cep)
    {
        try
        {
            var response = await _httpClient.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return Ok(content);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

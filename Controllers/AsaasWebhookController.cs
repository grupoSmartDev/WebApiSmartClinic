using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Dto.Asaas;
using WebApiSmartClinic.Services.Asaas;

namespace WebApiSmartClinic.Controllers;

[ApiController]
[Route("api/Asaas")]
public sealed class AsaasWebhookController : ControllerBase
{
    private readonly AsaasWebhookService _webhookService;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AsaasWebhookController> _logger;

    public AsaasWebhookController(
        AsaasWebhookService webhookService,
        IConfiguration configuration,
        ILogger<AsaasWebhookController> logger)
    {
        _webhookService = webhookService;
        _configuration = configuration;
        _logger = logger;
    }

    // O Asaas chama este endpoint sem JWT -> precisa ser anônimo para o ConnectionStringMiddleware
    // deixar passar. Opcionalmente valida o header "asaas-access-token" contra Asaas:WebhookToken
    // (configure o mesmo valor no painel do Asaas). Se o token não estiver configurado, a checagem
    // é ignorada.
    [HttpPost("Webhook")]
    [AllowAnonymous]
    public async Task<IActionResult> Webhook([FromBody] AsaasWebhookEventDto dto)
    {
        var expectedToken = _configuration["Asaas:WebhookToken"];
        if (!string.IsNullOrWhiteSpace(expectedToken))
        {
            var received = Request.Headers["asaas-access-token"].FirstOrDefault();
            if (!string.Equals(received, expectedToken, StringComparison.Ordinal))
            {
                _logger.LogWarning("Webhook Asaas rejeitado: token inválido.");
                return Unauthorized(new { received = false });
            }
        }

        try
        {
            await _webhookService.ProcessarEventoAsync(dto);
        }
        catch (Exception ex)
        {
            // Devolve 500 para o Asaas reprocessar o evento numa próxima tentativa em vez de perdê-lo.
            _logger.LogError(ex, "Falha ao processar webhook Asaas (evento: {Event}).", dto?.Event);
            return StatusCode(StatusCodes.Status500InternalServerError, new { received = false });
        }

        return Ok(new { received = true });
    }
}

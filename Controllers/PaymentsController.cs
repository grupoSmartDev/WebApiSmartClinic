using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Services.StripeService;
using static WebApiSmartClinic.Models.stripe.StripeModels;

namespace WebApiSmartClinic.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class PaymentController : ControllerBase
{
    private readonly IStripeService _stripeService;
    private readonly IConfiguration _configuration;

    public PaymentController(IStripeService stripeService, IConfiguration configuration)
    {
        _stripeService = stripeService;
        _configuration = configuration;

}

    [HttpPost("create-customer")]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest request)
    {
        try
        {
            var customerId = await _stripeService.CreateCustomerAsync(request);
            return Ok(new { CustomerId = customerId });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpPost("create-payment-intent")]
    public async Task<IActionResult> CreatePaymentIntent([FromBody] CreatePaymentIntentRequest request)
    {
        try
        {
            var response = await _stripeService.CreatePaymentIntentAsync(request);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpPost("webhook")]
    public async Task<IActionResult> HandleWebhook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        var stripeSignature = Request.Headers["Stripe-Signature"];

        var success = await _stripeService.ProcessWebhookAsync(json, stripeSignature);

        return success ? Ok() : BadRequest();
    }

    [HttpGet("config")]
    public IActionResult GetConfig()
    {
        return Ok(new
        {
            PublishableKey = _configuration["Stripe:PublishableKey"]
        });
    }
}


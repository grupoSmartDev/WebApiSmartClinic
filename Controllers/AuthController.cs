using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using WebApiSmartClinic.Dto.User;
using WebApiSmartClinic.Services.Auth;

namespace WebApiSmartClinic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthInterface _authService;

        public AuthController(IAuthInterface authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var userKey = HttpContext.Request.Headers["UserKey"].FirstOrDefault();
            var result = await _authService.LoginAsync(model, userKey);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserCreateRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var userKey = HttpContext.Request.Headers["UserKey"].FirstOrDefault();
            var result = await _authService.RegisterAsync(model, userKey);

            return Ok(result);
        }

        [Authorize(Roles = "Admin,Support")]
        [HttpGet("Listar")]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 100, [FromQuery] string? filter = null)
        {
            var result = await _authService.GetAllUsersAsync(page, pageSize, filter);
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            var result = await _authService.GetUserByIdAsync(id, User);
            return Ok(result);
        }

        [Authorize(Roles = "Admin,Support")]
        [HttpPut("Update/{id}"), DisableRequestSizeLimit]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update(string id, [FromForm] UserUpdateRequest model)
        {
            var result = await _authService.UpdateUserAsync(id, model, User);

            return Ok(result);
        }

        [HttpPut("Editar/{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Editar([FromRoute] string id, [FromForm] UserUpdateRequest model)
        {
            var result = await _authService.Editar(id, model);
            return Ok(result);
        }

        [Authorize(Roles = "Admin,Support")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _authService.DeleteUserAsync(id);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("products")]
        public async Task<IActionResult> Products()
        {
            StripeConfiguration.ApiKey = "";
            var options = new ProductListOptions
            {
                Limit = 10,
            };
            var service = new ProductService();
            StripeList<Product> products = service.List(options);
            return Ok(products);
        }
    }
}
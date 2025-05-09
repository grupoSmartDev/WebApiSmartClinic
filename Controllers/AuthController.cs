using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiSmartClinic.Services.Auth;
using WebApiSmartClinic.Dto.User;
using WebApiSmartClinic.Helpers;

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

            // Aqui voc� decide como retorna a resposta
            // Exemplo: se "success" est� false, retorne 400, se true, retorne 200
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
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 100, [FromQuery] string? filter = null)
        {
            var result = await _authService.GetAllUsersAsync(page, pageSize, filter);
            return Ok(result);
        }

        [ClaimsAuthorize("Usu�rio", "Visualizar")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _authService.GetUserByIdAsync(id, User);
            return Ok(result);
        }

        [ClaimsAuthorize("Usu�rio", "Alterar")]
        [HttpPut("{id}"), DisableRequestSizeLimit]
        public async Task<IActionResult> Update(string id, [FromForm] UserUpdateRequest model, IFormFile? profilePicture)
        {
            var result = await _authService.UpdateUserAsync(id, model, profilePicture, User);
            return Ok(result);
        }

        [Authorize(Roles = "Admin,Support")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _authService.DeleteUserAsync(id);
            return Ok(result);
        }
    }
}
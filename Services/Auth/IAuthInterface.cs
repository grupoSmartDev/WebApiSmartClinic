using System.Security.Claims;
using WebApiSmartClinic.Dto.User;

namespace WebApiSmartClinic.Services.Auth
{
    public interface IAuthInterface
    {
        Task<object> LoginAsync(UserLoginRequest model, string? userKey);
        Task<object> RegisterAsync(UserCreateRequest model, string? userKey);
        Task<object> GetAllUsersAsync(int page, int pageSize, string? filter);
        Task<object> GetUserByIdAsync(string id, ClaimsPrincipal currentUser);
        Task<object> UpdateUserAsync(string id, UserUpdateRequest model, ClaimsPrincipal currentUser);
        Task<object> Editar(string id, UserUpdateRequest model, ClaimsPrincipal currentUser);
        Task<object> DeleteUserAsync(string id);
    }
}

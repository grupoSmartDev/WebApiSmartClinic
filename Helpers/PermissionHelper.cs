using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Helpers;

public sealed class RoleFlags
{
    public RoleFlags(bool ehAdmin, bool ehSupport, bool ehUser)
    {
        EhAdmin = ehAdmin;
        EhSupport = ehSupport;
        EhUser = ehUser;
    }

    public bool EhAdmin { get; }
    public bool EhSupport { get; }
    public bool EhUser { get; }
}

public static class PermissionHelper
{
    public static bool IsSupportOrAdmin(ClaimsPrincipal principal)
    {
        return principal?.Identity?.IsAuthenticated == true &&
               (principal.IsInRole(Perfis.Admin) || principal.IsInRole(Perfis.Support));
    }

    public static bool CanManageUser(string targetUserId, ClaimsPrincipal principal)
    {
        var requesterId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(requesterId)) return false;

        return requesterId == targetUserId || IsSupportOrAdmin(principal);
    }

    public static async Task<RoleFlags> ResolveRolesAsync(UserManager<User> userManager, string? usuarioId)
    {
        if (string.IsNullOrWhiteSpace(usuarioId))
        {
            return new RoleFlags(false, false, false);
        }

        var user = await userManager.FindByIdAsync(usuarioId);
        if (user is null)
        {
            return new RoleFlags(false, false, false);
        }

        var roles = await userManager.GetRolesAsync(user);
        var ehAdmin = roles.Contains(Perfis.Admin);
        var ehSupport = roles.Contains(Perfis.Support);
        var ehUser = roles.Contains(Perfis.User) && !ehAdmin && !ehSupport;

        return new RoleFlags(ehAdmin, ehSupport, ehUser);
    }
}

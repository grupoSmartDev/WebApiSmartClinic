using System.Security.Claims;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Services.EmpresaPermissao;

namespace WebApiSmartClinic.Helpers;

/// <summary>
/// Define no AppDbContext a Empresa selecionada e se o usuário pode ver todas.
/// Lê do header "X-Empresa-Id" quando aplicável.
/// </summary>
public class EmpresaContextoMiddleware
{
    private readonly RequestDelegate _next;
    public EmpresaContextoMiddleware(RequestDelegate next) => _next = next;

    string? ObterUsuarioId(HttpContext ctx)
    {
        // nameidentifier (Identity), sub (JWT comum), uid/UserId (variações)
        return ctx.User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? ctx.User.FindFirst("sub")?.Value
            ?? ctx.User.FindFirst("uid")?.Value
            ?? ctx.User.FindFirst("UserId")?.Value;
    }

    public async Task Invoke(HttpContext ctx, AppDbContext db, IEmpresaPermissaoInterface perm)
    {
        // Se caiu aqui sem autenticar, não prossiga (protege backoffice)
        if (!ctx.User.Identity?.IsAuthenticated ?? true)
        {
            await _next(ctx);
            return;
        }

        db.UsuarioAtualId = ObterUsuarioId(ctx);   // <<< aqui deixa de ficar nulo

        db.VerTodasEmpresas = await perm.UsuarioPodeVerTodasEmpresasAsync(db.UsuarioAtualId);

        if (!db.VerTodasEmpresas)
        {
            if (int.TryParse(ctx.Request.Headers["X-Empresa-Id"], out var empId) &&
                await perm.UsuarioTemAcessoEmpresaAsync(db.UsuarioAtualId, empId))
            {
                db.EmpresaSelecionada = empId;
            }
            else
            {
                db.EmpresaSelecionada = await perm.ObterEmpresaPadraoAsync(db.UsuarioAtualId);
            }
        }

        await _next(ctx);
    }

    //public async Task Invoke(HttpContext ctx, AppDbContext db, IEmpresaPermissaoInterface perm)
    //{
    //    // Usuário logado (Identity)
    //    var usuarioId = ctx.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
    //    db.UsuarioAtualId = usuarioId;

    //    // Pode ver todas?
    //    db.VerTodasEmpresas = await perm.UsuarioPodeVerTodasEmpresasAsync(usuarioId);

    //    // Seleção explícita de empresa via header (para quem NÃO pode ver todas, é ignorado se não tiver acesso)
    //    if (!db.VerTodasEmpresas)
    //    {
    //        if (int.TryParse(ctx.Request.Headers["X-Empresa-Id"], out var empId) &&
    //            await perm.UsuarioTemAcessoEmpresaAsync(usuarioId, empId))
    //        {
    //            db.EmpresaSelecionada = empId;
    //        }
    //        else
    //        {
    //            db.EmpresaSelecionada = await perm.ObterEmpresaPadraoAsync(usuarioId);
    //        }
    //    }

    //    await _next(ctx);
    //}
}

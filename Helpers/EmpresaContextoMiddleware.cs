using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Helpers;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.EmpresaPermissao;

public class EmpresaContextoMiddleware
{
    private const string FlagKey = "__EmpresaContexto_Init";
    private readonly RequestDelegate _next;
    private readonly ILogger<EmpresaContextoMiddleware> _logger;

    public EmpresaContextoMiddleware(RequestDelegate next, ILogger<EmpresaContextoMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(
        HttpContext ctx,
        AppDbContext db,
        UserManager<User> userManager,
        IEmpresaPermissaoInterface permissao)
    {
        if (!ctx.Request.Path.StartsWithSegments("/api") || HttpMethods.IsOptions(ctx.Request.Method))
        {
            await _next(ctx);
            return;
        }

        if (ctx.Items.ContainsKey(FlagKey))
        {
            await _next(ctx);
            return;
        }

        ctx.Items[FlagKey] = true;

        var usuarioId = ctx.User?.FindFirst("sub")?.Value
                     ?? ctx.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        db.UsuarioAtualId = usuarioId;

        bool ehAdmin = false, ehSupport = false, ehUser = false;

        if (ctx.User?.Identity?.IsAuthenticated == true && !string.IsNullOrEmpty(usuarioId))
        {
            var flags = await PermissionHelper.ResolveRolesAsync(userManager, usuarioId);
            ehAdmin = flags.EhAdmin;
            ehSupport = flags.EhSupport;
            ehUser = flags.EhUser;
        }

        db.VerTodasEmpresas = ehAdmin;

        if (ctx.Request.Headers.TryGetValue("X-Empresa-Id", out var h) && int.TryParse(h, out var empresaHeader))
        {
            if (!string.IsNullOrEmpty(usuarioId) && await permissao.UsuarioTemAcessoEmpresaAsync(usuarioId, empresaHeader))
                db.EmpresaSelecionada = empresaHeader;
        }

        if (db.EmpresaSelecionada is null && !string.IsNullOrEmpty(usuarioId))
            db.EmpresaSelecionada = await permissao.ObterEmpresaPadraoAsync(usuarioId);

        int? profissionalAtualId = null;

        if (ehUser)
        {
            if (!await db.Database.CanConnectAsync())
            {
                await Task.Delay(200);
                if (!await db.Database.CanConnectAsync())
                {
                    ctx.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                    await ctx.Response.WriteAsync("Banco do tenant indisponivel no momento. Tente novamente.");
                    return;
                }
            }

            try
            {
                profissionalAtualId = await db.Profissional
                    .AsNoTracking()
                    .Where(p => p.UsuarioId == usuarioId)
                    .Select(p => (int?)p.Id)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha ao consultar Profissional do usuario {UserId}. Erro base: {Base}",
                    usuarioId, ex.GetBaseException().Message);

                ctx.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                await ctx.Response.WriteAsync("Nao foi possivel carregar o contexto do profissional. Tente novamente.");
                return;
            }
        }

        db.EhAdmin = ehAdmin;
        db.EhSupport = ehSupport;
        db.EhUser = ehUser;
        db.ProfissionalAtualId = profissionalAtualId;

        await _next(ctx);
    }
}

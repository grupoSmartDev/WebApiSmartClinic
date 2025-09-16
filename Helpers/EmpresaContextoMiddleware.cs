using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Helpers;
using WebApiSmartClinic.Models;
using WebApiSmartClinic.Services.EmpresaPermissao;

public class EmpresaContextoMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<EmpresaContextoMiddleware> _logger;

    public EmpresaContextoMiddleware(RequestDelegate next, ILogger<EmpresaContextoMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext ctx,
                                  AppDbContext db,
                                  UserManager<User> userManager,
                                  IEmpresaPermissaoInterface permissao)
    {
        // 1) Usuário atual (pelo JWT)
        var usuarioId = ctx.User?.FindFirst("sub")?.Value
                     ?? ctx.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        db.UsuarioAtualId = usuarioId;

        // 2) Papéis
        bool ehAdmin = false, ehSupport = false, ehUser = false;
        int? profissionalAtualId = null;

        if (!string.IsNullOrEmpty(usuarioId))
        {
            var user = await userManager.FindByIdAsync(usuarioId);
            var roles = user != null ? await userManager.GetRolesAsync(user) : new List<string>();

            ehAdmin = roles.Contains(Perfis.Admin);
            ehSupport = roles.Contains(Perfis.Support);
            ehUser = roles.Contains(Perfis.User) && !ehAdmin && !ehSupport;

            // 3) Profissional do usuário (se for "User", deve existir)
            if (ehUser)
            {
                // Com filtro por empresa já calculado, esta consulta pega o profissional do usuário
                // na empresa selecionada (ou padrão), respeitando o filtro global de Empresa.
                profissionalAtualId = await db.Profissional
                    .AsNoTracking()
                    .Where(p => p.UsuarioId == usuarioId)
                    .Select(p => p.Id)
                    .FirstOrDefaultAsync();

                if (profissionalAtualId == 0)
                {
                    _logger.LogWarning("Usuário {User} com papel User, mas sem vínculo de Profissional.", usuarioId);
                    // Se quiser bloquear o uso sem vínculo, descomente:
                    // ctx.Response.StatusCode = StatusCodes.Status403Forbidden;
                    // await ctx.Response.WriteAsync("Usuário sem vínculo de Profissional.");
                    // return;
                }
            }

        }

        // 4) Empresa selecionada e escopo de visualização
        //    - Admin: pode ver TODAS as empresas do tenant
        //    - Support: vê APENAS a empresa selecionada (NÃO pode ver todas)
        //    - User: vê APENAS a empresa selecionada (ou padrão)
        db.VerTodasEmpresas = ehAdmin; // <<< SOMENTE ADMIN agora

        // Seleção de empresa via header (validado) ou padrão
        if (!db.VerTodasEmpresas)
        {
            int? empresaHeader = null;
            if (ctx.Request.Headers.TryGetValue("X-Empresa-Id", out var h) && int.TryParse(h, out var eid))
                empresaHeader = eid;

            if (empresaHeader.HasValue)
            {
                var temAcesso = await permissao.UsuarioTemAcessoEmpresaAsync(usuarioId!, empresaHeader.Value);
                if (temAcesso) db.EmpresaSelecionada = empresaHeader.Value;
            }

            if (db.EmpresaSelecionada is null && !string.IsNullOrEmpty(usuarioId))
                db.EmpresaSelecionada = await permissao.ObterEmpresaPadraoAsync(usuarioId);
        }

        // 5) Flags no DbContext (usadas no filtro global e no SaveChanges)
        db.EhAdmin = ehAdmin;
        db.EhSupport = ehSupport;
        db.EhUser = ehUser;
        db.ProfissionalAtualId = profissionalAtualId;

        await _next(ctx);
    }
}


//using System.Security.Claims;
//using WebApiSmartClinic.Data;
//using WebApiSmartClinic.Services.EmpresaPermissao;

//namespace WebApiSmartClinic.Helpers;

///// <summary>
///// Define no AppDbContext a Empresa selecionada e se o usuário pode ver todas.
///// Lê do header "X-Empresa-Id" quando aplicável.
///// </summary>
//public class EmpresaContextoMiddleware
//{
//    private readonly RequestDelegate _next;
//    public EmpresaContextoMiddleware(RequestDelegate next) => _next = next;

//    string? ObterUsuarioId(HttpContext ctx)
//    {
//        // nameidentifier (Identity), sub (JWT comum), uid/UserId (variações)
//        return ctx.User.FindFirstValue(ClaimTypes.NameIdentifier)
//            ?? ctx.User.FindFirst("sub")?.Value
//            ?? ctx.User.FindFirst("uid")?.Value
//            ?? ctx.User.FindFirst("UserId")?.Value;
//    }

//    public async Task Invoke(HttpContext ctx, AppDbContext db, IEmpresaPermissaoInterface perm)
//    {
//        // Se caiu aqui sem autenticar, não prossiga (protege backoffice)
//        if (!ctx.User.Identity?.IsAuthenticated ?? true)
//        {
//            await _next(ctx);
//            return;
//        }

//        db.UsuarioAtualId = ObterUsuarioId(ctx);   // <<< aqui deixa de ficar nulo

//        db.VerTodasEmpresas = await perm.UsuarioPodeVerTodasEmpresasAsync(db.UsuarioAtualId);

//        if (!db.VerTodasEmpresas)
//        {
//            if (int.TryParse(ctx.Request.Headers["X-Empresa-Id"], out var empId) &&
//                await perm.UsuarioTemAcessoEmpresaAsync(db.UsuarioAtualId, empId))
//            {
//                db.EmpresaSelecionada = empId;
//            }
//            else
//            {
//                db.EmpresaSelecionada = await perm.ObterEmpresaPadraoAsync(db.UsuarioAtualId);
//            }
//        }

//        await _next(ctx);
//    }

//    //public async Task Invoke(HttpContext ctx, AppDbContext db, IEmpresaPermissaoInterface perm)
//    //{
//    //    // Usuário logado (Identity)
//    //    var usuarioId = ctx.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
//    //    db.UsuarioAtualId = usuarioId;

//    //    // Pode ver todas?
//    //    db.VerTodasEmpresas = await perm.UsuarioPodeVerTodasEmpresasAsync(usuarioId);

//    //    // Seleção explícita de empresa via header (para quem NÃO pode ver todas, é ignorado se não tiver acesso)
//    //    if (!db.VerTodasEmpresas)
//    //    {
//    //        if (int.TryParse(ctx.Request.Headers["X-Empresa-Id"], out var empId) &&
//    //            await perm.UsuarioTemAcessoEmpresaAsync(usuarioId, empId))
//    //        {
//    //            db.EmpresaSelecionada = empId;
//    //        }
//    //        else
//    //        {
//    //            db.EmpresaSelecionada = await perm.ObterEmpresaPadraoAsync(usuarioId);
//    //        }
//    //    }

//    //    await _next(ctx);
//    //}
//}

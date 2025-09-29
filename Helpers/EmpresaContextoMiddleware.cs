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
        // Curto-circuitos (opcional, mas recomendado)
        if (!ctx.Request.Path.StartsWithSegments("/api") || HttpMethods.IsOptions(ctx.Request.Method))
        {
            await _next(ctx); return;
        }
        if (ctx.Items.ContainsKey("__EmpresaContexto_Init")) { await _next(ctx); return; }
        ctx.Items["__EmpresaContexto_Init"] = true;

        // 1) Usuário atual
        var usuarioId = ctx.User?.FindFirst("sub")?.Value
                     ?? ctx.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        db.UsuarioAtualId = usuarioId;

        bool ehAdmin = false, ehSupport = false, ehUser = false;

        if (ctx.User?.Identity?.IsAuthenticated == true && !string.IsNullOrEmpty(usuarioId))
        {
            var user = await userManager.FindByIdAsync(usuarioId);
            var roles = user is null ? Array.Empty<string>() : await userManager.GetRolesAsync(user);
            ehAdmin = roles.Contains(Perfis.Admin);
            ehSupport = roles.Contains(Perfis.Support);
            ehUser = roles.Contains(Perfis.User) && !ehAdmin && !ehSupport;
        }

        // 2) Escopo de empresa ANTES de consultar Profissional
        db.VerTodasEmpresas = ehAdmin;

        if (!db.VerTodasEmpresas)
        {
            // Header opcional
            if (ctx.Request.Headers.TryGetValue("X-Empresa-Id", out var h) && int.TryParse(h, out var empresaHeader))
            {
                if (!string.IsNullOrEmpty(usuarioId) && await permissao.UsuarioTemAcessoEmpresaAsync(usuarioId, empresaHeader))
                    db.EmpresaSelecionada = empresaHeader;
            }

            // Padrão (se ainda não definido)
            if (db.EmpresaSelecionada is null && !string.IsNullOrEmpty(usuarioId))
                db.EmpresaSelecionada = await permissao.ObterEmpresaPadraoAsync(usuarioId);
        }
        else
        {
            db.EmpresaSelecionada = 0;
        }

        // 3) Agora sim, se for User, buscar o Profissional
        int? profissionalAtualId = null;

        if (ehUser)
        {
            // (opcional) sanity check de conexão
            if (!await db.Database.CanConnectAsync())
            {
                // pequeno retry rápido (evita flake em DB recém-criado)
                await Task.Delay(200);
                if (!await db.Database.CanConnectAsync())
                {
                    ctx.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                    await ctx.Response.WriteAsync("Banco do tenant indisponível no momento. Tente novamente.");
                    return;
                }
            }

            try
            {
                profissionalAtualId = await db.Profissional
                    .AsNoTracking()
                    .Where(p => p.UsuarioId == usuarioId)
                    .Select(p => (int?)p.Id) // nullable-safe
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha ao consultar Profissional do usuário {UserId}. Erro base: {Base}",
                    usuarioId, ex.GetBaseException().Message);

                // Trate como indisponibilidade transitória
                ctx.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                await ctx.Response.WriteAsync("Não foi possível carregar o contexto do profissional. Tente novamente.");
                return;
            }
        }

        // 4) Seta flags finais no DbContext (usadas no filtro global e SaveChanges)
        db.EhAdmin = ehAdmin;
        db.EhSupport = ehSupport;
        db.EhUser = ehUser;
        db.ProfissionalAtualId = profissionalAtualId;

        await _next(ctx);
    }


    //public async Task InvokeAsync(
    //    HttpContext ctx,
    //    AppDbContext db,
    //    UserManager<User> userManager,
    //    IEmpresaPermissaoInterface permissao)
    //{
    //    // 0) Atalhos: não-API, CORS preflight, já inicializado, rota de erro
    //    //if (!ctx.Request.Path.StartsWithSegments("/api"))
    //    //{
    //    //    await _next(ctx);
    //    //    return;
    //    //}

    //    if (HttpMethods.IsOptions(ctx.Request.Method))
    //    {
    //        await _next(ctx);
    //        return;
    //    }

    //    if (ctx.Items.ContainsKey(FlagKey))
    //    {
    //        await _next(ctx);
    //        return;
    //    }

    //    // se estiver na reexecução do ExceptionHandler, evite refazer
    //    var ehReexecucaoErro = ctx.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>() != null;
    //    if (ehReexecucaoErro)
    //    {
    //        await _next(ctx);
    //        return;
    //    }

    //    // marca como inicializado para este request
    //    ctx.Items[FlagKey] = true;

    //    // 1) Usuário atual (pelo JWT)
    //    var usuarioId = ctx.User?.FindFirst("sub")?.Value
    //                 ?? ctx.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    //    db.UsuarioAtualId = usuarioId;

    //    // 2) Papéis
    //    bool ehAdmin = false, ehSupport = false, ehUser = false;
    //    int? profissionalAtualId = null;

    //    if (ctx.User?.Identity?.IsAuthenticated == true && !string.IsNullOrEmpty(usuarioId))
    //    {
    //        var user = await userManager.FindByIdAsync(usuarioId);
    //        var roles = user is null ? Array.Empty<string>() : await userManager.GetRolesAsync(user);

    //        ehAdmin = roles.Contains(Perfis.Admin);
    //        ehSupport = roles.Contains(Perfis.Support);
    //        ehUser = roles.Contains(Perfis.User) && !ehAdmin && !ehSupport;

    //        if (ehUser)
    //        {
    //            profissionalAtualId = await db.Profissional
    //                .AsNoTracking()
    //                .Where(p => p.UsuarioId == usuarioId)
    //                .Select(p => p.Id)   // <- nullable para evitar cast
    //                .FirstOrDefaultAsync();

    //            if (profissionalAtualId is null)
    //                _logger.LogWarning("User {UserId} com papel User, sem vínculo Profissional.", usuarioId);
    //        }
    //    }

    //    // 3) Empresa selecionada / escopo
    //    db.VerTodasEmpresas = ehAdmin; // somente Admin vê todas

    //    if (!db.VerTodasEmpresas)
    //    {
    //        // header opcional
    //        if (ctx.Request.Headers.TryGetValue("X-Empresa-Id", out var h) && int.TryParse(h, out var eid))
    //        {
    //            if (!string.IsNullOrEmpty(usuarioId))
    //            {
    //                var temAcesso = await permissao.UsuarioTemAcessoEmpresaAsync(usuarioId, eid);
    //                if (temAcesso) db.EmpresaSelecionada = eid;
    //            }
    //        }

    //        // padrão
    //        if (db.EmpresaSelecionada is null && !string.IsNullOrEmpty(usuarioId))
    //            db.EmpresaSelecionada = await permissao.ObterEmpresaPadraoAsync(usuarioId);
    //    }

    //    // 4) Flags no DbContext
    //    db.EhAdmin = ehAdmin;
    //    db.EhSupport = ehSupport;
    //    db.EhUser = ehUser;
    //    db.ProfissionalAtualId = profissionalAtualId;

    //    await _next(ctx);
    //}
}


//public class EmpresaContextoMiddleware
//{
//    private readonly RequestDelegate _next;
//    private readonly ILogger<EmpresaContextoMiddleware> _logger;

//    public EmpresaContextoMiddleware(RequestDelegate next, ILogger<EmpresaContextoMiddleware> logger)
//    {
//        _next = next;
//        _logger = logger;
//    }

//    public async Task InvokeAsync(HttpContext ctx, AppDbContext db, UserManager<User> userManager, IEmpresaPermissaoInterface permissao)
//    {
//        // 1) Usuário atual (pelo JWT)
//        var usuarioId = ctx.User?.FindFirst("sub")?.Value
//                     ?? ctx.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

//        db.UsuarioAtualId = usuarioId;

//        // 2) Papéis
//        bool ehAdmin = false, ehSupport = false, ehUser = false;
//        int? profissionalAtualId = null;

//        if (!string.IsNullOrEmpty(usuarioId))
//        {
//            var user = await userManager.FindByIdAsync(usuarioId);
//            var roles = user != null ? await userManager.GetRolesAsync(user) : new List<string>();

//            ehAdmin = roles.Contains(Perfis.Admin);
//            ehSupport = roles.Contains(Perfis.Support);
//            ehUser = roles.Contains(Perfis.User) && !ehAdmin && !ehSupport;

//            // 3) Profissional do usuário (se for "User", deve existir)
//            if (ehUser)
//            {
//                // Com filtro por empresa já calculado, esta consulta pega o profissional do usuário
//                // na empresa selecionada (ou padrão), respeitando o filtro global de Empresa.
//                profissionalAtualId = await db.Profissional
//                    .AsNoTracking()
//                    .Where(p => p.UsuarioId == usuarioId)
//                    .Select(p => p.Id)
//                    .FirstOrDefaultAsync();

//                if (profissionalAtualId == 0)
//                {
//                    _logger.LogWarning("Usuário {User} com papel User, mas sem vínculo de Profissional.", usuarioId);
//                    // Se quiser bloquear o uso sem vínculo, descomente:
//                    // ctx.Response.StatusCode = StatusCodes.Status403Forbidden;
//                    // await ctx.Response.WriteAsync("Usuário sem vínculo de Profissional.");
//                    // return;
//                }
//            }

//        }

//        // 4) Empresa selecionada e escopo de visualização
//        //    - Admin: pode ver TODAS as empresas do tenant
//        //    - Support: vê APENAS a empresa selecionada (NÃO pode ver todas)
//        //    - User: vê APENAS a empresa selecionada (ou padrão)
//        db.VerTodasEmpresas = ehAdmin; // <<< SOMENTE ADMIN agora

//        // Seleção de empresa via header (validado) ou padrão
//        if (!db.VerTodasEmpresas)
//        {
//            int? empresaHeader = null;
//            if (ctx.Request.Headers.TryGetValue("X-Empresa-Id", out var h) && int.TryParse(h, out var eid))
//                empresaHeader = eid;

//            if (empresaHeader.HasValue)
//            {
//                var temAcesso = await permissao.UsuarioTemAcessoEmpresaAsync(usuarioId!, empresaHeader.Value);
//                if (temAcesso) db.EmpresaSelecionada = empresaHeader.Value;
//            }

//            if (db.EmpresaSelecionada is null && !string.IsNullOrEmpty(usuarioId))
//                db.EmpresaSelecionada = await permissao.ObterEmpresaPadraoAsync(usuarioId);
//        }

//        // 5) Flags no DbContext (usadas no filtro global e no SaveChanges)
//        db.EhAdmin = ehAdmin;
//        db.EhSupport = ehSupport;
//        db.EhUser = ehUser;
//        db.ProfissionalAtualId = profissionalAtualId;

//        await _next(ctx);
//    }
//}
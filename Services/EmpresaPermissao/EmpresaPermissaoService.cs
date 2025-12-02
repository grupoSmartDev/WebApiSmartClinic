using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Helpers;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.EmpresaPermissao;

public class EmpresaPermissaoService : IEmpresaPermissaoInterface
{
    private readonly AppDbContext _db;
    private readonly UserManager<User> _userManager;

    public EmpresaPermissaoService(AppDbContext db, UserManager<User> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    public async Task<bool> UsuarioPodeVerTodasEmpresasAsync(string? usuarioId)
    {
        usuarioId ??= _db.UsuarioAtualId;
        if (string.IsNullOrWhiteSpace(usuarioId)) return false;

        var flags = await PermissionHelper.ResolveRolesAsync(_userManager, usuarioId);
        return flags.EhAdmin;
    }

    public async Task<bool> UsuarioTemAcessoEmpresaAsync(string? usuarioId, int empresaId)
    {
        usuarioId ??= _db.UsuarioAtualId;

        if (string.IsNullOrWhiteSpace(usuarioId)) return false;

        return await _db.UsuarioEmpresas
            .AsNoTracking()
            .AnyAsync(x => x.UsuarioId == usuarioId && x.EmpresaId == empresaId);
    }

    public async Task<int> ObterEmpresaPadraoAsync(string? usuarioId)
    {
        usuarioId ??= _db.UsuarioAtualId; // fallback seguro
        if (string.IsNullOrWhiteSpace(usuarioId))
            throw new InvalidOperationException("Usuário não autenticado.");

        var padrao = await _db.UsuarioEmpresas
            .AsNoTracking()
            .Where(x => x.UsuarioId == usuarioId && x.EmpresaPadrao)
            .Select(x => x.EmpresaId)
            .FirstOrDefaultAsync();

        if (padrao != 0) return padrao;

        var primeira = await _db.UsuarioEmpresas
            .AsNoTracking()
            .Where(x => x.UsuarioId == usuarioId)
            .Select(x => x.EmpresaId)
            .FirstOrDefaultAsync();

        if (primeira == 0)
            throw new InvalidOperationException("Usuário não possui empresas vinculadas.");

        return primeira;
    }

    public async Task<bool> UsuarioPodeExcluirAsync(string usuarioId, int empresaId)
    {
        // 1) Papéis que “podem tudo”? Se SIM, devolver true aqui.
        var flags = await PermissionHelper.ResolveRolesAsync(_userManager, usuarioId);
        if (flags.EhAdmin || flags.EhSupport)
            return true; // se essa for sua regra

        // 2) Caso contrário, consulta UsuarioEmpresas
        return await _db.UsuarioEmpresas
            //.AsNoTracking()
            .Where(x => x.UsuarioId == usuarioId && x.EmpresaId == empresaId)
            .Select(x => x.PodeExcluir)
            .FirstOrDefaultAsync();
    }

    //public async Task<int> ObterEmpresaPadraoAsync(string? usuarioId)
    //{
    //    var padrao = await _db.UsuarioEmpresas
    //        .AsNoTracking()
    //        .Where(x => x.UsuarioId == usuarioId && x.EmpresaPadrao)
    //        .Select(x => x.EmpresaId)
    //        .FirstOrDefaultAsync();

    //    if (padrao != 0) return padrao;

    //    // Se não tiver padrão, pega a primeira empresa disponível
    //    return await _db.UsuarioEmpresas
    //        .AsNoTracking()
    //        .Where(x => x.UsuarioId == usuarioId)
    //        .Select(x => x.EmpresaId)
    //        .FirstOrDefaultAsync();
    //}
}

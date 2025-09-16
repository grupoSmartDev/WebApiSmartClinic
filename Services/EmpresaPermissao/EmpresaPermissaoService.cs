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
        // Fallback: usa o usuário atual do DbContext se o parâmetro vier nulo
        usuarioId ??= _db.UsuarioAtualId;
        if (string.IsNullOrWhiteSpace(usuarioId)) return false;

        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == usuarioId);
        if (user == null) return false;

        var roles = await _userManager.GetRolesAsync(user);
        // Admin e Support podem enxergar TODAS as empresas do tenant
        return roles.Contains(Perfis.Admin);
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
        var user = await _userManager.FindByIdAsync(usuarioId);
        var roles = await _userManager.GetRolesAsync(user!);
        if (roles.Contains(Perfis.Admin) || roles.Contains(Perfis.Support))
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
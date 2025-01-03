using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;

namespace WebApiSmartClinic.Middleware;

public class EmpresasMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<EmpresasMiddleware> _logger;

    public EmpresasMiddleware(
        RequestDelegate next,
        IServiceScopeFactory serviceScopeFactory,
        ILogger<EmpresasMiddleware> logger)
    {
        _next = next;
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            var tenantId = context.User?.FindFirst("empresa")?.Value;

            if (string.IsNullOrEmpty(tenantId))
            {
                _logger.LogWarning("TenantId não encontrado no token.");
                throw new Exception("TenantId inválido.");
            }

            using var scope = _serviceScopeFactory.CreateScope();
            var masterContext = scope.ServiceProvider.GetRequiredService<MasterDbContext>();

            var tenant = await masterContext.Empresas
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Identificador == tenantId && t.Ativo);

            if (tenant == null)
            {
                _logger.LogError("Tenant não encontrado ou inativo: {TenantId}", tenantId);
                throw new Exception("Tenant não encontrado.");
            }

            context.Items["TenantConnection"] = tenant.DatabaseConnectionString;
            _logger.LogInformation("Tenant configurado com sucesso: {TenantId}", tenantId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao configurar tenant");
            throw;
        }

        await _next(context);
    }
}

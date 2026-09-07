using Microsoft.EntityFrameworkCore;
using WebApiSmartClinic.Data;
using WebApiSmartClinic.Dto.Asaas;
using WebApiSmartClinic.Helpers;
using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Services.Asaas;

// Processa os eventos de webhook do Asaas. O request chega SEM tenant (é [AllowAnonymous] e
// o Asaas não manda header/claim UserKey), então este serviço resolve o banco do cliente
// a partir do externalReference ("clinicsmart_cpf_{cpfKey}"): extrai a Key, busca a
// connection string em DataConnections e abre um AppDbContext com escopo próprio — mesmo
// padrão usado em CadastroClienteService.Criar.
public class AsaasWebhookService
{
    private readonly DataConnectionContext _dataConnections;
    private readonly IConnectionStringProvider _connectionStringProvider;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<AsaasWebhookService> _logger;

    public AsaasWebhookService(
        DataConnectionContext dataConnections,
        IConnectionStringProvider connectionStringProvider,
        IServiceScopeFactory scopeFactory,
        ILogger<AsaasWebhookService> logger)
    {
        _dataConnections = dataConnections;
        _connectionStringProvider = connectionStringProvider;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    public async Task<bool> ProcessarEventoAsync(AsaasWebhookEventDto dto)
    {
        if (dto == null || string.IsNullOrWhiteSpace(dto.Event))
            return false;

        _logger.LogInformation("Webhook Asaas recebido: {Event}", dto.Event);

        switch (dto.Event)
        {
            case "PAYMENT_RECEIVED":
            case "PAYMENT_CONFIRMED":
                await ProcessarPagamentoConfirmado(dto.Payment);
                break;

            case "PAYMENT_OVERDUE":
                await ProcessarPagamentoVencido(dto.Payment);
                break;

            case "SUBSCRIPTION_INACTIVATED":
            case "SUBSCRIPTION_DELETED":
                await ProcessarAssinaturaInativada(dto.Subscription);
                break;

            default:
                _logger.LogInformation("Evento Asaas sem tratamento: {Event}", dto.Event);
                break;
        }

        return true;
    }

    // 1) Pagamento confirmado/recebido -> LIBERA o plano.
    private Task ProcessarPagamentoConfirmado(AsaasWebhookPaymentDto? payment)
    {
        if (payment == null) return Task.CompletedTask;

        return ComEmpresaAsync(payment.ExternalReference, payment.Customer, payment.Subscription, payment.Id,
            async (db, empresa) =>
            {
                var (plano, semestral) = ExtrairPlanoEPeriodo(payment.Description, empresa);

                empresa.AsaasStatus = "Ativo";
                empresa.PeriodoTeste = false;

                if (!string.IsNullOrWhiteSpace(plano))
                {
                    empresa.PlanoEscolhido = plano;
                    empresa.QtdeLicencaEmpresaPermitida = ObterLicencasPermitidas(plano);
                    empresa.QtdeLicencaUsuarioPermitida = ObterLicencasPermitidas(plano);
                }

                empresa.PeriodoCobranca = semestral ? "semiannual" : "monthly";
                empresa.DataFim = DateTime.UtcNow.AddMonths(semestral ? 6 : 1);

                if (!string.IsNullOrWhiteSpace(payment.Id)) empresa.AsaasPaymentId = payment.Id;
                if (!string.IsNullOrWhiteSpace(payment.Subscription)) empresa.AsaasSubscriptionId = payment.Subscription;

                empresa.AsaasErroDetalhe = null;
                empresa.AsaasUltimaTentativa = DateTime.UtcNow;

                db.Empresas.Update(empresa);
                await db.SaveChangesAsync();

                _logger.LogInformation("Empresa {Id} ativada via webhook Asaas (plano {Plano}, período {Periodo}).",
                    empresa.Id, empresa.PlanoEscolhido, empresa.PeriodoCobranca);
            });
    }

    // 2) Pagamento vencido -> marca Pendente, NÃO bloqueia (pode ser só atraso).
    private Task ProcessarPagamentoVencido(AsaasWebhookPaymentDto? payment)
    {
        if (payment == null) return Task.CompletedTask;

        return ComEmpresaAsync(payment.ExternalReference, payment.Customer, payment.Subscription, payment.Id,
            async (db, empresa) =>
            {
                empresa.AsaasStatus = "Pendente";
                empresa.AsaasUltimaTentativa = DateTime.UtcNow;

                db.Empresas.Update(empresa);
                await db.SaveChangesAsync();

                _logger.LogInformation("Empresa {Id} marcada como Pendente (pagamento vencido no Asaas).", empresa.Id);
            });
    }

    // 3) Assinatura inativada/cancelada -> bloqueia acesso.
    private Task ProcessarAssinaturaInativada(AsaasWebhookSubscriptionDto? subscription)
    {
        if (subscription == null) return Task.CompletedTask;

        return ComEmpresaAsync(subscription.ExternalReference, null, subscription.Id, null,
            async (db, empresa) =>
            {
                empresa.Ativo = false;
                empresa.AsaasStatus = "Cancelado";
                empresa.AsaasUltimaTentativa = DateTime.UtcNow;

                db.Empresas.Update(empresa);
                await db.SaveChangesAsync();

                _logger.LogInformation("Empresa {Id} bloqueada (assinatura {Sub} inativada no Asaas).",
                    empresa.Id, subscription.Id);
            });
    }

    // Resolve o banco do tenant a partir do externalReference, abre um AppDbContext com escopo
    // próprio, localiza a empresa e executa a ação. Qualquer falha de resolução é logada e
    // ignorada (o controller responde 200 pro Asaas não reenfileirar indefinidamente).
    private async Task ComEmpresaAsync(
        string? externalReference,
        string? asaasCustomerId,
        string? asaasSubscriptionId,
        string? asaasPaymentId,
        Func<AppDbContext, EmpresaModel, Task> acao)
    {
        var key = ExtrairTenantKey(externalReference);
        if (string.IsNullOrWhiteSpace(key))
        {
            _logger.LogWarning("Webhook Asaas sem tenant key utilizável no externalReference: '{Ref}'", externalReference);
            return;
        }

        var conn = await _dataConnections.DataConnection
            .AsNoTracking()
            .Where(c => c.Key == key)
            .Select(c => c.StringConnection)
            .FirstOrDefaultAsync();

        if (string.IsNullOrWhiteSpace(conn))
        {
            _logger.LogWarning("Webhook Asaas: nenhuma DataConnection para a key '{Key}'.", key);
            return;
        }

        _connectionStringProvider.SetConnectionString(conn);

        await using var scope = _scopeFactory.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var empresa = await db.Empresas.FirstOrDefaultAsync(e =>
            (asaasSubscriptionId != null && e.AsaasSubscriptionId == asaasSubscriptionId) ||
            (asaasPaymentId != null && e.AsaasPaymentId == asaasPaymentId) ||
            (asaasCustomerId != null && e.AsaasCustomerId == asaasCustomerId));

        empresa ??= await db.Empresas.OrderBy(e => e.Id).FirstOrDefaultAsync();

        if (empresa == null)
        {
            _logger.LogWarning("Webhook Asaas: empresa não encontrada no banco do tenant '{Key}'.", key);
            return;
        }

        await acao(db, empresa);
    }

    // "clinicsmart_cpf_123..." -> "123...". Tolera os formatos antigos "clinicsmart_empresa_{id}"
    // e "clinicsmart_upgrade_empresa_{id}" retornando null (sem tenant key não dá pra resolver o banco).
    internal static string? ExtrairTenantKey(string? externalReference)
    {
        if (string.IsNullOrWhiteSpace(externalReference)) return null;

        const string marker = "cpf_";
        var idx = externalReference.IndexOf(marker, StringComparison.OrdinalIgnoreCase);
        if (idx < 0) return null;

        var rest = externalReference[(idx + marker.Length)..];
        var key = new string(rest.TakeWhile(c => char.IsLetterOrDigit(c) || c == '_').ToArray());
        return string.IsNullOrWhiteSpace(key) ? null : key;
    }

    // Deriva plano ("Basic"/"Plus"/"Premium") e se é semestral a partir da description da cobrança
    // ("SmartClinic - Plano Plus Semestral" / "ClinicSmart - Basic"). Cai pro que já está na empresa
    // quando a description não é conclusiva.
    internal static (string? plano, bool semestral) ExtrairPlanoEPeriodo(string? description, EmpresaModel empresa)
    {
        var d = description ?? string.Empty;

        string? plano = null;
        foreach (var nome in new[] { "Premium", "Plus", "Basic" })
        {
            if (d.Contains(nome, StringComparison.OrdinalIgnoreCase)) { plano = nome; break; }
        }
        plano ??= empresa.PlanoEscolhido;

        bool? semestral = null;
        if (d.Contains("semestral", StringComparison.OrdinalIgnoreCase) ||
            d.Contains("semiannual", StringComparison.OrdinalIgnoreCase))
        {
            semestral = true;
        }
        else if (d.Contains("mensal", StringComparison.OrdinalIgnoreCase) ||
                 d.Contains("monthly", StringComparison.OrdinalIgnoreCase))
        {
            semestral = false;
        }

        semestral ??= string.Equals(empresa.PeriodoCobranca, "semiannual", StringComparison.OrdinalIgnoreCase);
        return (plano, semestral.Value);
    }

    private static int ObterLicencasPermitidas(string plano)
    {
        return plano?.ToLowerInvariant() switch
        {
            "basic" => 1,
            "plus" => 5,
            "premium" => 15,
            _ => 1
        };
    }
}

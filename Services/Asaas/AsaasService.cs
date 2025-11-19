using System.Text;
using System.Text.Json;
using WebApiSmartClinic.Models.Asaas;

namespace WebApiSmartClinic.Services.Asaas;

public interface IAsaasService
{
    Task<AsaasCustomerResponse> CreateCustomerAsync(AsaasCustomerRequest request);
    Task<AsaasSubscriptionResponse> CreateSubscriptionAsync(AsaasSubscriptionRequest request);
    Task<AsaasCustomerResponse> GetCustomerByEmailAsync(string email);
    Task<AsaasPaymentResponse> CreatePaymentAsync(AsaasPaymentRequest request); //pagamento para cartao de credito
}

public class AsaasService : IAsaasService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly string _baseUrl;

    public AsaasService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["Asaas:ApiKey"];
        _baseUrl = configuration["Asaas:BaseUrl"];

        _httpClient.DefaultRequestHeaders.Add("access_token", _apiKey);
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "ClinicSmart/1.0");
    }

    public async Task<AsaasCustomerResponse> CreateCustomerAsync(AsaasCustomerRequest request)
    {
        try
        {
            Console.WriteLine("\n🚀 CRIANDO CUSTOMER NO ASAAS");
            Console.WriteLine($"📧 Email: {request.email}");
            Console.WriteLine($"👤 Nome: {request.name}");
            Console.WriteLine($"📄 CPF: {request.cpfCnpj}");

            var json = JsonSerializer.Serialize(request, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });

            Console.WriteLine($"📤 Request:\n{json}");

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}/customers", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"📥 Response ({response.StatusCode}):\n{responseContent}");

            if (response.IsSuccessStatusCode)
            {
                var customer = JsonSerializer.Deserialize<AsaasCustomerResponse>(responseContent, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                Console.WriteLine($"✅ Customer criado: {customer.id}");
                return customer;
            }
            else
            {
                var error = JsonSerializer.Deserialize<AsaasErrorResponse>(responseContent, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                var errorMsg = error?.errors?.FirstOrDefault()?.description ?? "Erro desconhecido";
                Console.WriteLine($"❌ Erro: {errorMsg}");
                throw new Exception($"Erro Asaas: {errorMsg}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"💥 Exceção: {ex.Message}");
            throw new Exception($"Erro ao criar customer no Asaas: {ex.Message}");
        }
    }

    public async Task<AsaasSubscriptionResponse> CreateSubscriptionAsync(AsaasSubscriptionRequest request)
    {
        try
        {
            Console.WriteLine("\n💳 CRIANDO SUBSCRIPTION NO ASAAS");
            Console.WriteLine($"👤 Customer: {request.customer}");
            Console.WriteLine($"💰 Valor: R$ {request.value:F2}");
            Console.WriteLine($"🔄 Ciclo: {request.cycle}");
            Console.WriteLine($"💳 Tipo: {request.billingType}");

            var json = JsonSerializer.Serialize(request, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });

            Console.WriteLine($"📤 Request:\n{json}");

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}/subscriptions", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"📥 Response ({response.StatusCode}):\n{responseContent}");

            if (response.IsSuccessStatusCode)
            {
                var subscription = JsonSerializer.Deserialize<AsaasSubscriptionResponse>(responseContent, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                Console.WriteLine($"✅ Subscription criada: {subscription.id}");
                return subscription;
            }
            else
            {
                var error = JsonSerializer.Deserialize<AsaasErrorResponse>(responseContent, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                var errorMsg = error?.errors?.FirstOrDefault()?.description ?? "Erro desconhecido";
                Console.WriteLine($"❌ Erro: {errorMsg}");
                throw new Exception($"Erro Asaas: {errorMsg}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"💥 Exceção: {ex.Message}");
            throw new Exception($"Erro ao criar subscription no Asaas: {ex.Message}");
        }
    }

    public async Task<AsaasCustomerResponse> GetCustomerByEmailAsync(string email)
    {
        try
        {
            Console.WriteLine($"🔍 Buscando customer: {email}");

            var response = await _httpClient.GetAsync($"{_baseUrl}/customers?email={Uri.EscapeDataString(email)}");
            var responseContent = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"📥 Response ({response.StatusCode})");

            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<dynamic>(responseContent);
                return null; // Implementar quando necessário
            }

            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"💥 Erro: {ex.Message}");
            return null;
        }
    }

    public async Task<AsaasPaymentResponse> CreatePaymentAsync(AsaasPaymentRequest request)
    {
        try
        {
            Console.WriteLine("\n💳 CRIANDO PAGAMENTO COM CARTÃO NO ASAAS");
            Console.WriteLine($"👤 Customer: {request.customer}");
            Console.WriteLine($"💰 Valor: R$ {request.value:F2}");
            Console.WriteLine($"📅 Vencimento: {request.dueDate:dd/MM/yyyy}");
            Console.WriteLine($"💳 Parcelas: {request.installmentCount ?? 1}x");
            Console.WriteLine($"🔐 Cartão Final: ****{request.creditCard?.number?.Substring(request.creditCard.number.Length - 4)}");

            var json = JsonSerializer.Serialize(request, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            });

            Console.WriteLine($"📤 Request:\n{json}");

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}/payments", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"📥 Response ({response.StatusCode}):\n{responseContent}");

            if (response.IsSuccessStatusCode)
            {
                var payment = JsonSerializer.Deserialize<AsaasPaymentResponse>(responseContent, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                Console.WriteLine($"✅ Pagamento criado: {payment.id}");
                Console.WriteLine($"📊 Status: {payment.status}");
                Console.WriteLine($"🔗 URL da Fatura: {payment.invoiceUrl}");
                return payment;
            }
            else
            {
                var error = JsonSerializer.Deserialize<AsaasErrorResponse>(responseContent, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                var errorMsg = error?.errors?.FirstOrDefault()?.description ?? "Erro desconhecido";
                Console.WriteLine($"❌ Erro: {errorMsg}");

                // Log de todos os erros
                if (error?.errors != null)
                {
                    foreach (var err in error.errors)
                    {
                        Console.WriteLine($"   ⚠️ {err.code}: {err.description}");
                    }
                }

                throw new Exception($"Erro Asaas: {errorMsg}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"💥 Exceção: {ex.Message}");
            throw new Exception($"Erro ao criar pagamento no Asaas: {ex.Message}");
        }
    }
}
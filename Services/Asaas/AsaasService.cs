using System.Text;
using System.Text.Json;
using WebApiSmartClinic.Models.Asaas;

namespace WebApiSmartClinic.Services.Asaas;

public interface IAsaasService
{
    Task<AsaasCustomerResponse> CreateCustomerAsync(AsaasCustomerRequest request);
    Task<AsaasSubscriptionResponse> CreateSubscriptionAsync(AsaasSubscriptionRequest request);
    Task<AsaasCustomerResponse> GetCustomerByEmailAsync(string email);
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
            // FORÇAR CONSOLE OUTPUT
            Console.WriteLine("🚀 INICIANDO CRIAÇÃO DE CUSTOMER");
            Console.WriteLine($"📧 Email: {request.email}");
            Console.WriteLine($"🔑 API Key (primeiros 20 chars): {_apiKey?.Substring(0, Math.Min(20, _apiKey?.Length ?? 0))}");
            Console.WriteLine($"🌐 Base URL: {_baseUrl}");

            var json = JsonSerializer.Serialize(request, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });

            Console.WriteLine($"📄 JSON ENVIADO:\n{json}");

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{_baseUrl}/customers")
            {
                Content = content
            };

            requestMessage.Headers.Add("access_token", _apiKey);
            requestMessage.Headers.Add("User-Agent", "ClinicSmart/1.0");

            Console.WriteLine("📋 HEADERS ENVIADOS:");
            foreach (var header in requestMessage.Headers)
            {
                Console.WriteLine($"  {header.Key}: {string.Join(",", header.Value)}");
            }

            Console.WriteLine("🌐 ENVIANDO REQUISIÇÃO...");
            var response = await _httpClient.SendAsync(requestMessage);
            var responseContent = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"📊 RESPONSE STATUS: {response.StatusCode}");
            Console.WriteLine($"📄 RESPONSE CONTENT:\n{responseContent}");
            Console.WriteLine($"📋 RESPONSE HEADERS:");
            foreach (var header in response.Headers)
            {
                Console.WriteLine($"  {header.Key}: {string.Join(",", header.Value)}");
            }

            if (response.IsSuccessStatusCode)
            {
                var customer = JsonSerializer.Deserialize<AsaasCustomerResponse>(responseContent, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                Console.WriteLine($"✅ CUSTOMER CRIADO COM SUCESSO: {customer.id}");
                return customer;
            }
            else
            {
                Console.WriteLine($"❌ ERRO NO ASAAS - STATUS: {response.StatusCode}");
                Console.WriteLine($"❌ CONTEÚDO DO ERRO: {responseContent}");

                // Tentar fazer parse do erro
                try
                {
                    var error = JsonSerializer.Deserialize<AsaasErrorResponse>(responseContent, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });

                    var errorMsg = error.errors?.FirstOrDefault()?.description ?? "Erro desconhecido";
                    Console.WriteLine($"❌ ERRO PARSEADO: {errorMsg}");
                    throw new Exception($"Erro Asaas: {errorMsg}");
                }
                catch (JsonException jsonEx)
                {
                    Console.WriteLine($"❌ ERRO AO FAZER PARSE DO JSON: {jsonEx.Message}");
                    throw new Exception($"Erro Asaas - Status: {response.StatusCode}, Response: {responseContent}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"💥 EXCEÇÃO GERAL: {ex.Message}");
            Console.WriteLine($"💥 STACK TRACE: {ex.StackTrace}");
            throw new Exception($"Erro ao criar customer no Asaas: {ex.Message}");
        }
    }

    public async Task<AsaasSubscriptionResponse> CreateSubscriptionAsync(AsaasSubscriptionRequest request)
    {
        try
        {
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_baseUrl}/subscriptions", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<AsaasSubscriptionResponse>(responseContent);
            }
            else
            {
                var error = JsonSerializer.Deserialize<AsaasErrorResponse>(responseContent);
                throw new Exception($"Erro Asaas: {error.errors?.FirstOrDefault()?.description}");
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao criar subscription no Asaas: {ex.Message}");
        }
    }

    public async Task<AsaasCustomerResponse> GetCustomerByEmailAsync(string email)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/customers?email={email}");
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<dynamic>(responseContent);
                // Implementar lógica para extrair customer do resultado
                return null; // Por enquanto
            }

            return null;
        }
        catch
        {
            return null;
        }
    }
}

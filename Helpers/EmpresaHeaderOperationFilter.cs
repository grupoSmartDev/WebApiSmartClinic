using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApiSmartClinic.Helpers;

public class EmpresaHeaderOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation == null) return;

        operation.Parameters ??= new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "X-Empresa-Id",
            In = ParameterLocation.Header,
            Description = "Empresa/Filial ativa. Para usuários sem permissão de 'todas', é ignorado.",
            Required = false,
            Schema = new OpenApiSchema { Type = "integer", Format = "int32" }
        });

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "UserKey",
            In = ParameterLocation.Header,
            Description = "UserKey para testes (ex.: chave do usuário). Insira aqui para testar endpoints que dependem desta informação.",
            Required = false,
            Schema = new OpenApiSchema { Type = "string" }
        });
    }
}
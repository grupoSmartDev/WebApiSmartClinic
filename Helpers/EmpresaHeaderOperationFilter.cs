using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApiSmartClinic.Helpers;
public class EmpresaHeaderOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation op, OperationFilterContext ctx)
    {
        op.Parameters ??= new List<OpenApiParameter>();
        op.Parameters.Add(new OpenApiParameter
        {
            Name = "X-Empresa-Id",
            In = ParameterLocation.Header,
            Description = "Empresa/Filial ativa. Para usuários sem permissão de 'todas', é ignorado.",
            Required = false,
            Schema = new OpenApiSchema { Type = "integer", Format = "int32" }
        });
    }
}
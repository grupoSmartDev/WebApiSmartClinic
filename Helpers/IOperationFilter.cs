using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApiSmartClinic.Helpers;

public class AddRequiredHeaderParameter : IOperationFilter
{

    public bool IsDevelopment { get; set; } = false;
    public AddRequiredHeaderParameter(IWebHostEnvironment env)
    {
        if (env.IsDevelopment()) IsDevelopment = true;
    }

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
            operation.Parameters = new List<OpenApiParameter>();

        if (IsDevelopment)
        {
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "UserKey",
                In = ParameterLocation.Header,
                Description = "Chave de acesso",
                Required = true, // Defina como true se a chave for obrigatória em todas as requisições
                Schema = new OpenApiSchema { Type = "string", Default = new OpenApiString("datamentor") }
            });
        }
        else
        {
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "UserKey",
                In = ParameterLocation.Header,
                Description = "Chave de acesso",
                Required = true // Defina como true se a chave for obrigatória em todas as requisições
            });
        }

    }
}

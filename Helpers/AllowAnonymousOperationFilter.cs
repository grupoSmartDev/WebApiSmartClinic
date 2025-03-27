using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApiSmartClinic.Helpers;

public class AllowAnonymousOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var hasAllowAnonymous = context.MethodInfo.GetCustomAttributes(true)
            .OfType<AllowAnonymousAttribute>().Any();

        if (hasAllowAnonymous)
        {
            operation.Security.Clear();
        }
    }
}

//namespace WebApiSmartClinic.Helpers
//{
//    public class AllowAnonymousOperationFilter
//    {
//    }
//}

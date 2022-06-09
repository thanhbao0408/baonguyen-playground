using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BN.CleanArchitecture.Infrastructure.Swagger;
public class AuthorizeCheckOperationFilter : IOperationFilter
{
    public virtual void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
    }
}
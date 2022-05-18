using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BN.CleanArchitecture.Infrastructure.Swagger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Playground.AppContracts;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Blog.Infrastructure;
internal class BlogAuthorizeCheckOperationFilter : AuthorizeCheckOperationFilter
{
    public override void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var hasAuthorize =
          context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any()
          || context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any();

        if (hasAuthorize)
        {
            operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
            operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });

            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                        },
                        new[] { PlaygroundAppConstants.BlogAPIScopeName }
                    }
                }
            };

        }
    }
}

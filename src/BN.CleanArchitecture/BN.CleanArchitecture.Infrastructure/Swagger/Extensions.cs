using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BN.CleanArchitecture.Infrastructure.Swagger;

// https://github.com/dotnet/aspnet-api-versioning/blob/master/samples/aspnetcore/SwaggerSample/Startup.cs
public static class Extensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services,
                                                Type anchor,
                                                Action<SwaggerGenOptions> swaggerGenOptionsBuilder = null)
    {
        // https://github.com/dotnet/aspnet-api-versioning/blob/master/samples/aspnetcore/SwaggerSample/ConfigureSwaggerOptions.cs
        services.AddApiVersioning(
            options =>
            {
                options.ReportApiVersions = true;
            });

        services.AddVersionedApiExplorer(
            options =>
            {
                options.GroupNameFormat = "'v'VVV";

                options.SubstituteApiVersionInUrl = true;
            });

        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        services.AddSwaggerGen(
            options =>
            {
                options.OperationFilter<SwaggerDefaultValues>();

                string xmlFile = XmlCommentsFilePath(anchor);
                if (File.Exists(xmlFile))
                {
                    options.IncludeXmlComments(xmlFile);
                }

                if(swaggerGenOptionsBuilder != null)
                {
                    swaggerGenOptionsBuilder(options);
                }
            }
        );

        return services;

        static string XmlCommentsFilePath(Type anchor)
        {
            string? basePath = PlatformServices.Default.Application.ApplicationBasePath;
            string fileName = anchor.GetTypeInfo().Assembly.GetName().Name + ".xml";
            return Path.Combine(basePath, fileName);
        }
    }

    public static IApplicationBuilder UseSwagger(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
    {
        app.UseSwagger();
        app.UseSwaggerUI(
            options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }

                options.EnablePersistAuthorization();

                options.OAuthClientId("api_swagger");
                options.OAuthAppName("The Vow API- Swagger");
                options.OAuthUsePkce();
            });

        return app;
    }
}
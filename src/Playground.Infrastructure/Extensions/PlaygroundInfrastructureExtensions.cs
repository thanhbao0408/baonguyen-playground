﻿using BN.CleanArchitecture.Infrastructure;
using BN.CleanArchitecture.Infrastructure.AutoMapper;
using BN.CleanArchitecture.Infrastructure.Swagger;
using BN.CleanArchitecture.Infrastructure.Validator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using CoreAnchor = Playground.Core.Anchor;

namespace Playground.Infrastructure.Extensions
{
    public static class PlaygroundInfrastructureExtensions
    {
        private const string CorsName = "api";
        private const string DbName = "postgres";

        public static IServiceCollection AddCoreServices(this IServiceCollection services,
            IConfiguration config, IWebHostEnvironment env, Type apiType)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(CorsName, policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            services.AddHttpContextAccessor();
            services.AddCustomMediatR(new[] { typeof(CoreAnchor) });
            services.AddCustomValidators(new[] { typeof(CoreAnchor) });
            services.AddAutoMapperConfig(typeof(CoreAnchor));

            services.AddControllers();

            services.AddSwagger(apiType,
            options =>
            {
                // https://www.scottbrady91.com/identity-server/aspnet-core-swagger-ui-authorization-using-identityserver4
                // Swagger authentication
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(config.GetValue<string>("SwaggerConfig:AuthorizationBaseUrl") + "/connect/authorize"),
                            TokenUrl = new Uri(config.GetValue<string>("SwaggerConfig:AuthorizationBaseUrl") + "/connect/token"),
                            //Scopes = new Dictionary<string, string>
                            //{
                            //    { PlaygroundAppConstants.BlogApiScopeReadName, PlaygroundAppConstants.BlogApiScopeReadDisplayName },
                            //    { PlaygroundAppConstants.BlogApiScopeWriteName, PlaygroundAppConstants.BlogApiScopeWriteDisplayName },
                            //},
                        },
                        
                    },
                });
                options.OperationFilter<PlaygroundAuthorizeCheckOperationFilter>();
            });

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = config.GetValue<string>("IdentityServer:AuthorityUrl");
                    // options.MapInboundClaims = false;

                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = false,
                        ValidTypes = new[] { "at+jwt" },
                        NameClaimType = "name",
                        RoleClaimType = "role"
                    };
                    //options.RequireHttpsMetadata = false;
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiCaller", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    //policy.RequireClaim("scope", PlaygroundAppConstants.BlogApiScopeReadName);
                });

                //options.AddPolicy("RequireInteractiveUser", policy =>
                //{
                //    policy.RequireClaim("sub");
                //});
            });

            return services;
        }

        public static IApplicationBuilder UseCoreApplication(this WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(CorsName);
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            // app.UseCloudEvents();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapSubscribeHandler();
                endpoints.MapControllers()
                .RequireAuthorization("ApiCaller");
            });

            IApiVersionDescriptionProvider? provider = app.Services.GetService<IApiVersionDescriptionProvider>();
            return app.UseSwagger(provider, app.Configuration);
        }
    }
}
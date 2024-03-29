﻿using BN.CleanArchitecture.Infrastructure;
using BN.CleanArchitecture.Infrastructure.AutoMapper;
using BN.CleanArchitecture.Infrastructure.Swagger;
using BN.CleanArchitecture.Infrastructure.Validator;
using IdentityModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Playground.Application.Contracts.Constants;
using Serilog;
using ApplicationAnchor = Playground.Application.Anchor;

namespace Playground.Infrastructure.Extensions
{
    public static class PlaygroundInfrastructureExtensions
    {
        private const string CorsName = "api";

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
            services.AddCustomMediatR(new[] { typeof(ApplicationAnchor) });
            services.AddCustomValidators(new[] { typeof(ApplicationAnchor) });
            services.AddAutoMapperConfig(typeof(ApplicationAnchor));

            services.AddMvc()
                .AddRazorRuntimeCompilation();

            services.AddSwagger(apiType
            //, options =>
            //{
            //    // https://www.scottbrady91.com/identity-server/aspnet-core-swagger-ui-authorization-using-identityserver4
            //    // Swagger authentication
            //    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            //    {
            //        Type = SecuritySchemeType.ApiKey,
            //        Flows = new OpenApiOAuthFlows
            //        {
            //            AuthorizationCode = new OpenApiOAuthFlow
            //            {
            //                AuthorizationUrl = new Uri(config.GetValue<string>("SwaggerConfig:AuthorizationBaseUrl") + "/connect/authorize"),
            //                TokenUrl = new Uri(config.GetValue<string>("SwaggerConfig:AuthorizationBaseUrl") + "/connect/token"),
            //                Scopes = new Dictionary<string, string>
            //                {
            //                    { config.GetValue<string>("SwaggerConfig:OidcApiName"), config.GetValue<string>("SwaggerConfig:ApiName") }
            //                }
            //            },
                        
            //        },
            //    });
            //    options.OperationFilter<PlaygroundAuthorizeCheckOperationFilter>();
            //}
            );

            // TODO
            services.AddAuthorization(options =>
            {
                // Admin access Playground API policy
                //options.AddPolicy(PlaygroundConsts.PlaygroundAdministrationPolicy, policy =>
                //{
                //    //policy.RequireAuthenticatedUser();
                //    policy.RequireAssertion(context => context.User.HasClaim(c =>
                //               ((c.Type == JwtClaimTypes.Role && c.Value == PlaygroundConsts.AdminRole) ||
                //                (c.Type == $"client_{JwtClaimTypes.Role}" && c.Value == PlaygroundConsts.AdminRole))
                //                ));
                //   policy.RequireClaim(JwtClaimTypes.Scope, config.GetValue<string>("SwaggerConfig:OidcApiName"));
                //});

                //options.AddPolicy("RequireInteractiveUser", policy =>
                //{
                //    policy.RequireClaim("sub");
                //});
            });

            return services;
        }

        public static IApplicationBuilder UseCoreApplication(this WebApplication app, IWebHostEnvironment env)
        {
            app.UseSerilogRequestLogging();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseCors(CorsName);

            app.UseStaticFiles();

            app.UseRouting();

            var isUseIdentityServer = app.Configuration.GetValue<bool>("IdentityServer:Enabled");
            if (isUseIdentityServer)
            {
                app.UseIdentityServer();
            }
            else
            {
                app.UseAuthentication();
            }
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapSubscribeHandler();
                //endpoints.MapControllers();
                //.RequireAuthorization(PlaygroundConsts.PlaygroundAdministrationPolicy);

                endpoints.MapControllerRoute(
                    name: "default-with-slug",
                    pattern: "{controller=Home}/{action=Index}/{slug?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                //.RequireAuthorization();
                endpoints.MapRazorPages();

                if (isUseIdentityServer)
                {
                    endpoints.MapRazorPages()
                    .RequireAuthorization();
                }

            });

            IApiVersionDescriptionProvider? provider = app.Services.GetService<IApiVersionDescriptionProvider>();

            app.UseSwagger(provider, app.Configuration);
            return app;
        }
    }
}

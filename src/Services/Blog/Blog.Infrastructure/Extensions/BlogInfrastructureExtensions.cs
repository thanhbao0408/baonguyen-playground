using BN.CleanArchitecture.Infrastructure;
using BN.CleanArchitecture.Infrastructure.Swagger;
using BN.CleanArchitecture.Infrastructure.Validator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CoreAnchor = Blog.Core.Anchor;

namespace Blog.Infrastructure.Extensions
{
    public static class BlogInfrastructureExtensions
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

            services.AddControllers();

            services.AddSwagger(apiType);

            //services.AddAuthentication("Bearer")
            //    .AddJwtBearer("Bearer", options =>
            //    {
            //        options.Authority = "https://localhost:5001";
            //        //options.MapInboundClaims = false;

            //        options.TokenValidationParameters = new TokenValidationParameters()
            //        {
            //            ValidateAudience = false,
            //            ValidTypes = new[] { "at+jwt" },
            //            //NameClaimType = "name",
            //            //RoleClaimType = "role"
            //        };
            //    });

            services.AddAuthorization(options =>
            {
                //options.AddPolicy("ApiCaller", policy =>
                //{
                //    policy.RequireAuthenticatedUser();
                //    policy.RequireClaim("scope", "TheVowAPI");
                //});

                //options.AddPolicy("RequireInteractiveUser", policy =>
                //{
                //    policy.RequireClaim("sub");
                //});
            });

            //var builder = services.AddIdentityCore<ApplicationUser>(options => { });
            //builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            //builder.AddEntityFrameworkStores<ApplicationDbContext>();

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
                // endpoints.MapSubscribeHandler();
                endpoints.MapControllers()
                    .RequireAuthorization("ApiCaller");
            });

            IApiVersionDescriptionProvider? provider = app.Services.GetService<IApiVersionDescriptionProvider>();
            return app.UseSwagger(provider);
        }
    }
}

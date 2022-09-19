using BN.CleanArchitecture.Infrastructure.EfCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Playground.Infrastructure.Data;
using Playground.Infrastructure.Data.DbContext;

namespace Playground.Infrastructure.Identity
{
    public static class IdentityExtensions
    {
        private const string DefaultDbName = "identitydb";

        public static IServiceCollection AddIdentityServices(this IServiceCollection services,
     IConfiguration config)
        {
            // Add DbContext for Identity
            services.AddSQLDbContext<PlaygroundIdentityDbContext>(
                config.GetConnectionString(DefaultDbName),
                null,
                svc => svc.AddRepository(typeof(Repository<,>))
            );

            services
                //.AddIdentity<PlaygroundUser, IdentityRole>()
                //.AddDefaultIdentity<PlaygroundUser>()
                .AddIdentityCore<PlaygroundUser>()
                .AddRoles<IdentityRole>()
                .AddSignInManager()
                .AddDefaultUI()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<PlaygroundIdentityDbContext>();

            var isUseIdentityServer = config.GetValue<bool>("IdentityServer:Enabled");

            if (isUseIdentityServer)
            {
                services
                   .AddIdentityServer(options =>
                   {
                       options.Events.RaiseErrorEvents = true;
                       options.Events.RaiseInformationEvents = true;
                       options.Events.RaiseFailureEvents = true;
                       options.Events.RaiseSuccessEvents = true;

                       // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
                       options.EmitStaticAudienceClaim = true;
                   })
                   .AddInMemoryIdentityResources(Config.IdentityResources)
                   .AddInMemoryApiScopes(Config.ApiScopes)
                   .AddInMemoryClients(Config.Clients)
                   .AddAspNetIdentity<PlaygroundUser>();
            }
                
            return services;
        }
    }
}

using BN.CleanArchitecture.Infrastructure.EfCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Playground.Infrastructure.Data;
using Playground.Infrastructure.Data.DbContext;
using Duende.IdentityServer;

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

            services.AddDefaultIdentity<PlaygroundUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<PlaygroundIdentityDbContext>();

            var isUseIdentityServer = config.GetValue<Boolean>("IdentityServer:Enabled");

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

            services.AddAuthentication();
                // TODO Add an 3rd login options
                //.AddGoogle(options =>
                //{
                //    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                //    // register your IdentityServer with Google at https://console.developers.google.com
                //    // enable the Google+ API
                //    // set the redirect URI to https://localhost:5001/signin-google
                //    options.ClientId = "copy client ID from Google here";
                //    options.ClientSecret = "copy client secret from Google here";
                //});

            return services;
        }
    }
}

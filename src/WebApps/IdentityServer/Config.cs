using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Playground.AppContracts;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope(name: PlaygroundAppConstants.BlogAPIScopeName,
                    displayName: PlaygroundAppConstants.BlogAPIScopeDisplayName),
                // new ApiScope(name: "OtherAPIs", displayName: "Other APIs"), Define other API
                // ...
            };

        public static IEnumerable<Client> Clients(IConfiguration configuration) =>
            new Client[]
            {
                 // Swagger client
                new Client
                {
                    ClientId = "api_swagger",
                    ClientName = "Swagger UI for APIs",
                    //ClientSecrets = {new Secret("secret".Sha256())}, // change me!

                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = true,

                    RedirectUris = { 
                        configuration.GetValue<string>("SwaggerUrls:BlogApiIdentityRedirectUris"),
                    },
                    AllowedCorsOrigins = {
                        configuration.GetValue<string>("SwaggerUrls:BlogApiAllowedCorsOrigin"),
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        PlaygroundAppConstants.BlogAPIScopeName
                    }
                },

                //// interactive client using code flow + pkce
                //new Client
                //{
                //    ClientId = "interactive",
                //    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                //    AllowedGrantTypes = GrantTypes.Code,

                //    RedirectUris = { "https://localhost:44300/signin-oidc" },
                //    FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                //    PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

                //    AllowOfflineAccess = true,
                //    AllowedScopes = { "openid", "profile", "scope2" }
                //},
            };
    }
}
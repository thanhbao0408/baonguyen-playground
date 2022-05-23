using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using IdentityModel;
using Playground.AppContracts;
using System.Security.Claims;

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

        public static IEnumerable<ApiResource> ApiResources => new ApiResource[] {
             new ApiResource
                {
                    Name = PlaygroundAppConstants.BlogApiResourceName,
                    DisplayName = PlaygroundAppConstants.BlogApiResourceDisplayName,
                    Description = PlaygroundAppConstants.BlogApiResourceDescription,
                    Scopes = new List<string> {
                        PlaygroundAppConstants.BlogApiScopeReadName,
                        PlaygroundAppConstants.BlogApiScopeWriteName,
                    },
                    ApiSecrets = new List<Secret> {new Secret("P@ss123".Sha256())}, // change me!
                    UserClaims = new List<string> {"role"}
                }
        };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope(name: PlaygroundAppConstants.BlogApiScopeReadName,
                    displayName: PlaygroundAppConstants.BlogApiScopeReadDisplayName),
                new ApiScope(name: PlaygroundAppConstants.BlogApiScopeWriteName,
                    displayName: PlaygroundAppConstants.BlogApiScopeWriteDisplayName),
                // new ApiScope(name: "OtherAPIs", displayName: "Other APIs"), Define other API
                // ...
            };

        public static IEnumerable<TestUser> Users => new TestUser[]
        {  
            new TestUser
                {
                    SubjectId = "b5310d5d-074c-4858-bb05-544b8ae01f2d",
                    Username = "thanhbao0408",
                    Password = "P@ss123",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Email, "thanhbao0408@gmail.com"),
                        new Claim(JwtClaimTypes.Role, "admin")
                    }
                }
        };

        public static IEnumerable<Client> Clients(IConfiguration configuration) =>
            new Client[]
            {
                 // Swagger client
                new Client
                {
                    ClientId = "api_swagger",
                    ClientName = "Swagger UI for APIs",

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
                        PlaygroundAppConstants.BlogApiScopeReadName,
                        PlaygroundAppConstants.BlogApiScopeWriteName
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
using Microsoft.IdentityModel.Tokens;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json");
});

var authenticationProviderKey = "IdentityApiKey";
builder.Services.AddAuthentication()
            .AddJwtBearer(authenticationProviderKey, options =>
            {
                options.Authority = builder.Configuration.GetValue<string>("IdentityServer:AuthorityUrl");
                //options.RequireHttpsMetadata = builder.Configuration.GetValue<bool>("IdentityServer:RequireHttps");
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false
                };
            });

builder.Services.AddOcelot()
    .AddCacheManager(config =>
    {
        config.WithDictionaryHandle();
    });

//builder.Host.UseSerilog(SeriLogger.Configure);

var app = builder.Build();

app.MapGet("/", async context =>
{
    await context.Response.WriteAsync("Hello World!");
});

app.UseOcelot();

app.Run();

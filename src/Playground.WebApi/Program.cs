using Playground.Infrastructure.EfCore;
using Playground.Infrastructure.EfCore.DbContext;
using Playground.Infrastructure.Extensions;
using Serilog;
using Skoruba.Duende.IdentityServer.Shared.Configuration.Helpers;
//using Playground.Infrastructure.;
using ApiAnchor = Playground.WebApi.V1.Anchor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseSerilog((ctx, lc) =>
    lc
        .MinimumLevel.Debug()
        .WriteTo.Console()
        .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day));

DockerHelpers.ApplyDockerConfiguration(builder.Configuration);

// Add Core services
builder.Services
    .AddEfCoreServices(builder.Configuration)
    .AddCoreServices(builder.Configuration, builder.Environment, typeof(ApiAnchor));

var app = builder.Build();

app.UseCoreApplication(builder.Environment);

// Seed Database
using (IServiceScope scope = app.Services.CreateScope())
{
    IServiceProvider services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<PlaygroundDbContext>();
        SeedDataInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        ILogger<Program> logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

app.Run();
using Playground.Infrastructure;
using Playground.Infrastructure.Data;
using Playground.Infrastructure.Extensions;
using Playground.Infrastructure.Identity;
using Serilog;
using ApiAnchor = Playground.WebAdmin.Controllers.Api.V1.Anchor;

var builder = WebApplication.CreateBuilder(args);


builder.Host.UseSerilog((ctx, lc) =>
    lc
        .MinimumLevel.Debug()
        .WriteTo.Console()
        .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day));

// Add Core services
builder.Services
    .AddEfCoreServices(builder.Configuration)
    .AddIdentityServices(builder.Configuration)
    .AddCoreServices(builder.Configuration, builder.Environment, typeof(ApiAnchor))
    .AddCustomAuthentication(builder.Configuration);

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseCoreApplication(builder.Environment);

// Seed Database
using (IServiceScope scope = app.Services.CreateScope())
{
    IServiceProvider services = scope.ServiceProvider;

    try
    {
        SeedDataInitializer.Initialize(services);
    }
    catch (Exception ex)
    {
        ILogger<Program> logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

app.Run();
 
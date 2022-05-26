using BN.CleanArchitecture.Infrastructure.Identity;
using IdentityServer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Microsoft.Extensions.DependencyInjection;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace IdentityServer
{
    internal static class HostingExtensions
    {
        private const string DbName = "postgres";
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddRazorPages();

            builder.Services.AddDbContext<IdentityServerDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString(DbName), sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(IdentityServerDbContext).Assembly.GetName().Name);
                    sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                }).UseSnakeCaseNamingConvention();
            });

            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
                    .AddEntityFrameworkStores<IdentityServerDbContext>()
                .AddDefaultTokenProviders();

            builder.Services
                .AddIdentityServer(options =>
                {
                    options.KeyManagement.Enabled = true;
                    options.IssuerUri = builder.Configuration.GetValue<string>("IssuerUri");
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = optionsBuilder =>
                    {
                        optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString(DbName), sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(typeof(IdentityServerDbContext).Assembly.GetName().Name);
                            sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                        }).UseSnakeCaseNamingConvention();
                    };
                })
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = optionsBuilder =>
                    {
                        optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString(DbName), sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(typeof(IdentityServerDbContext).Assembly.GetName().Name);
                            sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                        }).UseSnakeCaseNamingConvention();
                    };
                })
                .AddAspNetIdentity<ApplicationUser>();

            builder.Services.AddScoped<ApplicationUserManager>();
            builder.Services.AddScoped<ApplicationSignInManager>();

            return builder.Build();
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            app.UseSerilogRequestLogging();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.MapRazorPages()
                .RequireAuthorization();

            InitializeDbTestData(app);

            return app;
        }


        /// <summary>
        /// A small bootstrapping method that will run EF migrations against the database
        /// and create your test data.
        /// </summary>
        private static void InitializeDbTestData(WebApplication app)
        {
            using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
                serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>().Database.Migrate();
                serviceScope.ServiceProvider.GetRequiredService<IdentityServerDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

                if (!context.Clients.Any())
                {
                    foreach (var client in Config.Clients(app.Configuration))
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in Config.IdentityResources)
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiScopes.Any())
                {
                    foreach (var scope in Config.ApiScopes)
                    {
                        context.ApiScopes.Add(scope.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in Config.ApiResources)
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                var userManager = serviceScope.ServiceProvider.GetRequiredService<ApplicationUserManager>();
                if (!userManager.Users.Any())
                {
                    foreach (var testUser in Config.Users)
                    {
                        var identityUser = new ApplicationUser
                        {
                            Id = Guid.Parse(testUser.SubjectId),
                            UserName = testUser.Username
                        };

                        userManager.CreateAsync(identityUser, "Password123!").Wait();
                        userManager.AddClaimsAsync(identityUser, testUser.Claims.ToList()).Wait();
                    }
                }
            }
        }
    }
}
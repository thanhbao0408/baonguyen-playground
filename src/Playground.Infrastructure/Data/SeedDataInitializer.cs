
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Playground.Infrastructure.Data.DbContext;
using Playground.Infrastructure.Identity;
using Serilog;
using System.Security.Claims;

namespace Playground.Infrastructure.Data;

public static class SeedDataInitializer
{
    public static void Initialize(IServiceProvider services)
    {
        SeedIdentityData(services);
        SeedPlaygroundData(services);
    }

    private static void SeedIdentityData(IServiceProvider services)
    {
        var identityContext = services.GetRequiredService<PlaygroundIdentityDbContext>();

        identityContext.Database.Migrate();

        // TODO: Seed Data
        var userMgr = services.GetRequiredService<UserManager<PlaygroundUser>>();
        var alice = userMgr.FindByNameAsync("alice").Result;
        if (alice == null)
        {
            alice = new PlaygroundUser
            {
                UserName = "alice",
                Email = "AliceSmith@email.com",
                EmailConfirmed = true,
            };
            var result = userMgr.CreateAsync(alice, "Pass123$").Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            result = userMgr.AddClaimsAsync(alice, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Alice Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Alice"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                        }).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            Log.Debug("alice created");
        }
        else
        {
            Log.Debug("alice already exists");
        }

        var bob = userMgr.FindByNameAsync("bob").Result;
        if (bob == null)
        {
            bob = new PlaygroundUser
            {
                UserName = "bob",
                Email = "BobSmith@email.com",
                EmailConfirmed = true
            };
            var result = userMgr.CreateAsync(bob, "Pass123$").Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            result = userMgr.AddClaimsAsync(bob, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Bob Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Bob"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                            new Claim("location", "somewhere")
                        }).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            Log.Debug("bob created");
        }
        else
        {
            Log.Debug("bob already exists");
        }

        identityContext.SaveChanges();
    }


    private static void SeedPlaygroundData(IServiceProvider services)
    {
        var playgroundContext = services.GetRequiredService<PlaygroundDbContext>();

        playgroundContext.Database.Migrate();

        // TODO: Seed Data

        playgroundContext.SaveChanges();
    }
}
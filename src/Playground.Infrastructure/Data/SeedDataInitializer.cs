
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Playground.Application.Contracts.Dtos.Blog.Articles;
using Playground.Core.Entities.Blog.Articles;
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
        var testAccount = userMgr.FindByNameAsync("testAccount").Result;
        if (testAccount == null)
        {
            testAccount = new PlaygroundUser
            {
                UserName = "testAccount@email.com",
                Email = "testAccount@email.com",
                EmailConfirmed = true,
            };
            var result = userMgr.CreateAsync(testAccount, "Password123$").Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            result = userMgr.AddClaimsAsync(testAccount, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Test Test"),
                            new Claim(JwtClaimTypes.GivenName, "Test"),
                            new Claim(JwtClaimTypes.FamilyName, "Test"),
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


        identityContext.SaveChanges();
    }


    private static void SeedPlaygroundData(IServiceProvider services)
    {
        var playgroundContext = services.GetRequiredService<PlaygroundDbContext>();

        playgroundContext.Database.Migrate();

        // TODO: Seed Data
        var articles = new List<Article>
        {
            new Article(Guid.NewGuid())
            {
                State = ArticleState.Draft,
                Title = "Article 1",
                Description = "Description 1",
                Content = "Content 1",
                Slug = "article-1",
            },
            new Article(Guid.NewGuid())
            {
                State = ArticleState.Draft,
                Title = "Article 2",
                Description = "Description 2",
                Content = "Content 2",
                Slug = "article-2",
            },
            new Article(Guid.NewGuid())
            {
                State = ArticleState.Draft,
                Title = "Article 3",
                Description = "Description 3",
                Content = "Content 3",
                Slug = "article-33",
            },
            new Article(Guid.NewGuid())
            {
                State = ArticleState.Draft,
                Title = "Article 4",
                Description = "Description 4",
                Content = "Content 4",
                Slug = "article-4",
            },
            new Article(Guid.NewGuid())
            {
                State = ArticleState.Draft,
                Title = "Article 5",
                Description = "Description 5",
                Content = "Content 5",
                Slug = "article-5",
            },
            new Article(Guid.NewGuid())
            {
                State = ArticleState.Draft,
                Title = "Article 6",
                Description = "Description 6",
                Content = "Content 6",
                Slug = "article-6",
            },
        };

        playgroundContext.AddRange(articles);

        playgroundContext.SaveChanges();
    }
}
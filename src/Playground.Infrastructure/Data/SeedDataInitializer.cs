
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Playground.Application.Contracts.Dtos.Blog.Articles;
using Playground.Core.Entities.Blog.Articles;
using Playground.Core.Entities.Taggings;
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

        if (identityContext.Users.Any())
        {
            return;
        }

        return;

        //// TODO: Seed Data
        //var userMgr = services.GetRequiredService<UserManager<PlaygroundUser>>();
        //var testAccount = userMgr.FindByNameAsync("testAccount").Result;
        //if (testAccount == null)
        //{
        //    testAccount = new PlaygroundUser
        //    {
        //        UserName = "testAccount@email.com",
        //        Email = "testAccount@email.com",
        //        EmailConfirmed = true,
        //    };
        //    var result = userMgr.CreateAsync(testAccount, "Password123$").Result;
        //    if (!result.Succeeded)
        //    {
        //        throw new Exception(result.Errors.First().Description);
        //    }

        //    result = userMgr.AddClaimsAsync(testAccount, new Claim[]{
        //                    new Claim(JwtClaimTypes.Name, "Test Test"),
        //                    new Claim(JwtClaimTypes.GivenName, "Test"),
        //                    new Claim(JwtClaimTypes.FamilyName, "Test"),
        //                    new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
        //                }).Result;
        //    if (!result.Succeeded)
        //    {
        //        throw new Exception(result.Errors.First().Description);
        //    }
        //    Log.Debug("alice created");
        //}
        //else
        //{
        //    Log.Debug("alice already exists");
        //}


        //identityContext.SaveChanges();
    }


    private static void SeedPlaygroundData(IServiceProvider services)
    {
        var playgroundContext = services.GetRequiredService<PlaygroundDbContext>();

        playgroundContext.Database.Migrate();

        if (playgroundContext.Articles.Any())
        {
            return;
        }

        var tagColors = new List<TagColor>
        {
            new TagColor(Guid.NewGuid())
            {
                Name = "green",
                TextColor = "#402c1b",
                BgColor = "#fdecc8",
                BorderColor = "#402c1b",
            },
            new TagColor(Guid.NewGuid())
            {
                Name = "yellow",
                TextColor = "#1c3829",
                BgColor = "#dbeddb",
                BorderColor = "#1c3829",
            },
            new TagColor(Guid.NewGuid())
            {
                Name = "light gray",
                TextColor = "#32302c",
                BgColor = "#e3e2e080",
                BorderColor = "#32302c",
            },
            new TagColor(Guid.NewGuid())
            {
                Name = "blue",
                TextColor = "#183347",
                BgColor = "#d3e5ef",
                BorderColor = "#183347",
            },
            new TagColor(Guid.NewGuid())
            {
                Name = "pink",
                TextColor = "#4c2337",
                BgColor = "#f5e0e9",
                BorderColor = "#4c2337",
            },
            new TagColor(Guid.NewGuid())
            {
                Name = "gray",
                TextColor = "#32302c",
                BgColor = "#e3e2e0",
                BorderColor = "#32302c",
            },
            new TagColor(Guid.NewGuid())
            {
                Name = "brown",
                TextColor = "#442a1e",
                BgColor = "#eee0da",
                BorderColor = "#442a1e",
            },
            new TagColor(Guid.NewGuid())
            {
                Name = "orange",
                TextColor = "#49290e",
                BgColor = "#fadec9",
                BorderColor = "#49290e",
            },
            new TagColor(Guid.NewGuid())
            {
                Name = "red",
                TextColor = "#5d1715",
                BgColor = "#ffe2dd",
                BorderColor = "#5d1715",
            },
            new TagColor(Guid.NewGuid())
            {
                Name = "purple",
                TextColor = "#412454",
                BgColor = "#e8deee",
                BorderColor = "#412454",
            },
        };

        playgroundContext.AddRange(tagColors);

        var tags = new List<Tag>
        {
            new Tag(Guid.NewGuid())
            {
                Name = "ASP.NET",
                Color = tagColors[9],
            }
        };

        playgroundContext.AddRange(tags);

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
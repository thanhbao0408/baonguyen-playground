using BN.CleanArchitecture.Core.Domain.Cqrs;
using FluentValidation;
using Playground.Application.Contracts.Dtos.Blog.Articles;

namespace Playground.Application.Methods.Queries
{
    public class CheckSlugAvailableQuery : IItemQuery<string, bool>
    {
        public CheckSlugAvailableQuery(string slug, Guid? articleId)
        {
            Key = slug;
            ArticleId = articleId;
        }

        public string Key { get; init; }
        public Guid? ArticleId { get; set; }
        public List<string> Includes { get; init; } = new();
    }
}

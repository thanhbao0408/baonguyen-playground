using BN.CleanArchitecture.Core.Domain.Cqrs;
using FluentValidation;
using Playground.Application.Contracts.Dtos.Blog.Articles;

namespace Playground.Application.Methods.Queries
{
    public class GetArticleDetailBySlugQuery : IItemQuery<string, ArticleDetailDto>
    {
        public GetArticleDetailBySlugQuery(string slug)
        {
            Key = slug;
        }

        public List<string> Includes { get; init; } = new();
        public string Key { get; init; }

        internal class Validator : AbstractValidator<GetArticleDetailBySlugQuery>
        {
            public Validator()
            {
                RuleFor(x => x.Key)
                    .NotNull()
                    .NotEmpty().WithMessage("Id is required.");
            }
        }
    }
}

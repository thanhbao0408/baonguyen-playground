using BN.CleanArchitecture.Core.Domain.Cqrs;
using BN.CleanArchitecture.Core.Repository;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Playground.Application.Contracts.Dtos.Blog.Articles;
using Playground.Core.Entities.Blog.Articles;

namespace Playground.Application.Methods.Commands.Articles.CreateArticle
{
    public record CreateArticleCommand : ICreateCommand<ArticleDetailDto, ArticleDetailDto>
    {
        public ArticleDetailDto Model { get; init; }
        internal class Validator: AbstractValidator<CreateArticleCommand>
        {
            public Validator(IRepository<Article, Guid> _articleRepo)
            {
                RuleFor(v => v.Model.Title)
                    .NotEmpty().WithMessage("Title is required.")
                    .MaximumLength(500).WithMessage("Title must not exceed 500 characters.");

                RuleFor(v => v.Model.Slug)
                    .NotEmpty().WithMessage("Slug is required")
                    .MaximumLength(100).WithMessage("Slug must not exceed 100 characters")
                    .MustAsync(async (slug, cancellation) =>
                    {
                        var isDuplicated = await _articleRepo.AnyAsync(article => EF.Functions.Like(article.Slug, slug));
                        return !isDuplicated;
                    }).WithMessage("Slug must be unique");

                RuleFor(v => v.Model.Description)
                    .NotEmpty().WithMessage("Description is required")
                    .MaximumLength(1000).WithMessage("Description must not exceed 500 characters");
            }
        }
    }
}

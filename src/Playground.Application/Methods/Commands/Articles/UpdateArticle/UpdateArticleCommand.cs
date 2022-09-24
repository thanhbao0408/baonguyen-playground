using BN.CleanArchitecture.Core.Domain.Cqrs;
using BN.CleanArchitecture.Core.Repository;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Playground.Application.Contracts.Dtos.Blog.Articles;
using Playground.Core.Entities.Blog.Articles;
using System.Text.RegularExpressions;

namespace Playground.Application.Methods.Commands.Articles.CreateArticle
{
    public record UpdateArticleCommand : ICreateCommand<ArticleDetailDto, ArticleDetailDto>
    {
        public ArticleDetailDto Model { get; init; }
        public class Validator : AbstractValidator<UpdateArticleCommand>
        {
            public Validator(IRepository<Article, Guid> _articleRepo)
            {
                RuleFor(v => v.Model.Title)
                    .NotEmpty().WithMessage("Title is required.")
                    .MaximumLength(500).WithMessage("Title must not exceed 500 characters.");

                RuleFor(v => v.Model.Slug)
                    .NotEmpty().WithMessage("Slug is required")
                    .MaximumLength(100).WithMessage("Slug must not exceed 100 characters")
                    .Must(slug =>
                    {
                        return Regex.IsMatch(slug, @"^[a-zA-Z0-9-]+$");
                    })
                    .MustAsync(async (slug, cancellation) =>
                    {
                        var isDuplicated = await _articleRepo.AnyAsync(article => EF.Functions.Like(article.Slug, slug));
                        return !isDuplicated;
                    }).WithMessage("Slug must be unique");

                RuleFor(v => v.Model.Description)
                    .NotEmpty().WithMessage("Description is required")
                    .MaximumLength(1000).WithMessage("Description must not exceed 500 characters");
                ArticleRepo = _articleRepo;
            }

            public IRepository<Article, Guid> ArticleRepo { get; }
        }
    }
}

using Blog.Contracts.Dtos.Articles;
using Blog.Core.Entities.Articles;
using BN.CleanArchitecture.Core.Domain.Cqrs;
using BN.CleanArchitecture.Core.Repository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Methods.Queries
{
    public class GetArticlesQuery : IListQuery<ListResultModel<ArticleDto>>
    {
        public List<string> Includes { get; init; } = new();
        public List<FilterModel> Filters { get; init; } = new();
        public List<string> Sorts { get; init; } = new();
        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 20;

        internal class Validator : AbstractValidator<GetArticlesQuery>
        {
            public Validator()
            {
                RuleFor(x => x.Page)
                    .GreaterThanOrEqualTo(1).WithMessage("Page should at least greater than or equal to 1.");

                RuleFor(x => x.PageSize)
                    .GreaterThanOrEqualTo(1).WithMessage("PageSize should at least greater than or equal to 1.");
            }
        }

    }
}

using BN.CleanArchitecture.Core.Domain.Cqrs;
using FluentValidation;
using Playground.Application.Contracts.Dtos.Blog.Taggings;

namespace Playground.Application.Methods.Queries
{
    public class GetTagsQuery : IListQuery<ListResultModel<TagDto>>
    {
        public List<string> Includes { get; init; } = new();
        public List<FilterModel> Filters { get; init; } = new();
        public List<string> Sorts { get; init; } = new();
        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 20;
        public bool IsPagingEnabled { get; init; } = true;

        internal class Validator : AbstractValidator<GetTagsQuery>
        {
            public Validator()
            {
                RuleFor(x => x.Page)
                    .GreaterThanOrEqualTo(0).WithMessage("Page should at least greater than or equal to 0.");

                RuleFor(x => x.PageSize)
                    .GreaterThanOrEqualTo(1).WithMessage("PageSize should at least greater than or equal to 1.");
            }
        }

    }
}

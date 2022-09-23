
using BN.CleanArchitecture.Core.Domain.Cqrs;
using BN.CleanArchitecture.Core.Specification;
using Playground.Core.Entities.Taggings;

namespace Playground.Application.Specs.Taggings
{
    public class TagListQuerySpec<TResponse> : GridSpecificationBase<Tag>
    {
        public TagListQuerySpec(IListQuery<ListResultModel<TResponse>> gridQueryInput)
        {
            ApplyIncludeList(gridQueryInput.Includes);

            ApplyFilterList(gridQueryInput.Filters);

            ApplySortingList(gridQueryInput.Sorts);

            ApplyPaging(gridQueryInput.Page, gridQueryInput.PageSize, gridQueryInput.IsPagingEnabled);
        }
    }
}

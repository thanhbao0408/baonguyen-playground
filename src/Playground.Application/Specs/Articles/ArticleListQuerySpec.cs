
using BN.CleanArchitecture.Core.Domain.Cqrs;
using BN.CleanArchitecture.Core.Specification;
using Playground.Core.Entities.Blog.Articles;

namespace Playground.Application.Specs.Articles
{
    public class ArticleListQuerySpec<TResponse> : GridSpecificationBase<Article>
    {
        public ArticleListQuerySpec(IListQuery<ListResultModel<TResponse>> gridQueryInput)
        {
            ApplyIncludeList(gridQueryInput.Includes);

            ApplyFilterList(gridQueryInput.Filters);

            ApplySortingList(gridQueryInput.Sorts);

            ApplyPaging(gridQueryInput.Page, gridQueryInput.PageSize, gridQueryInput.IsPagingEnabled);
        }
    }
}

using BN.CleanArchitecture.Core.Domain.Cqrs;
using BN.CleanArchitecture.Core.Specification;
using Playground.Core.Entities.Blog.Articles;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Playground.Application.Specs.Articles
{
    public class ArticleBySlugQuerySpec<TResponse> : SpecificationBase<Article>
    {
        private string _slug; 
        public ArticleBySlugQuerySpec([NotNull] IItemQuery<string, TResponse> queryInput)
        {
            ApplyIncludeList(queryInput.Includes);

            _slug = queryInput.Key.ToLower(); ;
        }

        public override Expression<Func<Article, bool>> Criteria => p => p.Slug == _slug;
    }
}

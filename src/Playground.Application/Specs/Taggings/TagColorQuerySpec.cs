
using BN.CleanArchitecture.Core.Domain.Cqrs;
using BN.CleanArchitecture.Core.Specification;
using Playground.Core.Entities.Taggings;
using System.Linq.Expressions;

namespace Playground.Application.Specs.Taggings
{
    public class TagColorQuerySpec<TResponse> : SpecificationBase<TagColor>
    {
        public override Expression<Func<TagColor, bool>> Criteria { get; }

    }
}

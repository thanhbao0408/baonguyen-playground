using BN.CleanArchitecture.Core.Domain.Cqrs;
using FluentValidation;
using Playground.Application.Contracts.Dtos.Blog.Taggings;

namespace Playground.Application.Methods.Queries
{
    public class GetTagColorsQuery : IQuery<List<TagColorDto>>
    {

    }
}

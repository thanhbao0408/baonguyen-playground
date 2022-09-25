using BN.CleanArchitecture.Core.Domain.Cqrs;
using BN.CleanArchitecture.Core.Repository;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Playground.Application.Contracts.Dtos.Blog.Taggings;
using Playground.Core.Entities.Taggings;
using System.Text.RegularExpressions;

namespace Playground.Application.Methods.Commands.Tags.CreateTag
{
    public record CreateTagCommand : ICreateCommand<TagDto, TagDto>
    {
        public TagDto Model { get; init; }
        internal class Validator: AbstractValidator<CreateTagCommand>
        {
            public Validator(IRepository<Tag, Guid> _tagRepo)
            {
                RuleFor(v => v.Model.Name)
                    .NotEmpty().WithMessage("Name is required.")
                    .MaximumLength(200).WithMessage("Name must not exceed 500 characters.")
                    .MustAsync(async (name, cancellation) =>
                    {
                        var isDuplicated = await _tagRepo.AnyAsync(tag => EF.Functions.Like(tag.Name, name));
                        return !isDuplicated;
                    });
            }
        }
    }
}

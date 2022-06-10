using BN.CleanArchitecture.Contracts.Dtos;

namespace Playground.Application.Contracts.Dtos.Blog.Tags
{
    public class TagDto: EntityDto<Guid>
    {
        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;

        public Guid TagColorId { get; set; } = default!;
        public TagColorDto TagColor { get; set; } = default!;
    }
}

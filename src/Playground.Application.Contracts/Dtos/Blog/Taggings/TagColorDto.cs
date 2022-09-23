using BN.CleanArchitecture.Contracts.Dtos;

namespace Playground.Application.Contracts.Dtos.Blog.Taggings
{
    public class TagColorDto: EntityDto<Guid>
    {
        public string Name { get; set; } = default!;

        public string BgColor { get; set; } = default!;

        public string BorderColor { get; set; } = default!;

        public string TextColor { get; set; } = default!;
    }
}

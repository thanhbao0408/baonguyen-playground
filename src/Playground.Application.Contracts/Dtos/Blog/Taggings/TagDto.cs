using BN.CleanArchitecture.Contracts.Dtos;

namespace Playground.Application.Contracts.Dtos.Blog.Taggings
{
    public class TagDto: EntityDto<Guid>
    {
        public string Name { get; set; } = default!;
        public Guid ColorId { get; set; }
        public string TagBgColor { get; set; }
        public string TagTextColor { get; set; }
        public string TagBorderColor { get; set; }

    }
}

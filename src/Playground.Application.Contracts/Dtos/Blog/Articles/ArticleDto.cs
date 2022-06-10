using BN.CleanArchitecture.Contracts.Dtos;

namespace Playground.Application.Contracts.Dtos.Blog.Articles
{
    public class ArticleDto: AuditedEntityDto<Guid>
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Content { get; set; } = default!;
        public string CoverImage { get; set; } = default!;
        public string Slug { get; set; } = default!;
    }
}

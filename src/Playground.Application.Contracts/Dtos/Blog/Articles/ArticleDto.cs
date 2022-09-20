using BN.CleanArchitecture.Contracts.Dtos;

namespace Playground.Application.Contracts.Dtos.Blog.Articles
{
    public class ArticleDto : AuditedEntityDto<Guid>
    {
        public ArticleState State { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Slug { get; set; } = default!;
        public DateTime? PublishDate { get; set; }

        public List<string> ArticleTags { get; set; }
    }
}

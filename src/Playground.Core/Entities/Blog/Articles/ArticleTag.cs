using BN.CleanArchitecture.Core.Domain.Entities;
using Playground.Core.Entities.Taggings;

namespace Playground.Core.Entities.Blog.Articles
{
    public class ArticleTag : AuditedEntity
    {
        public ArticleTag(Guid articleId, Guid tagId)
        {
            ArticleId = articleId;
            TagId = tagId;
        }

        public Guid ArticleId { get; set; }

        public Guid TagId { get; set; }

        public BlogTag Tag { get; set; }

    }
}

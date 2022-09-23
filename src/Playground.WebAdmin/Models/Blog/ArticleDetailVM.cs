using Playground.Application.Contracts.Dtos.Blog.Articles;
using Playground.Application.Contracts.Dtos.Blog.Taggings;

namespace Playground.WebAdmin.Models.Blog
{
    public class ArticleDetailVM
    {
        public ArticleDetailDto ArticleDetail { get; set; }
        public List<Guid> ArticleTagIds { get; set; }
        public List<TagDto> Tags { get; set; }
    }
}

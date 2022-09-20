
using BN.CleanArchitecture.Contracts.Dtos;

namespace Playground.Application.Contracts.Dtos.Blog.Articles
{
    public class ArticleDetailDto: EntityDto<Guid>
    {
        public string Title { get; set; }
    }
}

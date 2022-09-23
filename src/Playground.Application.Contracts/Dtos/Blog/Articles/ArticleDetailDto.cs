using BN.CleanArchitecture.Contracts.Dtos;
using Playground.Application.Contracts.Dtos.Blog.Taggings;
using System.Collections.ObjectModel;

namespace Playground.Application.Contracts.Dtos.Blog.Articles
{
    public class ArticleDetailDto : EntityDto<Guid>
    {
        public ArticleDetailDto()
        {

        }
        public ArticleDetailDto(Guid id) : base(id)
        {

        }

        public ArticleState State { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string? Content { get; set; }

        public string? CoverImage { get; set; }

        public string Slug { get; set; }

        public Collection<TagDto> ArticleTags { get; set; } = new Collection<TagDto>();

        public DateTime? PublishDate { get; set; }
    }
}

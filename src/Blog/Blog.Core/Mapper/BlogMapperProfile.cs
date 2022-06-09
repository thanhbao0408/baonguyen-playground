using AutoMapper;
using Blog.Contracts.Dtos.Articles;
using Blog.Core.Entities.Articles;

namespace Blog.Contracts.Mapper
{
    public class BlogMapperProfile : Profile
    {
        public BlogMapperProfile()
        {
            CreateMap<Article, ArticleDto>().ReverseMap();
        }
    }
}

using AutoMapper;
using Playground.Application.Contracts.Dtos.Blog.Articles;
using Playground.Core.Entities.Blog.Articles;

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

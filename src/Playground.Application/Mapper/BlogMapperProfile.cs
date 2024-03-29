﻿using AutoMapper;
using Playground.Application.Contracts.Dtos.Blog.Articles;
using Playground.Application.Contracts.Dtos.Blog.Taggings;
using Playground.Core.Entities.Blog.Articles;
using Playground.Core.Entities.Taggings;

namespace Blog.Contracts.Mapper
{
    public class BlogMapperProfile : Profile
    {
        public BlogMapperProfile()
        {
            CreateMap<Article, ArticleDto>()
                .ForMember(dest => dest.ArticleTags, opt => opt.MapFrom(src => src.ArticleTags.Select(t => t.Tag.Name)))
                .ReverseMap();

            CreateMap<Article, ArticleDetailDto>()
                .ForMember(des => des.ArticleTags, opt => opt.MapFrom(src => src.ArticleTags.Select(p => p.Tag)))
                .ReverseMap();

            CreateMap<Tag, TagDto>()
                .ForMember(des => des.TagTextColor, opt => opt.MapFrom(src => src.Color.TextColor))
                .ForMember(des => des.TagBgColor, opt => opt.MapFrom(src => src.Color.BgColor))
                .ForMember(des => des.TagBorderColor, opt => opt.MapFrom(src => src.Color.BorderColor))
                .ReverseMap()
                .ForMember(des => des.Color, opt => opt.Ignore());

            CreateMap<TagColor, TagColorDto>().ReverseMap();

        }
    }
}

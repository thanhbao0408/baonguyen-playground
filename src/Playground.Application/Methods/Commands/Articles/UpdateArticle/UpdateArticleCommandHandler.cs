using AutoMapper;
using BN.CleanArchitecture.Core.Domain.Cqrs;
using BN.CleanArchitecture.Core.Repository;
using BN.Common;
using MediatR;
using Microsoft.VisualBasic;
using Playground.Application.Contracts.Dtos.Blog.Articles;
using Playground.Core.Entities.Blog.Articles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Methods.Commands.Articles.CreateArticle
{
    public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand, ResultModel<ArticleDetailDto>>
    {
        private readonly IRepository<Article, Guid> _articleRepository;
        private readonly IMapper _mapper;

        public UpdateArticleCommandHandler(IRepository<Article, Guid> articleRepository, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }
        
        public async Task<ResultModel<ArticleDetailDto>> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
        {
            var article = _articleRepository.FindById(request.Model.Id);

            article.Title = request.Model.Title;
            article.Slug = request.Model.Slug;
            article.Description = request.Model.Description;
            article.Content = request.Model.Content;
            article.State = request.Model.State;
            article.CoverImage = request.Model.CoverImage;
            article.PublishDate = request.Model.PublishDate;

            // Check update article tags
            var updatedArticleIds =request.Model.ArticleTags.Select(t => t.Id).ToList();
            var articleIds = article.ArticleTags.Select(p => p.TagId).ToList();

            if (!updatedArticleIds.IsListEqual(articleIds))
            {
                foreach(var tagId in updatedArticleIds)
                {
                    article.ArticleTags = new Collection<ArticleTag>()
                    {
                        new ArticleTag(article.Id, tagId)
                    };
                }
            }

            await _articleRepository.UpdateAsync(article);

            var _articleDetailDto = _mapper.Map<ArticleDetailDto>(article);

            return ResultModel<ArticleDetailDto>.Create(_articleDetailDto);
        }
    }
}

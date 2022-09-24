using AutoMapper;
using BN.CleanArchitecture.Core.Domain.Cqrs;
using BN.CleanArchitecture.Core.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Playground.Application.Contracts.Dtos.Blog.Articles;
using Playground.Application.Methods.Queries;
using Playground.Application.Specs.Articles;
using Playground.Core.Entities.Blog.Articles;

namespace Playground.Application.Methods.Queries
{
    public class CheckSlugAvailableQueryHandler : IRequestHandler<CheckSlugAvailableQuery, ResultModel<bool>>
    {
        private readonly IRepository<Article, Guid> _articleRepository;
        private readonly IMapper _mapper;

        public CheckSlugAvailableQueryHandler(IRepository<Article, Guid> articleRepository, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }

        public async Task<ResultModel<bool>> Handle(CheckSlugAvailableQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var alreadyHaveSlug = await _articleRepository.AnyAsync(p => EF.Functions.Like(request.Key, p.Slug));

            if (alreadyHaveSlug)
            {
                var spec = new ArticleBySlugQuerySpec<bool>(request);
                var article = await _articleRepository.FindOneAsync(spec);
                if (request.ArticleId.HasValue && article?.Id == request.ArticleId)
                {
                    alreadyHaveSlug = false;
                }
            }

            return ResultModel<bool>.Create(!alreadyHaveSlug);
        }
    }
}

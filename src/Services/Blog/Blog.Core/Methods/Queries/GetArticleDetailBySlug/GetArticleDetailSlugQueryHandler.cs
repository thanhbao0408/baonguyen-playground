using AutoMapper;
using Blog.Contracts.Dtos.Articles;
using Blog.Core.Entities.Articles;
using Blog.Core.Specs.Articles;
using BN.CleanArchitecture.Core.Domain.Cqrs;
using BN.CleanArchitecture.Core.Repository;
using MediatR;

namespace Blog.Core.Methods.Queries
{
    public class GetArticleDetailSlugQueryHandler : IRequestHandler<GetArticleDetailBySlugQuery, ResultModel<ArticleDetailDto>>
    {
        private readonly IRepository<Article, Guid> _articleRepository;
        private readonly IMapper _mapper;

        public GetArticleDetailSlugQueryHandler(IRepository<Article, Guid> articleRepository, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }

        public async Task<ResultModel<ArticleDetailDto>> Handle(GetArticleDetailBySlugQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var spec = new ArticleBySlugQuerySpec<ArticleDetailDto>(request);

            var article = await _articleRepository.FindOneAsync(spec);
            var articleDetailDto = _mapper.Map<ArticleDetailDto>(article);
            return ResultModel<ArticleDetailDto>.Create(articleDetailDto);
        }
    }
}

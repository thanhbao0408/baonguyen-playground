using AutoMapper;
using Blog.Contracts.Dtos.Articles;
using Blog.Core.Entities.Articles;
using Blog.Core.Specs.Articles;
using BN.CleanArchitecture.Core.Domain.Cqrs;
using BN.CleanArchitecture.Core.Repository;
using MediatR;

namespace Blog.Core.Queries
{
    public class GetArticlesQueryHandler : IRequestHandler<GetArticlesQuery, ResultModel<ListResultModel<ArticleDto>>>
    {
        private readonly IGridRepository<Article, Guid> _articleRepository;
        private readonly IMapper _mapper;

        public GetArticlesQueryHandler(IGridRepository<Article, Guid> articleRepository, IMapper mapper)
        {
            _articleRepository =
                articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
            _mapper = mapper;
        }

        public async Task<ResultModel<ListResultModel<ArticleDto>>> Handle(GetArticlesQuery request,
            CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var spec = new ArticleListQuerySpec<ArticleDto>(request);

            var articles = await _articleRepository.FindAsync(spec);

            var articleDtos = _mapper.Map<List<ArticleDto>>(articles);

            var totalArticles = await _articleRepository.CountAsync(spec);

            var resultModel = ListResultModel<ArticleDto>.Create(articleDtos,totalArticles,request.Page,request.PageSize);

            return ResultModel<ListResultModel<ArticleDto>>.Create(resultModel);
        }
    }
}

using AutoMapper;
using BN.CleanArchitecture.Core.Domain.Cqrs;
using BN.CleanArchitecture.Core.Repository;
using MediatR;
using Playground.Application.Contracts.Dtos.Blog.Articles;
using Playground.Core.Entities.Blog.Articles;

namespace Playground.Application.Methods.Commands.Articles.CreateArticle
{
    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, ResultModel<ArticleDetailDto>>
    {
        private readonly IRepository<Article, Guid> _articleRepository;
        private readonly IMapper _mapper;

        public CreateArticleCommandHandler(IRepository<Article, Guid> articleRepository, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }
        
        public async Task<ResultModel<ArticleDetailDto>> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var article = _mapper.Map<Article>(request.Model);

            await _articleRepository.AddAsync(article);

            var _articleDetailDto = _mapper.Map<ArticleDetailDto>(article);

            return ResultModel<ArticleDetailDto>.Create(_articleDetailDto);
        }
    }
}

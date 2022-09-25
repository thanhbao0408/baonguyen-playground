using AutoMapper;
using BN.CleanArchitecture.Core.Domain.Cqrs;
using BN.CleanArchitecture.Core.Repository;
using MediatR;
using Playground.Application.Contracts.Dtos.Blog.Articles;
using Playground.Application.Contracts.Dtos.Blog.Taggings;
using Playground.Application.Specs.Taggings;
using Playground.Core.Entities.Blog.Articles;
using Playground.Core.Entities.Taggings;

namespace Playground.Application.Methods.Queries
{
    public class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, ResultModel<ListResultModel<TagDto>>>
    {
        private readonly IGridRepository<Tag, Guid> _tagRepository;
        private readonly IMapper _mapper;

        public GetTagsQueryHandler(IGridRepository<Tag, Guid> tagRepository, IMapper mapper)
        {
            _tagRepository =
                tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
            _mapper = mapper;
        }

        public async Task<ResultModel<ListResultModel<TagDto>>> Handle(GetTagsQuery request,
            CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var spec = new TagListQuerySpec<TagDto>(request);

            var tags = await _tagRepository.FindAsync(spec);

            var tagDtos = _mapper.Map<List<TagDto>>(tags);

            var totalTags = await _tagRepository.CountAsync(spec);

            var resultModel = ListResultModel<TagDto>.Create(tagDtos, totalTags, request.Page, request.PageSize);

            return ResultModel<ListResultModel<TagDto>>.Create(resultModel);
        }
    }
}

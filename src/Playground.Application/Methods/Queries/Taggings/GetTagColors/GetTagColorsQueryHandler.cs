using AutoMapper;
using BN.CleanArchitecture.Core.Domain.Cqrs;
using BN.CleanArchitecture.Core.Repository;
using MediatR;
using Playground.Application.Contracts.Dtos.Blog.Taggings;
using Playground.Application.Specs.Taggings;
using Playground.Core.Entities.Taggings;

namespace Playground.Application.Methods.Queries
{
    public class GetTagColorsQueryHandler : IRequestHandler<GetTagColorsQuery, ResultModel<List<TagColorDto>>>
    {
        private readonly IRepository<TagColor, Guid> _tagColorRepo;
        private readonly IMapper _mapper;

        public GetTagColorsQueryHandler(IRepository<TagColor, Guid> tagColorRepo, IMapper mapper)
        {
            _tagColorRepo = tagColorRepo;
            _mapper = mapper;
        }

        public async Task<ResultModel<List<TagColorDto>>> Handle(GetTagColorsQuery request,
            CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var spec = new TagColorQuerySpec<TagColorDto>();

            var tagColors = await _tagColorRepo.FindAsync(spec);

            var tagColorDtos = _mapper.Map<List<TagColorDto>>(tagColors);

            return ResultModel<List<TagColorDto>>.Create(tagColorDtos);
        }
    }
}

using AutoMapper;
using BN.CleanArchitecture.Core.Domain.Cqrs;
using BN.CleanArchitecture.Core.Repository;
using MediatR;
using Playground.Application.Contracts.Dtos.Blog.Taggings;
using Playground.Core.Entities.Taggings;

namespace Playground.Application.Methods.Commands.Tags.CreateTag
{
    public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, ResultModel<TagDto>>
    {
        private readonly IRepository<Tag, Guid> _tagRepo;
        private readonly IMapper _mapper;

        public CreateTagCommandHandler(IRepository<Tag, Guid> tagRepo, IMapper mapper)
        {
            _tagRepo = tagRepo;
            _mapper = mapper;
        }
        
        public async Task<ResultModel<TagDto>> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            var tag = _mapper.Map<Tag>(request.Model);

            await _tagRepo.AddAsync(tag);

            var tagDto = _mapper.Map<TagDto>(tag);

            return ResultModel<TagDto>.Create(tagDto);
        }
    }
}

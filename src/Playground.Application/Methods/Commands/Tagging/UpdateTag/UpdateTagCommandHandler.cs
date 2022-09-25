using AutoMapper;
using BN.CleanArchitecture.Core.Domain.Cqrs;
using BN.CleanArchitecture.Core.Repository;
using BN.Common;
using MediatR;
using Playground.Application.Contracts.Dtos.Blog.Taggings;
using Playground.Core.Entities.Taggings;
using System.Collections.ObjectModel;

namespace Playground.Application.Methods.Commands.Tags.CreateTag
{
    public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand, ResultModel<TagDto>>
    {
        private readonly IRepository<Tag, Guid> _tagRepository;
        private readonly IMapper _mapper;

        public UpdateTagCommandHandler(IRepository<Tag, Guid> tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        public async Task<ResultModel<TagDto>> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            var tag = await _tagRepository.FindByIdAsync(request.Model.Id);

            tag.Name = request.Model.Name;
            tag.ColorId = request.Model.ColorId;

            await _tagRepository.UpdateAsync(tag);

            var _tagDetailDto = _mapper.Map<TagDto>(tag);

            return ResultModel<TagDto>.Create(_tagDetailDto);
        }
    }
}

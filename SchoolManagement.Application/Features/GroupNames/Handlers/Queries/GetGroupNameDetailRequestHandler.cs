using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.GroupNames;
using SchoolManagement.Application.Features.GroupNames.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.GroupNames.Handlers.Queries
{
    public class GetGroupNameDetailRequestHandler : IRequestHandler<GetGroupNameDetailRequest, GroupNameDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<GroupName> _GroupNameRepository;
        public GetGroupNameDetailRequestHandler(ISchoolManagementRepository<GroupName> GroupNameRepository, IMapper mapper)
        {
            _GroupNameRepository = GroupNameRepository;
            _mapper = mapper;
        }
        public async Task<GroupNameDto> Handle(GetGroupNameDetailRequest request, CancellationToken cancellationToken)
        {
            var GroupName = await _GroupNameRepository.Get(request.GroupNameId);
            return _mapper.Map<GroupNameDto>(GroupName);
        }
    }
}

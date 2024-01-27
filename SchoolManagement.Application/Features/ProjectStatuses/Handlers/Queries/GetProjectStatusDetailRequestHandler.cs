using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ProjectStatus;
using SchoolManagement.Application.Features.ProjectStatuses.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.ProjectStatuses.Handlers.Queries
{
    public class GetProjectStatusDetailRequestHandler : IRequestHandler<GetProjectStatusDetailRequest, ProjectStatusDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<ProjectStatus> _ProjectStatusRepository;
        public GetProjectStatusDetailRequestHandler(ISchoolManagementRepository<ProjectStatus> ProjectStatusRepository, IMapper mapper)
        {
            _ProjectStatusRepository = ProjectStatusRepository;
            _mapper = mapper;
        }
        public async Task<ProjectStatusDto> Handle(GetProjectStatusDetailRequest request, CancellationToken cancellationToken)
        {
            var ProjectStatus = await _ProjectStatusRepository.Get(request.ProjectStatusId);
            return _mapper.Map<ProjectStatusDto>(ProjectStatus);
        }
    }
}

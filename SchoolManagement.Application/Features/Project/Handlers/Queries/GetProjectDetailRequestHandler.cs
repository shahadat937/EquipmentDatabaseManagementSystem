using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Project;
using SchoolManagement.Application.Features.Project.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Project.Handlers.Queries
{
    public class GetProjectDetailRequestHandler : IRequestHandler<GetProjectDetailRequest, ProjectDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<SchoolManagement.Domain.Project> _ProjectRepository;
        private readonly ISchoolManagementRepository<ProjectBaseSchoolName> _ProjectBaseSchoolName;
        public GetProjectDetailRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.Project> ProjectRepository, IMapper mapper, ISchoolManagementRepository<ProjectBaseSchoolName> ProjectBaseSchoolName)
        {
            _ProjectRepository = ProjectRepository;
            _mapper = mapper;
            _ProjectBaseSchoolName = ProjectBaseSchoolName;

        }
        public async Task<ProjectDto> Handle(GetProjectDetailRequest request, CancellationToken cancellationToken)
        {
            var Project = await _ProjectRepository.Get(request.ProjectId);

            // Fetch only the BaseSchoolNameId values
            var baseSchoolIds = await _ProjectBaseSchoolName
                                 .Where(x => x.ProjectId == request.ProjectId)
                                 .Select(x => x.BaseSchoolNameId)
                                 .ToListAsync();


            var ProjectDto = _mapper.Map<ProjectDto>(Project);

            ProjectDto.BaseSchoolNameId =  baseSchoolIds;
           

            return ProjectDto;
        }


    }
}

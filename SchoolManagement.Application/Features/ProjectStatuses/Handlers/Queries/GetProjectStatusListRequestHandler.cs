using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.ProjectStatus;
using SchoolManagement.Application.Features.ProjectStatuses.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ProjectStatuses.Handlers.Queries
{
    public class GetProjectStatusListRequestHandler : IRequestHandler<GetProjectStatusListRequest, PagedResult<ProjectStatusDto>>
    {

        private readonly ISchoolManagementRepository<ProjectStatus> _ProjectStatusRepository;

        private readonly IMapper _mapper;

        public GetProjectStatusListRequestHandler(ISchoolManagementRepository<ProjectStatus> ProjectStatusRepository, IMapper mapper)
        {
            _ProjectStatusRepository = ProjectStatusRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ProjectStatusDto>> Handle(GetProjectStatusListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<ProjectStatus> ProjectStatuss = _ProjectStatusRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = ProjectStatuss.Count();
            ProjectStatuss = ProjectStatuss.OrderByDescending(x => x.ProjectStatusId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var ProjectStatusDtos = _mapper.Map<List<ProjectStatusDto>>(ProjectStatuss);
            var result = new PagedResult<ProjectStatusDto>(ProjectStatusDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}

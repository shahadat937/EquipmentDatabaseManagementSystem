using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.GroupNames;
using SchoolManagement.Application.Features.GroupNames.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.GroupNames.Handlers.Queries
{
    public class GetGroupNameListRequestHandler : IRequestHandler<GetGroupNameListRequest, PagedResult<GroupNameDto>>
    {

        private readonly ISchoolManagementRepository<GroupName> _GroupNameRepository;

        private readonly IMapper _mapper;

        public GetGroupNameListRequestHandler(ISchoolManagementRepository<GroupName> GroupNameRepository, IMapper mapper)
        {
            _GroupNameRepository = GroupNameRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<GroupNameDto>> Handle(GetGroupNameListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<GroupName> GroupNames = _GroupNameRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = GroupNames.Count();
            GroupNames = GroupNames.OrderByDescending(x => x.GroupNameId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var GroupNameDtos = _mapper.Map<List<GroupNameDto>>(GroupNames);
            var result = new PagedResult<GroupNameDto>(GroupNameDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}

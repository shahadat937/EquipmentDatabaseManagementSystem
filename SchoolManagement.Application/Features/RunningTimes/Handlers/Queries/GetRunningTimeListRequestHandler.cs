using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.RunningTimes;
using SchoolManagement.Application.Features.RunningTimes.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.RunningTimes.Handlers.Queries
{
    public class GetRunningTimeListRequestHandler : IRequestHandler<GetRunningTimeListRequest, PagedResult<RunningTimeDto>>
    {

        private readonly ISchoolManagementRepository<RunningTime> _RunningTimeRepository;

        private readonly IMapper _mapper;

        public GetRunningTimeListRequestHandler(ISchoolManagementRepository<RunningTime> RunningTimeRepository, IMapper mapper)
        {
            _RunningTimeRepository = RunningTimeRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<RunningTimeDto>> Handle(GetRunningTimeListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<RunningTime> RunningTimes = _RunningTimeRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = RunningTimes.Count();
            RunningTimes = RunningTimes.OrderByDescending(x => x.RunningTimeId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var RunningTimeDtos = _mapper.Map<List<RunningTimeDto>>(RunningTimes);
            var result = new PagedResult<RunningTimeDto>(RunningTimeDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}

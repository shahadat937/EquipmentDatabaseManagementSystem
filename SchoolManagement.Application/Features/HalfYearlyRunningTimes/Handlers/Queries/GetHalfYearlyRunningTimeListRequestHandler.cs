using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.HalfYearlyRunningTime;
using SchoolManagement.Application.Features.HalfYearlyRunningTimes.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.HalfYearlyRunningTimes.Handlers.Queries
{
    public class GetHalfYearlyRunningTimeListRequestHandler : IRequestHandler<GetHalfYearlyRunningTimeListRequest, PagedResult<HalfYearlyRunningTimeDto>>
    {

        private readonly ISchoolManagementRepository<HalfYearlyRunningTime> _HalfYearlyRunningTimeRepository;

        private readonly IMapper _mapper;

        public GetHalfYearlyRunningTimeListRequestHandler(ISchoolManagementRepository<HalfYearlyRunningTime> HalfYearlyRunningTimeRepository, IMapper mapper)
        {
            _HalfYearlyRunningTimeRepository = HalfYearlyRunningTimeRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<HalfYearlyRunningTimeDto>> Handle(GetHalfYearlyRunningTimeListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<HalfYearlyRunningTime> HalfYearlyRunningTimes = _HalfYearlyRunningTimeRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = HalfYearlyRunningTimes.Count();
            HalfYearlyRunningTimes = HalfYearlyRunningTimes.OrderByDescending(x => x.HalfYearlyRunningTimeId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var HalfYearlyRunningTimeDtos = _mapper.Map<List<HalfYearlyRunningTimeDto>>(HalfYearlyRunningTimes);
            var result = new PagedResult<HalfYearlyRunningTimeDto>(HalfYearlyRunningTimeDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}

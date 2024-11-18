using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.MonthlyReturns;
using SchoolManagement.Application.Features.MonthlyReturns.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.MonthlyReturns.Handlers.Queries
{
    public class GetMonthlyReturnListRequestHandler : IRequestHandler<GetMonthlyReturnListRequest, PagedResult<MonthlyReturnDto>>
    {

        private readonly ISchoolManagementRepository<MonthlyReturn> _MonthlyReturnRepository;

        private readonly IMapper _mapper;

        public GetMonthlyReturnListRequestHandler(ISchoolManagementRepository<MonthlyReturn> MonthlyReturnRepository, IMapper mapper)
        {
            _MonthlyReturnRepository = MonthlyReturnRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<MonthlyReturnDto>> Handle(GetMonthlyReturnListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<MonthlyReturn> MonthlyReturns = _MonthlyReturnRepository.FilterWithInclude(x => String.IsNullOrEmpty(request.QueryParams.SearchText), "EquipmentCategory", "EqupmentName", "ReportingMonth", "OperationalStatus", "ReturnType","BaseSchoolName");
            var totalCount = MonthlyReturns.Count();
            MonthlyReturns = MonthlyReturns.OrderByDescending(x => x.MonthlyReturnId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var MonthlyReturnDtos = _mapper.Map<List<MonthlyReturnDto>>(MonthlyReturns);
            var result = new PagedResult<MonthlyReturnDto>(MonthlyReturnDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}

using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.HalfYearlyReturn;
using SchoolManagement.Application.Features.HalfYearlyReturns.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.HalfYearlyReturns.Handlers.Queries
{
    public class GetHalfYearlyReturnListRequestHandler : IRequestHandler<GetHalfYearlyReturnListRequest, PagedResult<HalfYearlyReturnDto>>
    {

        private readonly ISchoolManagementRepository<HalfYearlyReturn> _HalfYearlyReturnRepository;

        private readonly IMapper _mapper;

        public GetHalfYearlyReturnListRequestHandler(ISchoolManagementRepository<HalfYearlyReturn> HalfYearlyReturnRepository, IMapper mapper)
        {
            _HalfYearlyReturnRepository = HalfYearlyReturnRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<HalfYearlyReturnDto>> Handle(GetHalfYearlyReturnListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<HalfYearlyReturn> HalfYearlyReturns = _HalfYearlyReturnRepository.FilterWithInclude(x => (x.InputPowerSupply.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)), "EquipmentCategory", "EqupmentName");
            var totalCount = HalfYearlyReturns.Count();
            HalfYearlyReturns = HalfYearlyReturns.OrderByDescending(x => x.HalfYearlyReturnId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var HalfYearlyReturnDtos = _mapper.Map<List<HalfYearlyReturnDto>>(HalfYearlyReturns);
            var result = new PagedResult<HalfYearlyReturnDto>(HalfYearlyReturnDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}

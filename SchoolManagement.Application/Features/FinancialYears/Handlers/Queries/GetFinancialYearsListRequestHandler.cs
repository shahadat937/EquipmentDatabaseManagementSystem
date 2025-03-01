using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Domain;
using SchoolManagement.Application.Features.YearSetups.Requests.Queries;
using SchoolManagement.Application.DTOs.FinancialYears;

namespace SchoolManagement.Application.Features.ReporingYears.Handlers.Queries
{
    public class GetFinancialYearsListRequestHandler : IRequestHandler<GetFinancialYearsListRequest, PagedResult<FinancialYearsDto>>
    {

        private readonly ISchoolManagementRepository<SchoolManagement.Domain.FinancialYears> _ReporingYearRepository;

        private readonly IMapper _mapper;

        public GetFinancialYearsListRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.FinancialYears> ReporingYearRepository, IMapper mapper)
        {
            _ReporingYearRepository = ReporingYearRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<FinancialYearsDto>> Handle(GetFinancialYearsListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<SchoolManagement.Domain.FinancialYears> ReporingYears = _ReporingYearRepository.FilterWithInclude(x => (x.FinancialYearName.ToString().Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = ReporingYears.Count();
            ReporingYears = ReporingYears.OrderByDescending(x => x.FinancialYearId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var ReporingYearDtos = _mapper.Map<List<FinancialYearsDto>>(ReporingYears);
            var result = new PagedResult<FinancialYearsDto>(ReporingYearDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}

using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.DTOs.YearlyReturns;
using SchoolManagement.Application.Features.YearlyReturns.Request.Queries;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Models;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagement.Application.Exceptions;

namespace SchoolManagement.Application.Features.YearlyReturns.Handler.Queries
{
    public class GetYearlyReturnListRequestHandler : IRequestHandler<GetYearlyReturnListRequest, PagedResult<YearlyReturnDto>>
    {
        private readonly ISchoolManagementRepository<YearlyReturn> _yearlyReturnRepository;
        private readonly IMapper _mapper;

        public GetYearlyReturnListRequestHandler(ISchoolManagementRepository<YearlyReturn> yearlyReturnRepository, IMapper mapper)
        {
            _yearlyReturnRepository = yearlyReturnRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<YearlyReturnDto>> Handle(GetYearlyReturnListRequest request, CancellationToken cancellationToken)
        {
            // Validate the query parameters
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);

            // Query the repository with filtering and including related entities
            IQueryable<YearlyReturn> yearlyReturns = _yearlyReturnRepository.FilterWithInclude(
                x => string.IsNullOrEmpty(request.QueryParams.SearchText) ||
                     x.BaseSchoolName.SchoolName.Contains(request.QueryParams.SearchText) ||
                     x.ReportingYearId.ToString().Contains(request.QueryParams.SearchText),
                "BaseSchoolName",
                "OperationalStatus",
                "ReportingMonth");

            // Get total count before applying pagination
            var totalCount = yearlyReturns.Count();

            // Apply sorting and pagination
            yearlyReturns = yearlyReturns
                .OrderByDescending(x => x.YearlyReturnId)
                .Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize)
                .Take(request.QueryParams.PageSize);

            // Map to DTO
            var yearlyReturnDtos = _mapper.Map<List<YearlyReturnDto>>(yearlyReturns);

            // Prepare paged result
            var result = new PagedResult<YearlyReturnDto>(
                yearlyReturnDtos,
                totalCount,
                request.QueryParams.PageNumber,
                request.QueryParams.PageSize);

            return result;
        }
    }
}

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.DTOs.YearlyReturns;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.YearlyReturns.Request.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.YearlyReturns.Handler.Queries
{
    public class GetYearlyReturnsByAuthorityIdRequestHandler : IRequestHandler<GetYearlyReturnsByAuthorityIdRequest, PagedResult<YearlyReturnDto>>
    {
        private readonly ISchoolManagementRepository<YearlyReturn> _yearlyReturn;
        private readonly IMapper _mapper;

        private readonly IConfiguration _config;

        public GetYearlyReturnsByAuthorityIdRequestHandler(ISchoolManagementRepository<YearlyReturn> yearlyReturn, IMapper mapper, IConfiguration config)
        {
            _mapper = mapper;
            _yearlyReturn = yearlyReturn;
            _config = config;
        }
        public async Task<PagedResult< YearlyReturnDto>> Handle(GetYearlyReturnsByAuthorityIdRequest request, CancellationToken cancellationToken)
        {

            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);


            IQueryable<YearlyReturn> yearlyReturns = _yearlyReturn.FilterWithInclude(
                x => x.BaseSchoolName.ShipInformations.Any(u=> u.AuthorityId == request.AuthorityId) && (string.IsNullOrEmpty(request.QueryParams.SearchText) ||
                     x.BaseSchoolName.SchoolName.Contains(request.QueryParams.SearchText)),
                "BaseSchoolName", "OperationalStatus", "ReportingMonth"
            );


            var totalCount = await yearlyReturns.CountAsync();


            yearlyReturns = yearlyReturns
                .OrderByDescending(x => x.YearlyReturnId)
                .Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize)
                .Take(request.QueryParams.PageSize);

            var yearlyReturnDtos = _mapper.Map<List<YearlyReturnDto>>(yearlyReturns);

            var result = new PagedResult<YearlyReturnDto>(
                yearlyReturnDtos,
                totalCount,
                request.QueryParams.PageNumber,
                request.QueryParams.PageSize
            );

            return result;
        }
    }
}

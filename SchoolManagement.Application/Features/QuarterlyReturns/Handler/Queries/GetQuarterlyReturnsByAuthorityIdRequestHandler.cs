using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.DTOs.QuarterlyReturns;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.QuarterlyReturns.Request.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.QuarterlyReturns.Handler.Queries
{
    public class GetQuarterlyReturnsByAuthorityIdRequestHandler : IRequestHandler<GetQuarterlyReturnsByAuthorityIdRequest, PagedResult<QuarterlyReturnDto>>
    {
        private readonly ISchoolManagementRepository<QuarterlyReturn> _QuarterlyReturn;
        private readonly IMapper _mapper;

        private readonly IConfiguration _config;

        public GetQuarterlyReturnsByAuthorityIdRequestHandler(ISchoolManagementRepository<QuarterlyReturn> QuarterlyReturn, IMapper mapper, IConfiguration config)
        {
            _mapper = mapper;
            _QuarterlyReturn = QuarterlyReturn;
            _config = config;
        }
        public async Task<PagedResult< QuarterlyReturnDto>> Handle(GetQuarterlyReturnsByAuthorityIdRequest request, CancellationToken cancellationToken)
        {

            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);


            IQueryable<QuarterlyReturn> QuarterlyReturns = _QuarterlyReturn.FilterWithInclude(
                x => x.BaseSchoolName.ShipInformations.Any(u=> u.AuthorityId == request.AuthorityId) && (string.IsNullOrEmpty(request.QueryParams.SearchText) ||
                     x.BaseSchoolName.SchoolName.Contains(request.QueryParams.SearchText)),
                "BaseSchoolName", "OperationalStatus", "ReportingMonth"
            );


            var totalCount = await QuarterlyReturns.CountAsync();


            QuarterlyReturns = QuarterlyReturns
                .OrderByDescending(x => x.QuarterlyReturnId)
                .Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize)
                .Take(request.QueryParams.PageSize);

            var QuarterlyReturnDtos = _mapper.Map<List<QuarterlyReturnDto>>(QuarterlyReturns);

            var result = new PagedResult<QuarterlyReturnDto>(
                QuarterlyReturnDtos,
                totalCount,
                request.QueryParams.PageNumber,
                request.QueryParams.PageSize
            );

            return result;
        }
    }
}

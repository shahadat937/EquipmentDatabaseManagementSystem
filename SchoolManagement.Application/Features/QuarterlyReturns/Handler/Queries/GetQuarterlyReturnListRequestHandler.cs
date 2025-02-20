using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.DTOs.QuarterlyReturns;
using SchoolManagement.Application.Features.QuarterlyReturns.Request.Queries;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Models;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagement.Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.DTOs.ShipDrowings;

namespace SchoolManagement.Application.Features.QuarterlyReturns.Handler.Queries
{
    public class GetQuarterlyReturnListRequestHandler : IRequestHandler<GetQuarterlyReturnListRequest, PagedResult<QuarterlyReturnDto>>
    {
        private readonly ISchoolManagementRepository<QuarterlyReturn> _QuarterlyReturnRepository;
        private readonly IMapper _mapper;

        public GetQuarterlyReturnListRequestHandler(ISchoolManagementRepository<QuarterlyReturn> QuarterlyReturnRepository, IMapper mapper)
        {
            _QuarterlyReturnRepository = QuarterlyReturnRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<QuarterlyReturnDto>> Handle(GetQuarterlyReturnListRequest request, CancellationToken cancellationToken)
        {
           
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);

            
            IQueryable<QuarterlyReturn> QuarterlyReturns = _QuarterlyReturnRepository.FilterWithInclude(
                x => (request.ShipId == 0 || x.BaseSchoolNameId == request.ShipId) && (string.IsNullOrEmpty(request.QueryParams.SearchText) ||
                     x.BaseSchoolName.SchoolName.Contains(request.QueryParams.SearchText)),
                "BaseSchoolName", "OperationalStatus","ReportingMonth", "ReportingYear"
            );

         
            var totalCount = await QuarterlyReturns.CountAsync();

          
            QuarterlyReturns = QuarterlyReturns
                .OrderByDescending(x => x.QuarterlyReturnId)
                .Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize)
                .Take(request.QueryParams.PageSize);

            
            //var QuarterlyReturnDtos = await _mapper.ProjectTo<QuarterlyReturnDto>(QuarterlyReturns).ToListAsync();
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

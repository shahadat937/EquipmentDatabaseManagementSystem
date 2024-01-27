using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.ReportingMonths.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Application.DTOs.ShipInformationTypes;
using SchoolManagement.Doamin;
using SchoolManagement.Application.DTOs.ReportingMonths;

namespace SchoolManagement.Application.Features.ReportingMonths.Handlers.Queries
{
    public class GetReportingMonthListRequestHandler : IRequestHandler<GetReportingMonthListRequest, PagedResult<ReportingMonthDto>>
    {

        private readonly ISchoolManagementRepository<ReportingMonth> _ReportingMonthRepository;

        private readonly IMapper _mapper;

        public GetReportingMonthListRequestHandler(ISchoolManagementRepository<ReportingMonth> ReportingMonthRepository, IMapper mapper)
        {
            _ReportingMonthRepository = ReportingMonthRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ReportingMonthDto>> Handle(GetReportingMonthListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<ReportingMonth> ReportingMonths = _ReportingMonthRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = ReportingMonths.Count();
            ReportingMonths = ReportingMonths.OrderByDescending(x => x.ReportingMonthId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var ReportingMonthDtos = _mapper.Map<List<ReportingMonthDto>>(ReportingMonths);
            var result = new PagedResult<ReportingMonthDto>(ReportingMonthDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}

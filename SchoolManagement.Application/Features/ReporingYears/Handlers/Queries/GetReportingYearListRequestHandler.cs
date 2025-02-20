using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Domain;
using SchoolManagement.Application.Features.YearSetups.Requests.Queries;
using SchoolManagement.Application.DTOs.ReportingYear;

namespace SchoolManagement.Application.Features.ReporingYears.Handlers.Queries
{
    public class GetReportingYearListRequestHandler : IRequestHandler<GetReportingYearListRequest, PagedResult<ReportingYearDto>>
    {

        private readonly ISchoolManagementRepository<ReportingYear> _ReporingYearRepository;

        private readonly IMapper _mapper;

        public GetReportingYearListRequestHandler(ISchoolManagementRepository<ReportingYear> ReporingYearRepository, IMapper mapper)
        {
            _ReporingYearRepository = ReporingYearRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ReportingYearDto>> Handle(GetReportingYearListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<ReportingYear> ReporingYears = _ReporingYearRepository.FilterWithInclude(x => (x.Year.ToString().Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = ReporingYears.Count();
            ReporingYears = ReporingYears.OrderByDescending(x => x.Year).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var ReporingYearDtos = _mapper.Map<List<ReportingYearDto>>(ReporingYears);
            var result = new PagedResult<ReportingYearDto>(ReporingYearDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}

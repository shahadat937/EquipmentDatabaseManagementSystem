using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.ReportType;
using SchoolManagement.Application.Features.ReportTypes.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ReportTypes.Handlers.Queries
{
    public class GetReportTypeListRequestHandler : IRequestHandler<GetReportTypeListRequest, PagedResult<ReportTypeDto>>
    {

        private readonly ISchoolManagementRepository<ReportType> _ReportTypeRepository;

        private readonly IMapper _mapper;

        public GetReportTypeListRequestHandler(ISchoolManagementRepository<ReportType> ReportTypeRepository, IMapper mapper)
        {
            _ReportTypeRepository = ReportTypeRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ReportTypeDto>> Handle(GetReportTypeListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<ReportType> ReportTypes = _ReportTypeRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = ReportTypes.Count();
            ReportTypes = ReportTypes.OrderByDescending(x => x.ReportTypeId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var ReportTypeDtos = _mapper.Map<List<ReportTypeDto>>(ReportTypes);
            var result = new PagedResult<ReportTypeDto>(ReportTypeDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}

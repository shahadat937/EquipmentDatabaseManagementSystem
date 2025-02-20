using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.YearSetup;
using SchoolManagement.Application.Features.YearSetups.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.YearSetups.Handlers.Queries
{
    public class GetYearSetupListRequestHandler : IRequestHandler<GetYearSetupListRequest, PagedResult<YearSetupDto>>
    {

        private readonly ISchoolManagementRepository<YearSetup> _YearSetupRepository;

        private readonly IMapper _mapper;

        public GetYearSetupListRequestHandler(ISchoolManagementRepository<YearSetup> YearSetupRepository, IMapper mapper)
        {
            _YearSetupRepository = YearSetupRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<YearSetupDto>> Handle(GetYearSetupListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<YearSetup> YearSetups = _YearSetupRepository.FilterWithInclude(x => (x.Year.ToString().Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = YearSetups.Count();
            YearSetups = YearSetups.OrderByDescending(x => x.Year).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var YearSetupDtos = _mapper.Map<List<YearSetupDto>>(YearSetups);
            var result = new PagedResult<YearSetupDto>(YearSetupDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}

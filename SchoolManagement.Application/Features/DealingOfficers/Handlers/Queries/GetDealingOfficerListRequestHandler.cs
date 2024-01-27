using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.DealingOfficer;
using SchoolManagement.Application.Features.DealingOfficers.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.DealingOfficers.Handlers.Queries
{
    public class GetDealingOfficerListRequestHandler : IRequestHandler<GetDealingOfficerListRequest, PagedResult<DealingOfficerDto>>
    {

        private readonly ISchoolManagementRepository<DealingOfficer> _DealingOfficerRepository;

        private readonly IMapper _mapper;

        public GetDealingOfficerListRequestHandler(ISchoolManagementRepository<DealingOfficer> DealingOfficerRepository, IMapper mapper)
        {
            _DealingOfficerRepository = DealingOfficerRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<DealingOfficerDto>> Handle(GetDealingOfficerListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<DealingOfficer> DealingOfficers = _DealingOfficerRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = DealingOfficers.Count();
            DealingOfficers = DealingOfficers.OrderByDescending(x => x.DealingOfficerId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var DealingOfficerDtos = _mapper.Map<List<DealingOfficerDto>>(DealingOfficers);
            var result = new PagedResult<DealingOfficerDto>(DealingOfficerDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}

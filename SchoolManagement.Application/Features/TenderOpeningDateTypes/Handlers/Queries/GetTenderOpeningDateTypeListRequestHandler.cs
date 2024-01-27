using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.TenderOpeningDateTypes;
using SchoolManagement.Application.Features.TenderOpeningDateTypes.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.TenderOpeningDateTypes.Handlers.Queries
{
    public class GetTenderOpeningDateTypeListRequestHandler : IRequestHandler<GetTenderOpeningDateTypeListRequest, PagedResult<TenderOpeningDateTypeDto>>
    {

        private readonly ISchoolManagementRepository<TenderOpeningDateType> _TenderOpeningDateTypeRepository;

        private readonly IMapper _mapper;

        public GetTenderOpeningDateTypeListRequestHandler(ISchoolManagementRepository<TenderOpeningDateType> TenderOpeningDateTypeRepository, IMapper mapper)
        {
            _TenderOpeningDateTypeRepository = TenderOpeningDateTypeRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<TenderOpeningDateTypeDto>> Handle(GetTenderOpeningDateTypeListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<TenderOpeningDateType> TenderOpeningDateTypes = _TenderOpeningDateTypeRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = TenderOpeningDateTypes.Count();
            TenderOpeningDateTypes = TenderOpeningDateTypes.OrderByDescending(x => x.TenderOpeningDateTypeId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var TenderOpeningDateTypeDtos = _mapper.Map<List<TenderOpeningDateTypeDto>>(TenderOpeningDateTypes);
            var result = new PagedResult<TenderOpeningDateTypeDto>(TenderOpeningDateTypeDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}

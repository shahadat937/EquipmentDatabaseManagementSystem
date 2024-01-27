using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.OperationalStates.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Application.DTOs.ShipInformationTypes;
using SchoolManagement.Doamin;
using SchoolManagement.Application.DTOs.OperationalStates;

namespace SchoolManagement.Application.Features.OperationalStates.Handlers.Queries
{
    public class GetOperationalStateListRequestHandler : IRequestHandler<GetOperationalStateListRequest, PagedResult<OperationalStateDto>>
    {

        private readonly ISchoolManagementRepository<OperationalState> _OperationalStateRepository;

        private readonly IMapper _mapper;

        public GetOperationalStateListRequestHandler(ISchoolManagementRepository<OperationalState> OperationalStateRepository, IMapper mapper)
        {
            _OperationalStateRepository = OperationalStateRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<OperationalStateDto>> Handle(GetOperationalStateListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<OperationalState> OperationalStates = _OperationalStateRepository.FilterWithInclude(x => (String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = OperationalStates.Count();
            OperationalStates = OperationalStates.OrderByDescending(x => x.OperationalStateId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var OperationalStateDtos = _mapper.Map<List<OperationalStateDto>>(OperationalStates);
            var result = new PagedResult<OperationalStateDto>(OperationalStateDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}

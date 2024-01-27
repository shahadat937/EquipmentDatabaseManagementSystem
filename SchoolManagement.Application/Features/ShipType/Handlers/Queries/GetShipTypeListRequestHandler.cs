using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.ShipTypes.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Application.DTOs.ShipInformationTypes;

namespace SchoolManagement.Application.Features.ShipTypes.Handlers.Queries
{
    public class GetShipTypeListRequestHandler : IRequestHandler<GetShipTypeListRequest, PagedResult<ShipTypeDto>>
    {

        private readonly ISchoolManagementRepository<ShipType> _ShipTypeRepository;

        private readonly IMapper _mapper;

        public GetShipTypeListRequestHandler(ISchoolManagementRepository<ShipType> ShipTypeRepository, IMapper mapper)
        {
            _ShipTypeRepository = ShipTypeRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ShipTypeDto>> Handle(GetShipTypeListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<ShipType> ShipTypes = _ShipTypeRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = ShipTypes.Count();
            ShipTypes = ShipTypes.OrderByDescending(x => x.ShipTypeId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var ShipTypeDtos = _mapper.Map<List<ShipTypeDto>>(ShipTypes);
            var result = new PagedResult<ShipTypeDto>(ShipTypeDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}

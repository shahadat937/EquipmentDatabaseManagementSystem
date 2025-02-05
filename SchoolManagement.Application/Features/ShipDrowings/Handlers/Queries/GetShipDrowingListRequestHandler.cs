using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.ShipDrowings;
using SchoolManagement.Application.Features.ShipDrowings.Requests.Queries;

namespace SchoolManagement.Application.Features.ShipDrowings.Handlers.Queries
{
    public class GetShipDrowingListRequestHandler : IRequestHandler<GetShipDrowingListRequest, PagedResult<ShipDrowingDto>>
    {

        private readonly ISchoolManagementRepository<SchoolManagement.Domain.ShipDrowing> _ShipDrowingRepository;

        private readonly IMapper _mapper;

        public GetShipDrowingListRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.ShipDrowing> ShipDrowingRepository, IMapper mapper)
        {
            _ShipDrowingRepository = ShipDrowingRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ShipDrowingDto>> Handle(GetShipDrowingListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<SchoolManagement.Domain.ShipDrowing> ShipDrowings = _ShipDrowingRepository.FilterWithInclude(x => (request.ShipId == 0 || x.BaseSchoolNameId == request.ShipId) && ((x.BaseSchoolName.SchoolName.Contains(request.QueryParams.SearchText)) || String.IsNullOrEmpty(request.QueryParams.SearchText)), "Authority", "BaseName", "BaseSchoolName");
            var totalCount = ShipDrowings.Count();
            ShipDrowings = ShipDrowings.OrderByDescending(x => x.ShipDrowingId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var ShipDrowingDtos = _mapper.Map<List<ShipDrowingDto>>(ShipDrowings);
            var result = new PagedResult<ShipDrowingDto>(ShipDrowingDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}

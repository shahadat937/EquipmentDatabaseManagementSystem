using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.ShipEquipmentInfo;
using SchoolManagement.Application.Features.ShipEquipmentInfos.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ShipEquipmentInfos.Handlers.Queries
{
    public class GetShipEquipmentInfoListRequestHandler : IRequestHandler<GetShipEquipmentInfoListRequest, PagedResult<ShipEquipmentInfoDto>>
    {

        private readonly ISchoolManagementRepository<ShipEquipmentInfo> _ShipEquipmentInfoRepository;

        private readonly IMapper _mapper;

        public GetShipEquipmentInfoListRequestHandler(ISchoolManagementRepository<ShipEquipmentInfo> ShipEquipmentInfoRepository, IMapper mapper)
        {
            _ShipEquipmentInfoRepository = ShipEquipmentInfoRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ShipEquipmentInfoDto>> Handle(GetShipEquipmentInfoListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<ShipEquipmentInfo> ShipEquipmentInfos = _ShipEquipmentInfoRepository.FilterWithInclude(x => x.BaseSchoolNameId == (request.ShipId != 0 ? request.ShipId : x.BaseSchoolNameId) && (x.Model.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)), "EquipmentCategory", "EqupmentName", "StateOfEquipment", "BaseSchoolName");
            var totalCount = ShipEquipmentInfos.Count();
            ShipEquipmentInfos = ShipEquipmentInfos.OrderByDescending(x => x.ShipEquipmentInfoId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var ShipEquipmentInfoDtos = _mapper.Map<List<ShipEquipmentInfoDto>>(ShipEquipmentInfos);
            var result = new PagedResult<ShipEquipmentInfoDto>(ShipEquipmentInfoDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}

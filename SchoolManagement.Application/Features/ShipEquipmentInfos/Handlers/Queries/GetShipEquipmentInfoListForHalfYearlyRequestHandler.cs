using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ShipEquipmentInfo;
using SchoolManagement.Application.Features.ShipEquipmentInfos.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ShipEquipmentInfos.Handlers.Queries
{
    public class GetShipEquipmentInfoListForHalfYearlyRequestHandler :  IRequestHandler<GetShipEquipmentInfoListForHalfYearlyRequest, List<ShipEquipmentInfoDto>>
    {
        private readonly ISchoolManagementRepository<ShipEquipmentInfo> _ShipEquipmentInfoRepository;
        private readonly IMapper _mapper;


        public GetShipEquipmentInfoListForHalfYearlyRequestHandler(ISchoolManagementRepository<ShipEquipmentInfo> ShipEquipmentInfoRepository, IMapper mapper)
        {
            _ShipEquipmentInfoRepository = ShipEquipmentInfoRepository;
            _mapper = mapper;
        }

        public async Task<List<ShipEquipmentInfoDto>> Handle(GetShipEquipmentInfoListForHalfYearlyRequest request, CancellationToken cancellationToken)
        {
            IQueryable<ShipEquipmentInfo> ShipEquipmentInfos = _ShipEquipmentInfoRepository.FilterWithInclude(x => x.EquipmentCategoryId == request.EquipmentCategoryId && x.EqupmentNameId == request.EqupmentNameId && x.BaseSchoolNameId == request.ShipId, "EquipmentCategory", "EqupmentName", "StateOfEquipment");
            
            var IssueRegisterDtos = _mapper.Map<List<ShipEquipmentInfoDto>>(ShipEquipmentInfos);

            return IssueRegisterDtos;
        }
    }
}

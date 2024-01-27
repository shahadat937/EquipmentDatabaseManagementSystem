using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ShipEquipmentInfo;
using SchoolManagement.Application.Features.ShipEquipmentInfos.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.ShipEquipmentInfos.Handlers.Queries
{
    public class GetShipEquipmentInfoDetailRequestHandler : IRequestHandler<GetShipEquipmentInfoDetailRequest, ShipEquipmentInfoDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<ShipEquipmentInfo> _ShipEquipmentInfoRepository;
        public GetShipEquipmentInfoDetailRequestHandler(ISchoolManagementRepository<ShipEquipmentInfo> ShipEquipmentInfoRepository, IMapper mapper)
        {
            _ShipEquipmentInfoRepository = ShipEquipmentInfoRepository;
            _mapper = mapper;
        }
        public async Task<ShipEquipmentInfoDto> Handle(GetShipEquipmentInfoDetailRequest request, CancellationToken cancellationToken)
        {
            var ShipEquipmentInfo = await _ShipEquipmentInfoRepository.Get(request.ShipEquipmentInfoId);
            return _mapper.Map<ShipEquipmentInfoDto>(ShipEquipmentInfo);
        }
    }
}

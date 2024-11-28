using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.ShipEquipmentInfos.Requests.Queries;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.ShipEquipmentInfos.Handlers.Queries
{
    public class GetCompatSystemEequipmentCountRequestByCommandingAreaRequestHandler : IRequestHandler<GetCompatSystemEequipmentCountRequestByCommandingAreaRequest, object>
    {
        private readonly ISchoolManagementRepository<ShipEquipmentInfo> _shipEquipmentInfoRepository;
        private readonly IMapper _mapper;
        public GetCompatSystemEequipmentCountRequestByCommandingAreaRequestHandler(ISchoolManagementRepository<ShipEquipmentInfo> shipEquipmentRepository, IMapper mapper)
        {
            _mapper = mapper;
            _shipEquipmentInfoRepository = shipEquipmentRepository;
        }
        public async Task<object> Handle(GetCompatSystemEequipmentCountRequestByCommandingAreaRequest request, CancellationToken cancellationToken)
        {
            var spQuery = String.Format("exec [spGetCombatSystemEquipmentCountByAuthorityId ] {0}, {1}, {2}, {3}", request.CombatSystemId, request.StateOfEquipmentId1, request.StateOfEquipmentId2, request.CommandingAreaId);
            DataTable dataTable = _shipEquipmentInfoRepository.ExecWithSqlQuery(spQuery);
            return dataTable;
        }
    }
}

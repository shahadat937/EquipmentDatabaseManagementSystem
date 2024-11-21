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
    public class GetCompatSystemEequipmentCountRequestHandler : IRequestHandler<GetCompatSystemEequipmentCountRequest, object>
    {
        private readonly ISchoolManagementRepository<ShipEquipmentInfo> _shipEquipmentInfoRepository;
        private readonly IMapper _mapper;
        public GetCompatSystemEequipmentCountRequestHandler(ISchoolManagementRepository<ShipEquipmentInfo> shipEquipmentRepository, IMapper mapper)
        {
            _mapper = mapper;
            _shipEquipmentInfoRepository = shipEquipmentRepository;
        }
        public async Task<object> Handle(GetCompatSystemEequipmentCountRequest request, CancellationToken cancellationToken)
        {
            var spQuery = String.Format("exec [spGetCombatSystemEquipmentCount] {0}, {1}, {2}", request.CombatSystemId, request.StateOfEquipmentId1, request.StateOfEquipmentId2);
            DataTable dataTable = _shipEquipmentInfoRepository.ExecWithSqlQuery(spQuery);
            return dataTable;
        }
    }
}

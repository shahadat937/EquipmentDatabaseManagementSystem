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
    public class GetShipEquipmentCountByCategoryandCommaningAreaIdRequestHandlares : IRequestHandler<GetShipEquipmentCountByCategoryandCommaningAreaIdRequest, object>
    {
        private readonly ISchoolManagementRepository<ShipEquipmentInfo> _ShipEquipmentInfoRepository;

        private readonly IMapper _mapper;

        public GetShipEquipmentCountByCategoryandCommaningAreaIdRequestHandlares(ISchoolManagementRepository<ShipEquipmentInfo> ShipEquipmentInfoRepository, IMapper mapper)
        {
            _ShipEquipmentInfoRepository = ShipEquipmentInfoRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetShipEquipmentCountByCategoryandCommaningAreaIdRequest request, CancellationToken cancellationToken)
        {
            var spQuery = String.Format("exec [spGetShipEquipmentCountByAuthorityId] {0}, {1}, {2}", request.StateOfEquipmentId1, request.StateOfEquipmentId2, request.CommandingAreaId);
            DataTable dataTable = _ShipEquipmentInfoRepository.ExecWithSqlQuery(spQuery);
            return dataTable;
        }
    }
}

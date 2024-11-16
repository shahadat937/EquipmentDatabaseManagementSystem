using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ShipEquipmentInfo;
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
    public class GetShipEquipmentCountByCategoryRequestHandler : IRequestHandler<GetShipEquipmentCountByCategoryRequest, object>
    {
        private readonly ISchoolManagementRepository<ShipEquipmentInfo> _ShipEquipmentInfoRepository;

        private readonly IMapper _mapper;

        public GetShipEquipmentCountByCategoryRequestHandler(ISchoolManagementRepository<ShipEquipmentInfo> ShipEquipmentInfoRepository, IMapper mapper)
        {
            _ShipEquipmentInfoRepository = ShipEquipmentInfoRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetShipEquipmentCountByCategoryRequest request, CancellationToken cancellationToken)
        {
            var spQuery = String.Format("exec [spGetShipEquipmentCount] {0}, {1}", request.StateOfEquipmentId1, request.StateOfEquipmentId2);
            DataTable dataTable = _ShipEquipmentInfoRepository.ExecWithSqlQuery(spQuery);
            return dataTable;
        }
    }
}

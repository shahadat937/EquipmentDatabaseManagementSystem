using SchoolManagement.Application.Contracts.Persistence;
using MediatR;
using AutoMapper;
using SchoolManagement.Domain;
using SchoolManagement.Application.Features.ShipEquipmentInfos.Requests.Queries;
using System.Data;

namespace SchoolManagement.Application.Features.ShipEquipmentInfos.Handlers.Queries
{
    public class GetShipEquipListByBaseSchoolNameRequestHandler : IRequestHandler<GetShipEquipListByBaseSchoolNameRequest, object>
    {

        private readonly ISchoolManagementRepository<ShipInformation> _ShipInformationRepository;

        private readonly IMapper _mapper;

        public GetShipEquipListByBaseSchoolNameRequestHandler(ISchoolManagementRepository<ShipInformation> ShipInformationRepository, IMapper mapper)
        {
            _ShipInformationRepository = ShipInformationRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetShipEquipListByBaseSchoolNameRequest request, CancellationToken cancellationToken)
        {   
            var spQuery = String.Format("exec [spGetShipEquipListByBaseSchoolName] {0}", request.BaseSchoolNameId);
            
            DataTable dataTable = _ShipInformationRepository.ExecWithSqlQuery(spQuery);
            return dataTable;
         
        }
    }
}

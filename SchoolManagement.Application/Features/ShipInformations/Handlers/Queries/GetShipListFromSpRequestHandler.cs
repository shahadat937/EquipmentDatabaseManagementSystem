using SchoolManagement.Application.Contracts.Persistence;
using MediatR;
using AutoMapper;
using SchoolManagement.Domain;
using SchoolManagement.Application.Features.ShipInformations.Requests.Queries;
using System.Data;
using SchoolManagement.Application.Features.CourseDurations.Requests.Queries;

namespace SchoolManagement.Application.Features.ShipInformations.Handlers.Queries
{
    public class GetShipListFromSpRequestHandler : IRequestHandler<GetShipListFromSpRequest, object>
    {

        private readonly ISchoolManagementRepository<ShipInformation> _ShipInformationRepository;

        private readonly IMapper _mapper;

        public GetShipListFromSpRequestHandler(ISchoolManagementRepository<ShipInformation> ShipInformationRepository, IMapper mapper)
        {
            _ShipInformationRepository = ShipInformationRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetShipListFromSpRequest request, CancellationToken cancellationToken)
        {   
            var spQuery = String.Format("exec [spGetShipList] {0}", request.ShipTypeId);
            
            DataTable dataTable = _ShipInformationRepository.ExecWithSqlQuery(spQuery);
            return dataTable;
         
        }
    }
}

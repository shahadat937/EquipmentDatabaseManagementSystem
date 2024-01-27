using SchoolManagement.Application.Contracts.Persistence;
using MediatR;
using AutoMapper;
using SchoolManagement.Domain;
using SchoolManagement.Application.Features.ShipInformations.Requests.Queries;
using System.Data;
using SchoolManagement.Application.Features.CourseDurations.Requests.Queries;

namespace SchoolManagement.Application.Features.ShipInformations.Handlers.Queries
{
    public class GetAllShipInfoByBaseSpRequestHandler : IRequestHandler<GetAllShipInfoByBaseSpRequest, object>
    {

        private readonly ISchoolManagementRepository<ShipInformation> _ShipInformationRepository;

        private readonly IMapper _mapper;

        public GetAllShipInfoByBaseSpRequestHandler(ISchoolManagementRepository<ShipInformation> ShipInformationRepository, IMapper mapper)
        {
            _ShipInformationRepository = ShipInformationRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetAllShipInfoByBaseSpRequest request, CancellationToken cancellationToken)
        {   
            var spQuery = String.Format("exec [spGetAllShipInfoByBase] {0},{1}", request.AuthorityId, request.OperationalStatusId);
            
            DataTable dataTable = _ShipInformationRepository.ExecWithSqlQuery(spQuery);
            return dataTable;
         
        }
    }
}

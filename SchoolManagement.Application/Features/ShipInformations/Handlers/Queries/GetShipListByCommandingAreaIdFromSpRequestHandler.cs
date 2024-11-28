using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.ShipInformations.Requests.Queries;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.ShipInformations.Handlers.Queries
{
    public class GetShipListByCommandingAreaIdFromSpRequestHandler : IRequestHandler<GetShipListByCommandingAreaIdFromSpRequest, object>
    {

        private readonly ISchoolManagementRepository<ShipInformation> _ShipInformationRepository;

        private readonly IMapper _mapper;

        public GetShipListByCommandingAreaIdFromSpRequestHandler(ISchoolManagementRepository<ShipInformation> ShipInformationRepository, IMapper mapper)
        {
            _ShipInformationRepository = ShipInformationRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetShipListByCommandingAreaIdFromSpRequest request, CancellationToken cancellationToken)
        {
            var spQuery = String.Format("exec [spGetShipListByCommaningArea] {0}, {1}", request.ShipTypeId, request.CommandingAreaId);

            DataTable dataTable = _ShipInformationRepository.ExecWithSqlQuery(spQuery);
            return dataTable;

        }
    }
}

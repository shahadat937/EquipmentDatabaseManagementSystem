using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ShipInformations;
using SchoolManagement.Application.Features.ShipInformations.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.ShipInformations.Handlers.Queries
{
    public class GetShipInformationDetailRequestHandler : IRequestHandler<GetShipInformationDetailRequest, ShipInformationDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<ShipInformation> _ShipInformationRepository;
        public GetShipInformationDetailRequestHandler(ISchoolManagementRepository<ShipInformation> ShipInformationRepository, IMapper mapper)
        {
            _ShipInformationRepository = ShipInformationRepository;
            _mapper = mapper;
        }
        public async Task<ShipInformationDto> Handle(GetShipInformationDetailRequest request, CancellationToken cancellationToken)
        {
            var ShipInformation = _ShipInformationRepository.FinedOneInclude(x=>x.ShipInformationId == request.ShipInformationId, "BaseName", "AuthorityNavigation", "BaseSchoolName", "OperationalStatus", "ShipType", "Sqn");
            return _mapper.Map<ShipInformationDto>(ShipInformation);
        }
    }
}

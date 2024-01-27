using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ShipInformationTypes;
using SchoolManagement.Application.Features.ShipTypes.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.ShipTypes.Handlers.Queries
{
    public class GetShipTypeDetailRequestHandler : IRequestHandler<GetShipTypeDetailRequest, ShipTypeDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<ShipType> _ShipTypeRepository;
        public GetShipTypeDetailRequestHandler(ISchoolManagementRepository<ShipType> ShipTypeRepository, IMapper mapper)
        {
            _ShipTypeRepository = ShipTypeRepository;
            _mapper = mapper;
        }
        public async Task<ShipTypeDto> Handle(GetShipTypeDetailRequest request, CancellationToken cancellationToken)
        {
            var ShipType = await _ShipTypeRepository.Get(request.ShipTypeId);
            return _mapper.Map<ShipTypeDto>(ShipType);
        }
    }
}

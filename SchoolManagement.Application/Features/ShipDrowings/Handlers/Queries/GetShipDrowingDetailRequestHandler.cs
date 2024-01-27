using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ShipDrowings;
using SchoolManagement.Application.Features.ShipDrowings.Requests.Queries;

namespace SchoolManagement.Application.Features.ShipDrowings.Handlers.Queries
{
    public class GetShipDrowingDetailRequestHandler : IRequestHandler<GetShipDrowingDetailRequest, ShipDrowingDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<SchoolManagement.Domain.ShipDrowing> _ShipDrowingRepository;
        public GetShipDrowingDetailRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.ShipDrowing> ShipDrowingRepository, IMapper mapper)
        {
            _ShipDrowingRepository = ShipDrowingRepository;
            _mapper = mapper;
        }
        public async Task<ShipDrowingDto> Handle(GetShipDrowingDetailRequest request, CancellationToken cancellationToken)
        {
            var ShipDrowing =  _ShipDrowingRepository.FinedOneInclude(x => x.ShipDrowingId == request.ShipDrowingId, "Authority", "BaseName", "BaseSchoolName");
            return _mapper.Map<ShipDrowingDto>(ShipDrowing);
        }
    }
}

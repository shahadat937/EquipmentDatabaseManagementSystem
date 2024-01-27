using AutoMapper;
using SchoolManagement.Application.DTOs.ShipInformations;
using SchoolManagement.Application.Features.ShipInformations.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using MediatR;
using SchoolManagement.Domain;
using SchoolManagement.Application.Features.BaseSchoolNames.Requests.Queries;

namespace SchoolManagement.Application.Features.ShipInformations.Handlers.Queries   
{
    public class GetShipInformationListByShipTypeIdRequestHandler : IRequestHandler<GetShipInformationListByShipTypeIdRequest, List<ShipInformationDto>>
    {

        private readonly ISchoolManagementRepository<ShipInformation> _ShipInformationRepository;

        private readonly IMapper _mapper;

        public GetShipInformationListByShipTypeIdRequestHandler(ISchoolManagementRepository<ShipInformation> ShipInformationRepository, IMapper mapper)
        {
            _ShipInformationRepository = ShipInformationRepository;
            _mapper = mapper;
        }

        public async Task<List<ShipInformationDto>> Handle(GetShipInformationListByShipTypeIdRequest request, CancellationToken cancellationToken)
        {
            IQueryable<ShipInformation> ShipInformations = _ShipInformationRepository.FilterWithInclude(x => x.ShipTypeId == request.ShipTypeId);

            var ShipInformationDtos = _mapper.Map<List<ShipInformationDto>>(ShipInformations);

            return ShipInformationDtos;
        }
    }
}

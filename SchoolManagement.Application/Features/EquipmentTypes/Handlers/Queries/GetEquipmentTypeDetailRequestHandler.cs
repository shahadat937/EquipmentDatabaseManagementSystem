using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.EquipmentType;
using SchoolManagement.Application.Features.EquipmentTypes.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.EquipmentTypes.Handlers.Queries
{
    public class GetEquipmentTypeDetailRequestHandler : IRequestHandler<GetEquipmentTypeDetailRequest, EquipmentTypeDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<EquipmentType> _EquipmentTypeRepository;
        public GetEquipmentTypeDetailRequestHandler(ISchoolManagementRepository<EquipmentType> EquipmentTypeRepository, IMapper mapper)
        {
            _EquipmentTypeRepository = EquipmentTypeRepository;
            _mapper = mapper;
        }
        public async Task<EquipmentTypeDto> Handle(GetEquipmentTypeDetailRequest request, CancellationToken cancellationToken)
        {
            var EquipmentType = await _EquipmentTypeRepository.Get(request.EquipmentTypeId);
            return _mapper.Map<EquipmentTypeDto>(EquipmentType);
        }
    }
}

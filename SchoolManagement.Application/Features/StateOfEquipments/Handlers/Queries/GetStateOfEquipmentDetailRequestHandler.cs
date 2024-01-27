using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.StateOfEquipments;
using SchoolManagement.Application.Features.StateOfEquipments.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.StateOfEquipments.Handlers.Queries
{
    public class GetStateOfEquipmentDetailRequestHandler : IRequestHandler<GetStateOfEquipmentDetailRequest, StateOfEquipmentDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<StateOfEquipment> _StateOfEquipmentRepository;
        public GetStateOfEquipmentDetailRequestHandler(ISchoolManagementRepository<StateOfEquipment> StateOfEquipmentRepository, IMapper mapper)
        {
            _StateOfEquipmentRepository = StateOfEquipmentRepository;
            _mapper = mapper;
        }
        public async Task<StateOfEquipmentDto> Handle(GetStateOfEquipmentDetailRequest request, CancellationToken cancellationToken)
        {
            var StateOfEquipment = await _StateOfEquipmentRepository.Get(request.StateOfEquipmentId);
            return _mapper.Map<StateOfEquipmentDto>(StateOfEquipment);
        }
    }
}

using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.OperationalStates;
using SchoolManagement.Application.DTOs.ShipInformationTypes;
using SchoolManagement.Application.Features.OperationalStates.Requests.Queries;
using SchoolManagement.Doamin;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.OperationalStates.Handlers.Queries
{
    public class GetOperationalStateDetailRequestHandler : IRequestHandler<GetOperationalStateDetailRequest, OperationalStateDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<OperationalState> _OperationalStateRepository;
        public GetOperationalStateDetailRequestHandler(ISchoolManagementRepository<OperationalState> OperationalStateRepository, IMapper mapper)
        {
            _OperationalStateRepository = OperationalStateRepository;
            _mapper = mapper;
        }
        public async Task<OperationalStateDto> Handle(GetOperationalStateDetailRequest request, CancellationToken cancellationToken)
        {
            var OperationalState = await _OperationalStateRepository.Get(request.OperationalStateId);
            return _mapper.Map<OperationalStateDto>(OperationalState);
        }
    }
}

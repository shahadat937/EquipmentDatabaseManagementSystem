using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ProcurementType;
using SchoolManagement.Application.Features.ProcurementTypes.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.ProcurementTypes.Handlers.Queries
{
    public class GetProcurementTypeDetailRequestHandler : IRequestHandler<GetProcurementTypeDetailRequest, ProcurementTypeDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<ProcurementType> _ProcurementTypeRepository;
        public GetProcurementTypeDetailRequestHandler(ISchoolManagementRepository<ProcurementType> ProcurementTypeRepository, IMapper mapper)
        {
            _ProcurementTypeRepository = ProcurementTypeRepository;
            _mapper = mapper;
        }
        public async Task<ProcurementTypeDto> Handle(GetProcurementTypeDetailRequest request, CancellationToken cancellationToken)
        {
            var ProcurementType = await _ProcurementTypeRepository.Get(request.ProcurementTypeId);
            return _mapper.Map<ProcurementTypeDto>(ProcurementType);
        }
    }
}

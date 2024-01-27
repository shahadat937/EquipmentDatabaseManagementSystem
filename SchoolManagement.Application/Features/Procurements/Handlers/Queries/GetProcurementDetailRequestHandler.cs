using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Procurement;
using SchoolManagement.Application.Features.Procurements.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Procurements.Handlers.Queries
{
    public class GetProcurementDetailRequestHandler : IRequestHandler<GetProcurementDetailRequest, ProcurementDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<Procurement> _ProcurementRepository;
        public GetProcurementDetailRequestHandler(ISchoolManagementRepository<Procurement> ProcurementRepository, IMapper mapper)
        {
            _ProcurementRepository = ProcurementRepository;
            _mapper = mapper;
        }
        public async Task<ProcurementDto> Handle(GetProcurementDetailRequest request, CancellationToken cancellationToken)
        {
            var Procurement = await _ProcurementRepository.Get(request.ProcurementId);
            return _mapper.Map<ProcurementDto>(Procurement);
        }
    }
}

using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ProcurementMethod;
using SchoolManagement.Application.Features.ProcurementMethods.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.ProcurementMethods.Handlers.Queries
{
    public class GetProcurementMethodDetailRequestHandler : IRequestHandler<GetProcurementMethodDetailRequest, ProcurementMethodDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<ProcurementMethod> _ProcurementMethodRepository;
        public GetProcurementMethodDetailRequestHandler(ISchoolManagementRepository<ProcurementMethod> ProcurementMethodRepository, IMapper mapper)
        {
            _ProcurementMethodRepository = ProcurementMethodRepository;
            _mapper = mapper;
        }
        public async Task<ProcurementMethodDto> Handle(GetProcurementMethodDetailRequest request, CancellationToken cancellationToken)
        {
            var ProcurementMethod = await _ProcurementMethodRepository.Get(request.ProcurementMethodId);
            return _mapper.Map<ProcurementMethodDto>(ProcurementMethod);
        }
    }
}

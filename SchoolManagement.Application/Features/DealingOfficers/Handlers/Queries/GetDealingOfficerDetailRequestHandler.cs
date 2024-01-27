using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.DealingOfficer;
using SchoolManagement.Application.Features.DealingOfficers.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.DealingOfficers.Handlers.Queries
{
    public class GetDealingOfficerDetailRequestHandler : IRequestHandler<GetDealingOfficerDetailRequest, DealingOfficerDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<DealingOfficer> _DealingOfficerRepository;
        public GetDealingOfficerDetailRequestHandler(ISchoolManagementRepository<DealingOfficer> DealingOfficerRepository, IMapper mapper)
        {
            _DealingOfficerRepository = DealingOfficerRepository;
            _mapper = mapper;
        }
        public async Task<DealingOfficerDto> Handle(GetDealingOfficerDetailRequest request, CancellationToken cancellationToken)
        {
            var DealingOfficer = await _DealingOfficerRepository.Get(request.DealingOfficerId);
            return _mapper.Map<DealingOfficerDto>(DealingOfficer);
        }
    }
}

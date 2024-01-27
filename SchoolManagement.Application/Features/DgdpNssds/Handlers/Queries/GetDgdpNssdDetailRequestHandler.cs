using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.DgdpNssd;
using SchoolManagement.Application.Features.DgdpNssds.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.DgdpNssds.Handlers.Queries
{
    public class GetDgdpNssdDetailRequestHandler : IRequestHandler<GetDgdpNssdDetailRequest, DgdpNssdDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<DgdpNssd> _DgdpNssdRepository;
        public GetDgdpNssdDetailRequestHandler(ISchoolManagementRepository<DgdpNssd> DgdpNssdRepository, IMapper mapper)
        {
            _DgdpNssdRepository = DgdpNssdRepository;
            _mapper = mapper;
        }
        public async Task<DgdpNssdDto> Handle(GetDgdpNssdDetailRequest request, CancellationToken cancellationToken)
        {
            var DgdpNssd = await _DgdpNssdRepository.Get(request.DgdpNssdId);
            return _mapper.Map<DgdpNssdDto>(DgdpNssd);
        }
    }
}

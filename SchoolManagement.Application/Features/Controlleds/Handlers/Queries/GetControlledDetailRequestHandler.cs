using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Controlled;
using SchoolManagement.Application.Features.Controlleds.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Controlleds.Handlers.Queries
{
    public class GetControlledDetailRequestHandler : IRequestHandler<GetControlledDetailRequest, ControlledDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<Controlled> _ControlledRepository;
        public GetControlledDetailRequestHandler(ISchoolManagementRepository<Controlled> ControlledRepository, IMapper mapper)
        {
            _ControlledRepository = ControlledRepository;
            _mapper = mapper;
        }
        public async Task<ControlledDto> Handle(GetControlledDetailRequest request, CancellationToken cancellationToken)
        {
            var Controlled = await _ControlledRepository.Get(request.ControlledId);
            return _mapper.Map<ControlledDto>(Controlled);
        }
    }
}

using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ActionTaken;
using SchoolManagement.Application.Features.ActionTakens.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.ActionTakens.Handlers.Queries
{
    public class GetActionTakenDetailRequestHandler : IRequestHandler<GetActionTakenDetailRequest, ActionTakenDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<ActionTaken> _ActionTakenRepository;
        public GetActionTakenDetailRequestHandler(ISchoolManagementRepository<ActionTaken> ActionTakenRepository, IMapper mapper)
        {
            _ActionTakenRepository = ActionTakenRepository;
            _mapper = mapper;
        }
        public async Task<ActionTakenDto> Handle(GetActionTakenDetailRequest request, CancellationToken cancellationToken)
        {
            var ActionTaken = await _ActionTakenRepository.Get(request.ActionTakenId);
            return _mapper.Map<ActionTakenDto>(ActionTaken);
        }
    }
}

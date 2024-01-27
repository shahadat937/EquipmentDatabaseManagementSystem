using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.DailyWorkState;
using SchoolManagement.Application.Features.DailyWorkStates.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.DailyWorkStates.Handlers.Queries
{
    public class GetDailyWorkStateDetailRequestHandler : IRequestHandler<GetDailyWorkStateDetailRequest, DailyWorkStateDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<DailyWorkState> _DailyWorkStateRepository;
        public GetDailyWorkStateDetailRequestHandler(ISchoolManagementRepository<DailyWorkState> DailyWorkStateRepository, IMapper mapper)
        {
            _DailyWorkStateRepository = DailyWorkStateRepository;
            _mapper = mapper;
        }
        public async Task<DailyWorkStateDto> Handle(GetDailyWorkStateDetailRequest request, CancellationToken cancellationToken)
        {
            var DailyWorkState = await _DailyWorkStateRepository.Get(request.DailyWorkStateId);
            return _mapper.Map<DailyWorkStateDto>(DailyWorkState);
        }
    }
}

using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.RunningTimes;
using SchoolManagement.Application.Features.RunningTimes.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.RunningTimes.Handlers.Queries
{
    public class GetRunningTimeDetailRequestHandler : IRequestHandler<GetRunningTimeDetailRequest, RunningTimeDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<RunningTime> _RunningTimeRepository;
        public GetRunningTimeDetailRequestHandler(ISchoolManagementRepository<RunningTime> RunningTimeRepository, IMapper mapper)
        {
            _RunningTimeRepository = RunningTimeRepository;
            _mapper = mapper;
        }
        public async Task<RunningTimeDto> Handle(GetRunningTimeDetailRequest request, CancellationToken cancellationToken)
        {
            var RunningTime = await _RunningTimeRepository.Get(request.RunningTimeId);
            return _mapper.Map<RunningTimeDto>(RunningTime);
        }
    }
}

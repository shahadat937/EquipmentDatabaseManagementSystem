using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.HalfYearlyRunningTime;
using SchoolManagement.Application.Features.HalfYearlyRunningTimes.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.HalfYearlyRunningTimes.Handlers.Queries
{
    public class GetHalfYearlyRunningTimeDetailRequestHandler : IRequestHandler<GetHalfYearlyRunningTimeDetailRequest, HalfYearlyRunningTimeDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<HalfYearlyRunningTime> _HalfYearlyRunningTimeRepository;
        public GetHalfYearlyRunningTimeDetailRequestHandler(ISchoolManagementRepository<HalfYearlyRunningTime> HalfYearlyRunningTimeRepository, IMapper mapper)
        {
            _HalfYearlyRunningTimeRepository = HalfYearlyRunningTimeRepository;
            _mapper = mapper;
        }
        public async Task<HalfYearlyRunningTimeDto> Handle(GetHalfYearlyRunningTimeDetailRequest request, CancellationToken cancellationToken)
        {
            var HalfYearlyRunningTime = await _HalfYearlyRunningTimeRepository.Get(request.HalfYearlyRunningTimeId);
            return _mapper.Map<HalfYearlyRunningTimeDto>(HalfYearlyRunningTime);
        }
    }
}

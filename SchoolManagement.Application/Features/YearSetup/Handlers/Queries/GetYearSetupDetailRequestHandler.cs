using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.YearSetup;
using SchoolManagement.Application.Features.YearSetups.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.YearSetups.Handlers.Queries
{
    public class GetYearSetupDetailRequestHandler : IRequestHandler<GetYearSetupDetailRequest, YearSetupDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<YearSetup> _YearSetupRepository;
        public GetYearSetupDetailRequestHandler(ISchoolManagementRepository<YearSetup> YearSetupRepository, IMapper mapper)
        {
            _YearSetupRepository = YearSetupRepository;
            _mapper = mapper;
        }
        public async Task<YearSetupDto> Handle(GetYearSetupDetailRequest request, CancellationToken cancellationToken)
        {
            var YearSetup = await _YearSetupRepository.Get(request.YearSetupId);
            return _mapper.Map<YearSetupDto>(YearSetup);
        }
    }
}

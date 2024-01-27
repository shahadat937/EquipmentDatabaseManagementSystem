using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.HalfYearlyReturn;
using SchoolManagement.Application.Features.HalfYearlyReturns.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.HalfYearlyReturns.Handlers.Queries
{
    public class GetHalfYearlyReturnDetailRequestHandler : IRequestHandler<GetHalfYearlyReturnDetailRequest, HalfYearlyReturnDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<HalfYearlyReturn> _HalfYearlyReturnRepository;
        public GetHalfYearlyReturnDetailRequestHandler(ISchoolManagementRepository<HalfYearlyReturn> HalfYearlyReturnRepository, IMapper mapper)
        {
            _HalfYearlyReturnRepository = HalfYearlyReturnRepository;
            _mapper = mapper;
        }
        public async Task<HalfYearlyReturnDto> Handle(GetHalfYearlyReturnDetailRequest request, CancellationToken cancellationToken)
        {
            var HalfYearlyReturn = await _HalfYearlyReturnRepository.Get(request.HalfYearlyReturnId);
            return _mapper.Map<HalfYearlyReturnDto>(HalfYearlyReturn);
        }
    }
}

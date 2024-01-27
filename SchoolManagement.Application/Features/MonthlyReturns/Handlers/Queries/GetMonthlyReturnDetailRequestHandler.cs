using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.MonthlyReturns;
using SchoolManagement.Application.Features.MonthlyReturns.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.MonthlyReturns.Handlers.Queries
{
    public class GetMonthlyReturnDetailRequestHandler : IRequestHandler<GetMonthlyReturnDetailRequest, MonthlyReturnDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<MonthlyReturn> _MonthlyReturnRepository;
        public GetMonthlyReturnDetailRequestHandler(ISchoolManagementRepository<MonthlyReturn> MonthlyReturnRepository, IMapper mapper)
        {
            _MonthlyReturnRepository = MonthlyReturnRepository;
            _mapper = mapper;
        }
        public async Task<MonthlyReturnDto> Handle(GetMonthlyReturnDetailRequest request, CancellationToken cancellationToken)
        {
            var MonthlyReturn = await _MonthlyReturnRepository.Get(request.MonthlyReturnId);
            return _mapper.Map<MonthlyReturnDto>(MonthlyReturn);
        }
    }
}

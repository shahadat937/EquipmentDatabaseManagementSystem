using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.FinancialYears;
using SchoolManagement.Application.Features.FinancialYears.Requests.Queries;
using SchoolManagement.Application.Features.YearSetups.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.FinancialYearss.Handlers.Queries
{
    public class GetFinancialYearsDetailRequestHandler : IRequestHandler<GetFinancialYearsDetailRequest, FinancialYearDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<SchoolManagement.Domain.FinancialYear> _FinancialYearsRepository;
        public GetFinancialYearsDetailRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.FinancialYear> FinancialYearsRepository, IMapper mapper)
        {
            _FinancialYearsRepository = FinancialYearsRepository;
            _mapper = mapper;
        }
        public async Task<FinancialYearDto> Handle(GetFinancialYearsDetailRequest request, CancellationToken cancellationToken)
        {
            var FinancialYears = await _FinancialYearsRepository.Get(request.FinancialYearId);
            return _mapper.Map<FinancialYearDto>(FinancialYears);
        }
    }
}

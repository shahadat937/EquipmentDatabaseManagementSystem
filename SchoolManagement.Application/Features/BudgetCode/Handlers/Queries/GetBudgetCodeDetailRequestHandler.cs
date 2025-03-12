using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.BudgetCode;
using SchoolManagement.Application.Features.BudgetCode.Requests.Queries;
using SchoolManagement.Application.Features.YearSetups.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.BudgetCodes.Handlers.Queries
{
    public class GetBudgetCodeDetailRequestHandler : IRequestHandler<GetBudgetCodeDetailRequest, BudgetCodeDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<SchoolManagement.Domain.BudgetCode> _BudgetCodeRepository;
        public GetBudgetCodeDetailRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.BudgetCode> BudgetCodeRepository, IMapper mapper)
        {
            _BudgetCodeRepository = BudgetCodeRepository;
            _mapper = mapper;
        }
        public async Task<BudgetCodeDto> Handle(GetBudgetCodeDetailRequest request, CancellationToken cancellationToken)
        {
            var BudgetCode = await _BudgetCodeRepository.Get(request.BudgetCodeId);
            return _mapper.Map<BudgetCodeDto>(BudgetCode);
        }
    }
}

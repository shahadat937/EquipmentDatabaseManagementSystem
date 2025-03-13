using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Budget;
using SchoolManagement.Application.Features.Budget.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Budget.Handlers.Queries
{
    public class GetBudgetDetailRequestHandler : IRequestHandler<GetBudgetDetailRequest, BudgetDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<SchoolManagement.Domain.Budget> _BudgetRepository;
        private readonly ISchoolManagementRepository<BudgetBaseSchoolName> _BudgetBaseSchoolName;
        public GetBudgetDetailRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.Budget> BudgetRepository, IMapper mapper, ISchoolManagementRepository<BudgetBaseSchoolName> BudgetBaseSchoolName)
        {
            _BudgetRepository = BudgetRepository;
            _mapper = mapper;
            _BudgetBaseSchoolName = BudgetBaseSchoolName;

        }
        public async Task<BudgetDto> Handle(GetBudgetDetailRequest request, CancellationToken cancellationToken)
        {
            var Budget = await _BudgetRepository.Get(request.BudgetId);

            // Fetch only the BaseSchoolNameId values
            var baseSchoolIds = await _BudgetBaseSchoolName
                                 .Where(x => x.BudgetId == request.BudgetId)
                                 .Select(x => x.BaseSchoolNameId)
                                 .ToListAsync();


            var BudgetDto = _mapper.Map<BudgetDto>(Budget);

            BudgetDto.BaseSchoolNameId =  baseSchoolIds;
           

            return BudgetDto;
        }


    }
}

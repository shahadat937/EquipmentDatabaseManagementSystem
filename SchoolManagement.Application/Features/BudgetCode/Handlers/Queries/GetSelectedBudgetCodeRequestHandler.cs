using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.BudgetCode.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.BudgetCodes.Handlers.Queries
{
    public class GetSelectedBudgetCodeRequestHandler : IRequestHandler<GetSelectedBudgetCodeRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<SchoolManagement.Domain.BudgetCode> _BudgetCodeRepository;


        public GetSelectedBudgetCodeRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.BudgetCode> BudgetCodeRepository)
        {
            _BudgetCodeRepository = BudgetCodeRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedBudgetCodeRequest request, CancellationToken cancellationToken)
        {
            ICollection<SchoolManagement.Domain.BudgetCode> BudgetCode = await _BudgetCodeRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = BudgetCode.Select(x => new SelectedModel
            {
                Text = x.BudgetCodeName,
                Value = x.BudgetCodeId
            }).ToList();
            return selectModels;
        }
    }
}

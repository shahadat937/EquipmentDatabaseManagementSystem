using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.MonthlyReturns.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.MonthlyReturns.Handlers.Queries
{
    public class GetSelectedMonthlyReturnRequestHandler : IRequestHandler<GetSelectedMonthlyReturnRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<MonthlyReturn> _MonthlyReturnRepository;


        public GetSelectedMonthlyReturnRequestHandler(ISchoolManagementRepository<MonthlyReturn> MonthlyReturnRepository)
        {
            _MonthlyReturnRepository = MonthlyReturnRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedMonthlyReturnRequest request, CancellationToken cancellationToken)
        {
            ICollection<MonthlyReturn> codeValues = await _MonthlyReturnRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.MonthlyReturnId,
                Value = x.MonthlyReturnId
            }).ToList();
            return selectModels;
        }
    }
}

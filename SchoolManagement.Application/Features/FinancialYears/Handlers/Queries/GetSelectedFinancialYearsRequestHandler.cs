using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.FinancialYears.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.YearSetups.Handlers.Queries
{
    public class GetSelectedFinancialYearsRequestHandler : IRequestHandler<GetSelectedFinancialYearsRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<SchoolManagement.Domain.FinancialYears> _YearSetupRepository;


        public GetSelectedFinancialYearsRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.FinancialYears> YearSetupRepository)
        {
            _YearSetupRepository = YearSetupRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedFinancialYearsRequest request, CancellationToken cancellationToken)
        {
            ICollection<SchoolManagement.Domain.FinancialYears> yearSetup = await _YearSetupRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = yearSetup.Select(x => new SelectedModel
            {
                Text = x.FinancialYearName,
                Value = x.FinancialYearId
            }).ToList();
            return selectModels;
        }
    }
}

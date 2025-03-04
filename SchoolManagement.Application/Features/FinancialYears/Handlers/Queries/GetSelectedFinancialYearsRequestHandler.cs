using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.FinancialYears.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.YearSetups.Handlers.Queries
{
    public class GetSelectedFinancialYearsRequestHandler : IRequestHandler<GetSelectedFinancialYearsRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<SchoolManagement.Domain.FinancialYear> _YearSetupRepository;


        public GetSelectedFinancialYearsRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.FinancialYear> YearSetupRepository)
        {
            _YearSetupRepository = YearSetupRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedFinancialYearsRequest request, CancellationToken cancellationToken)
        {
            ICollection<SchoolManagement.Domain.FinancialYear> yearSetup = await _YearSetupRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = yearSetup.Select(x => new SelectedModel
            {
                Text = x.FinancialYearName,
                Value = x.FinancialYearId
            }).ToList();
            return selectModels;
        }
    }
}

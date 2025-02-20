using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.YearSetups.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.YearSetups.Handlers.Queries
{
    public class GetSelectedReportingYearRequestHandler : IRequestHandler<GetSelectedReportingYearRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<ReportingYear> _YearSetupRepository;


        public GetSelectedReportingYearRequestHandler(ISchoolManagementRepository<ReportingYear> YearSetupRepository)
        {
            _YearSetupRepository = YearSetupRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedReportingYearRequest request, CancellationToken cancellationToken)
        {
            ICollection<ReportingYear> yearSetup = await _YearSetupRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = yearSetup.Select(x => new SelectedModel
            {
                Text = x.Year,
                Value = x.ReportingYearId
            }).ToList();
            return selectModels;
        }
    }
}

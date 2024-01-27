using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.ReportingMonths.Requests.Queries;
using SchoolManagement.Doamin;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ReportingMonths.Handlers.Queries
{
    public class GetSelectedReportingMonthRequestHandler : IRequestHandler<GetSelectedReportingMonthRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<ReportingMonth> _ReportingMonthRepository;


        public GetSelectedReportingMonthRequestHandler(ISchoolManagementRepository<ReportingMonth> ReportingMonthRepository)
        {
            _ReportingMonthRepository = ReportingMonthRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedReportingMonthRequest request, CancellationToken cancellationToken)
        {
            ICollection<ReportingMonth> codeValues = await _ReportingMonthRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.ReportingMonthId
            }).ToList();
            return selectModels;
        }
    }
}

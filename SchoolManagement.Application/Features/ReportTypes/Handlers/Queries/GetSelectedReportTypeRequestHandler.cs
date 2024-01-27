using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.ReportTypes.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ReportTypes.Handlers.Queries
{
    public class GetSelectedReportTypeRequestHandler : IRequestHandler<GetSelectedReportTypeRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<ReportType> _ReportTypeRepository;


        public GetSelectedReportTypeRequestHandler(ISchoolManagementRepository<ReportType> ReportTypeRepository)
        {
            _ReportTypeRepository = ReportTypeRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedReportTypeRequest request, CancellationToken cancellationToken)
        {
            ICollection<ReportType> codeValues = await _ReportTypeRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.ReportTypeId
            }).ToList();
            return selectModels;
        }
    }
}

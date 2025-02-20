using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.YearSetups.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.YearSetups.Handlers.Queries
{
    public class GetSelectedYearSetupRequestHandler : IRequestHandler<GetSelectedYearSetupRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<YearSetup> _YearSetupRepository;


        public GetSelectedYearSetupRequestHandler(ISchoolManagementRepository<YearSetup> YearSetupRepository)
        {
            _YearSetupRepository = YearSetupRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedYearSetupRequest request, CancellationToken cancellationToken)
        {
            ICollection<YearSetup> yearSetup = await _YearSetupRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = yearSetup.Select(x => new SelectedModel
            {
                Text = x.Year,
                Value = x.YearId
            }).ToList();
            return selectModels;
        }
    }
}

using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.HalfYearlyRunningTimes.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.HalfYearlyRunningTimes.Handlers.Queries
{
    public class GetSelectedHalfYearlyRunningTimeRequestHandler : IRequestHandler<GetSelectedHalfYearlyRunningTimeRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<HalfYearlyRunningTime> _HalfYearlyRunningTimeRepository;


        public GetSelectedHalfYearlyRunningTimeRequestHandler(ISchoolManagementRepository<HalfYearlyRunningTime> HalfYearlyRunningTimeRepository)
        {
            _HalfYearlyRunningTimeRepository = HalfYearlyRunningTimeRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedHalfYearlyRunningTimeRequest request, CancellationToken cancellationToken)
        {
            ICollection<HalfYearlyRunningTime> codeValues = await _HalfYearlyRunningTimeRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.HalfYearlyRunningTimeId
            }).ToList();
            return selectModels;
        }
    }
}

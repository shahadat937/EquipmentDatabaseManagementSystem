using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.RunningTimes.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.RunningTimes.Handlers.Queries
{
    public class GetSelectedRunningTimeRequestHandler : IRequestHandler<GetSelectedRunningTimeRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<RunningTime> _RunningTimeRepository;


        public GetSelectedRunningTimeRequestHandler(ISchoolManagementRepository<RunningTime> RunningTimeRepository)
        {
            _RunningTimeRepository = RunningTimeRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedRunningTimeRequest request, CancellationToken cancellationToken)
        {
            ICollection<RunningTime> codeValues = await _RunningTimeRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.RunningTimeId
            }).ToList();
            return selectModels;
        }
    }
}

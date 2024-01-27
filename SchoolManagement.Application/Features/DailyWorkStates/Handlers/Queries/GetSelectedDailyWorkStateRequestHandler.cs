using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.DailyWorkStates.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.DailyWorkStates.Handlers.Queries
{
    public class GetSelectedDailyWorkStateRequestHandler : IRequestHandler<GetSelectedDailyWorkStateRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<DailyWorkState> _DailyWorkStateRepository;


        public GetSelectedDailyWorkStateRequestHandler(ISchoolManagementRepository<DailyWorkState> DailyWorkStateRepository)
        {
            _DailyWorkStateRepository = DailyWorkStateRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedDailyWorkStateRequest request, CancellationToken cancellationToken)
        {
            ICollection<DailyWorkState> codeValues = await _DailyWorkStateRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Subject,
                Value = x.DailyWorkStateId
            }).ToList();
            return selectModels;
        }
    }
}

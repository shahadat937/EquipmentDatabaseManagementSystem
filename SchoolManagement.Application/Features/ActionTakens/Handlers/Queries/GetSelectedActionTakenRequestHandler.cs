using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.ActionTakens.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ActionTakens.Handlers.Queries
{
    public class GetSelectedActionTakenRequestHandler : IRequestHandler<GetSelectedActionTakenRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<ActionTaken> _ActionTakenRepository;


        public GetSelectedActionTakenRequestHandler(ISchoolManagementRepository<ActionTaken> ActionTakenRepository)
        {
            _ActionTakenRepository = ActionTakenRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedActionTakenRequest request, CancellationToken cancellationToken)
        {
            ICollection<ActionTaken> codeValues = await _ActionTakenRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.ActionTakenId
            }).ToList();
            return selectModels;
        }
    }
}

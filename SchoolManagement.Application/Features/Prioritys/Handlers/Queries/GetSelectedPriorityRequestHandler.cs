using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Prioritys.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Prioritys.Handlers.Queries
{
    public class GetSelectedPriorityRequestHandler : IRequestHandler<GetSelectedPriorityRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<Priority> _PriorityRepository;


        public GetSelectedPriorityRequestHandler(ISchoolManagementRepository<Priority> PriorityRepository)
        {
            _PriorityRepository = PriorityRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedPriorityRequest request, CancellationToken cancellationToken)
        {
            ICollection<Priority> codeValues = await _PriorityRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.PriorityId
            }).ToList();
            return selectModels;
        }
    }
}

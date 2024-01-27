using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.GroupNames.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.GroupNames.Handlers.Queries
{
    public class GetSelectedGroupNameRequestHandler : IRequestHandler<GetSelectedGroupNameRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<GroupName> _GroupNameRepository;


        public GetSelectedGroupNameRequestHandler(ISchoolManagementRepository<GroupName> GroupNameRepository)
        {
            _GroupNameRepository = GroupNameRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedGroupNameRequest request, CancellationToken cancellationToken)
        {
            ICollection<GroupName> codeValues = await _GroupNameRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.GroupNameId
            }).ToList();
            return selectModels;
        }
    }
}

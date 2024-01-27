using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.ProjectStatuses.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ProjectStatuses.Handlers.Queries
{
    public class GetSelectedProjectStatusRequestHandler : IRequestHandler<GetSelectedProjectStatusRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<ProjectStatus> _ProjectStatusRepository;


        public GetSelectedProjectStatusRequestHandler(ISchoolManagementRepository<ProjectStatus> ProjectStatusRepository)
        {
            _ProjectStatusRepository = ProjectStatusRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedProjectStatusRequest request, CancellationToken cancellationToken)
        {
            ICollection<ProjectStatus> codeValues = await _ProjectStatusRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.ProjectStatusId
            }).ToList();
            return selectModels;
        }
    }
}

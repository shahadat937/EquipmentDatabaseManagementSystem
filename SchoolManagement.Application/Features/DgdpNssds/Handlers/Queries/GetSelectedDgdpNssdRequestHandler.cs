using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.DgdpNssds.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.DgdpNssds.Handlers.Queries
{
    public class GetSelectedDgdpNssdRequestHandler : IRequestHandler<GetSelectedDgdpNssdRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<DgdpNssd> _DgdpNssdRepository;


        public GetSelectedDgdpNssdRequestHandler(ISchoolManagementRepository<DgdpNssd> DgdpNssdRepository)
        {
            _DgdpNssdRepository = DgdpNssdRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedDgdpNssdRequest request, CancellationToken cancellationToken)
        {
            ICollection<DgdpNssd> codeValues = await _DgdpNssdRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.DgdpNssdId
            }).ToList();
            return selectModels;
        }
    }
}

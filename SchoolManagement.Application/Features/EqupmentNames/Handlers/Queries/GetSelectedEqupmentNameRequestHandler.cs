using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.EqupmentNames.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.EqupmentNames.Handlers.Queries
{
    public class GetSelectedEqupmentNameRequestHandler : IRequestHandler<GetSelectedEqupmentNameRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<EqupmentName> _EqupmentNameRepository;


        public GetSelectedEqupmentNameRequestHandler(ISchoolManagementRepository<EqupmentName> EqupmentNameRepository)
        {
            _EqupmentNameRepository = EqupmentNameRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedEqupmentNameRequest request, CancellationToken cancellationToken)
        {
            ICollection<EqupmentName> codeValues = await _EqupmentNameRepository.FilterAsync(x => x.IsActive);
            codeValues.OrderBy(x => x.MenuPosition);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.EqupmentNameId
            }).ToList();
            return selectModels;
        }
    }
}

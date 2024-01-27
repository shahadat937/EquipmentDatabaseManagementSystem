using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.EqupmentNames.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.EqupmentNames.Handlers.Queries
{
    public class GetSelectedEqupmentNameByCategoryRequestHandler : IRequestHandler<GetSelectedEqupmentNameByCategoryRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<EqupmentName> _EqupmentNameRepository;


        public GetSelectedEqupmentNameByCategoryRequestHandler(ISchoolManagementRepository<EqupmentName> EqupmentNameRepository)
        {
            _EqupmentNameRepository = EqupmentNameRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedEqupmentNameByCategoryRequest request, CancellationToken cancellationToken)
        {
            ICollection<EqupmentName> equipmentNames = _EqupmentNameRepository.Where(x => x.EquipmentCategoryId == request.EqepmentCategoryId).ToList();
            List<SelectedModel> selectModels = equipmentNames.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.EqupmentNameId
            }).ToList();
            return selectModels;
        }
    }
}

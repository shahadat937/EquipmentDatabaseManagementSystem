using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.EquipmentTypes.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.EquipmentTypes.Handlers.Queries
{
    public class GetSelectedEquipmentTypeRequestHandler : IRequestHandler<GetSelectedEquipmentTypeRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<EquipmentType> _EquipmentTypeRepository;


        public GetSelectedEquipmentTypeRequestHandler(ISchoolManagementRepository<EquipmentType> EquipmentTypeRepository)
        {
            _EquipmentTypeRepository = EquipmentTypeRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedEquipmentTypeRequest request, CancellationToken cancellationToken)
        {
            ICollection<EquipmentType> codeValues = await _EquipmentTypeRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.EquipmentTypeId
            }).ToList();
            return selectModels;
        }
    }
}

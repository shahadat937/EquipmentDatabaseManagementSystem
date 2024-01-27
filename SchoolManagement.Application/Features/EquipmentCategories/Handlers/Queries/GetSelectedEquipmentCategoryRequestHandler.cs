using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.EquipmentCategorys.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;
using System.Security.Cryptography.X509Certificates;

namespace SchoolManagement.Application.Features.EquipmentCategorys.Handlers.Queries
{
    public class GetSelectedEquipmentCategoryRequestHandler : IRequestHandler<GetSelectedEquipmentCategoryRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<EquipmentCategory> _EquipmentCategoryRepository;


        public GetSelectedEquipmentCategoryRequestHandler(ISchoolManagementRepository<EquipmentCategory> EquipmentCategoryRepository)
        {
            _EquipmentCategoryRepository = EquipmentCategoryRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedEquipmentCategoryRequest request, CancellationToken cancellationToken)
        {
            ICollection<EquipmentCategory> codeValues = await _EquipmentCategoryRepository.FilterAsync(x => x.IsActive);
            codeValues.OrderBy(x => x.MenuPosition);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.EquipmentCategoryId
            }).ToList();
            return selectModels;
        }
    }
}

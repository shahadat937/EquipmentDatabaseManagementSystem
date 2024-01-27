using MediatR;
using SchoolManagement.Application.DTOs.EquipmentCategorys;

namespace SchoolManagement.Application.Features.EquipmentCategorys.Requests.Commands
{
    public class UpdateEquipmentCategoryCommand : IRequest<Unit>
    { 
        public EquipmentCategoryDto EquipmentCategoryDto { get; set; }
    }
}
 
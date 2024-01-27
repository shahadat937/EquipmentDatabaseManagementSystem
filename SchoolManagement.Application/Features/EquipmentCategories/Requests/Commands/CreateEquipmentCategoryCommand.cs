using MediatR;
using SchoolManagement.Application.DTOs.EquipmentCategorys;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.EquipmentCategorys.Requests.Commands
{
    public class CreateEquipmentCategoryCommand : IRequest<BaseCommandResponse>
    {
        public CreateEquipmentCategoryDto EquipmentCategoryDto { get; set; }
    }
}

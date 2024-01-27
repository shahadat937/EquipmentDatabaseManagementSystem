using MediatR;
using SchoolManagement.Application.DTOs.EquipmentType;

namespace SchoolManagement.Application.Features.EquipmentTypes.Requests.Commands
{
    public class UpdateEquipmentTypeCommand : IRequest<Unit>
    { 
        public EquipmentTypeDto EquipmentTypeDto { get; set; }
    }
}
 
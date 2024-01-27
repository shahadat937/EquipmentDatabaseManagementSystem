using MediatR;
using SchoolManagement.Application.DTOs.EquipmentType;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.EquipmentTypes.Requests.Commands
{
    public class CreateEquipmentTypeCommand : IRequest<BaseCommandResponse>
    {
        public CreateEquipmentTypeDto EquipmentTypeDto { get; set; }
    }
}

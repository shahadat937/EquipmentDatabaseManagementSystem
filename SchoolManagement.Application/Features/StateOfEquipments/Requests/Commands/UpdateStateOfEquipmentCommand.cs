using MediatR;
using SchoolManagement.Application.DTOs.StateOfEquipments;

namespace SchoolManagement.Application.Features.StateOfEquipments.Requests.Commands
{
    public class UpdateStateOfEquipmentCommand : IRequest<Unit>
    { 
        public StateOfEquipmentDto StateOfEquipmentDto { get; set; }
    }
}
 
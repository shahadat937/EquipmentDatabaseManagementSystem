using MediatR;
using SchoolManagement.Application.DTOs.StateOfEquipments;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.StateOfEquipments.Requests.Commands
{
    public class CreateStateOfEquipmentCommand : IRequest<BaseCommandResponse>
    {
        public CreateStateOfEquipmentDto StateOfEquipmentDto { get; set; }
    }
}

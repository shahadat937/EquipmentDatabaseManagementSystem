using MediatR;

namespace SchoolManagement.Application.Features.StateOfEquipments.Requests.Commands
{
    public class DeleteStateOfEquipmentCommand : IRequest
    {
        public int StateOfEquipmentId { get; set; }
    }
} 

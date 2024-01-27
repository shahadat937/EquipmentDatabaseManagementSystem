using MediatR;

namespace SchoolManagement.Application.Features.EquipmentTypes.Requests.Commands
{
    public class DeleteEquipmentTypeCommand : IRequest
    {
        public int EquipmentTypeId { get; set; }
    }
} 

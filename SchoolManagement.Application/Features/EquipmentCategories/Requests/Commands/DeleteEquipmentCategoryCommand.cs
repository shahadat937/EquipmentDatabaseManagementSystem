using MediatR;

namespace SchoolManagement.Application.Features.EquipmentCategorys.Requests.Commands
{
    public class DeleteEquipmentCategoryCommand : IRequest
    {
        public int EquipmentCategoryId { get; set; }
    }
} 

using MediatR;
using SchoolManagement.Application.DTOs.EquipmentCategorys;

namespace SchoolManagement.Application.Features.EquipmentCategorys.Requests.Queries
{
    public class GetEquipmentCategoryDetailRequest : IRequest<EquipmentCategoryDto>
    {
        public int EquipmentCategoryId { get; set; }
    }
}

using MediatR;
using SchoolManagement.Application.DTOs.EquipmentType;

namespace SchoolManagement.Application.Features.EquipmentTypes.Requests.Queries
{
    public class GetEquipmentTypeDetailRequest : IRequest<EquipmentTypeDto>
    {
        public int EquipmentTypeId { get; set; }
    }
}

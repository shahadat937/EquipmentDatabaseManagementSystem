using MediatR;
using SchoolManagement.Application.DTOs.StateOfEquipments;

namespace SchoolManagement.Application.Features.StateOfEquipments.Requests.Queries
{
    public class GetStateOfEquipmentDetailRequest : IRequest<StateOfEquipmentDto>
    {
        public int StateOfEquipmentId { get; set; }
    }
}

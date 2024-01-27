using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ShipEquipmentInfos.Requests.Queries
{
    public class GetSelectedShipEquipmentInfoRequest : IRequest<List<SelectedModel>>
    {
    }
} 

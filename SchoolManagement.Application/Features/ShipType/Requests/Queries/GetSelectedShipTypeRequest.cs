using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ShipTypes.Requests.Queries
{
    public class GetSelectedShipTypeRequest : IRequest<List<SelectedModel>>
    {
    }
} 

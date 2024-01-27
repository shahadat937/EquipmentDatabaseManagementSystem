using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ShipDrowings.Requests.Queries
{
    public class GetSelectedShipDrowingRequest : IRequest<List<SelectedModel>>
    {
    }
} 

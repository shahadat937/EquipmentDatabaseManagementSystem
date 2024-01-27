using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ShipInformations.Requests.Queries
{
    public class GetSelectedShipInformationRequest : IRequest<List<SelectedModel>>
    {
    }
} 

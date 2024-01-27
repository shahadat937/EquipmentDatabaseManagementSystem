using MediatR;

namespace SchoolManagement.Application.Features.ShipInformations.Requests.Commands
{
    public class DeleteShipInformationCommand : IRequest
    {
        public int ShipInformationId { get; set; }
    }
} 

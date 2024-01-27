using MediatR;

namespace SchoolManagement.Application.Features.ShipDrowings.Requests.Commands
{
    public class DeleteShipDrowingCommand : IRequest
    {
        public int ShipDrowingId { get; set; }
    }
} 

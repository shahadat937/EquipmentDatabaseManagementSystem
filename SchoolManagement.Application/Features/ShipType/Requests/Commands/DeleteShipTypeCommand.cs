using MediatR;

namespace SchoolManagement.Application.Features.ShipTypes.Requests.Commands
{
    public class DeleteShipTypeCommand : IRequest
    {
        public int ShipTypeId { get; set; }
    }
} 

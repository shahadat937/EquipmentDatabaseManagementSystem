using MediatR;

namespace SchoolManagement.Application.Features.Controlleds.Requests.Commands
{
    public class DeleteControlledCommand : IRequest
    {
        public int ControlledId { get; set; }
    }
} 

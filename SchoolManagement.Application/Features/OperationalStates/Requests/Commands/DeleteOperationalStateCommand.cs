using MediatR;

namespace SchoolManagement.Application.Features.OperationalStates.Requests.Commands
{
    public class DeleteOperationalStateCommand : IRequest
    {
        public int OperationalStateId { get; set; }
    }
} 

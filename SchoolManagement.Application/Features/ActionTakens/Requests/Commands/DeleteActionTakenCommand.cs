using MediatR;

namespace SchoolManagement.Application.Features.ActionTakens.Requests.Commands
{
    public class DeleteActionTakenCommand : IRequest
    {
        public int ActionTakenId { get; set; }
    }
} 

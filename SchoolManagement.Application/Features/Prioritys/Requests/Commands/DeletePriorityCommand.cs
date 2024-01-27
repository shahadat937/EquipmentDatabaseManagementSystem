using MediatR;

namespace SchoolManagement.Application.Features.Prioritys.Requests.Commands
{
    public class DeletePriorityCommand : IRequest
    {
        public int PriorityId { get; set; }
    }
} 

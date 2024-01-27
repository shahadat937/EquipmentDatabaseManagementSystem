using MediatR;
using SchoolManagement.Application.DTOs.Priority;

namespace SchoolManagement.Application.Features.Prioritys.Requests.Commands
{
    public class UpdatePriorityCommand : IRequest<Unit>
    { 
        public PriorityDto PriorityDto { get; set; }
    }
}
 
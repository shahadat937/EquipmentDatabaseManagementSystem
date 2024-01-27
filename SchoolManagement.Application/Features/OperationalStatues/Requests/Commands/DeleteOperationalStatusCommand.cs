using MediatR;

namespace SchoolManagement.Application.Features.OperationalStatuss.Requests.Commands
{
    public class DeleteOperationalStatusCommand : IRequest
    {
        public int OperationalStatusId { get; set; }
    }
} 

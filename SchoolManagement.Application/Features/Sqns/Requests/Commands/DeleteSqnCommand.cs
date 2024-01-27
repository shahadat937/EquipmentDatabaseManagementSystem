using MediatR;

namespace SchoolManagement.Application.Features.Sqns.Requests.Commands
{
    public class DeleteSqnCommand : IRequest
    {
        public int SqnId { get; set; }
    }
} 

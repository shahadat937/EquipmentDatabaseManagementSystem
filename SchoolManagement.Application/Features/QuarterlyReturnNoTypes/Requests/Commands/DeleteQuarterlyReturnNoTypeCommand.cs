using MediatR;

namespace SchoolManagement.Application.Features.QuarterlyReturnNoTypes.Requests.Commands
{
    public class DeleteQuarterlyReturnNoTypeCommand : IRequest
    {
        public int QuarterlyReturnNoTypeId { get; set; }
    }
} 

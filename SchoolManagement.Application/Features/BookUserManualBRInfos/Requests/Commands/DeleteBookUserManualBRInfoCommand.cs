using MediatR;

namespace SchoolManagement.Application.Features.BookUserManualBRInfos.Requests.Commands
{
    public class DeleteBookUserManualBRInfoCommand : IRequest
    {
        public int BookUserManualBRInfoId { get; set; }
    }
} 

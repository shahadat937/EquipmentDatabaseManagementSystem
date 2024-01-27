using MediatR;

namespace SchoolManagement.Application.Features.BookTypes.Requests.Commands
{
    public class DeleteBookTypeCommand : IRequest
    {
        public int BookTypeId { get; set; }
    }
} 

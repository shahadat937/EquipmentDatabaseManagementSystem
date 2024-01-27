using MediatR;
using SchoolManagement.Application.DTOs.BookType;

namespace SchoolManagement.Application.Features.BookTypes.Requests.Commands
{
    public class UpdateBookTypeCommand : IRequest<Unit>
    { 
        public BookTypeDto BookTypeDto { get; set; }
    }
}
 
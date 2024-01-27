using MediatR;
using SchoolManagement.Application.DTOs.BookType;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.BookTypes.Requests.Commands
{
    public class CreateBookTypeCommand : IRequest<BaseCommandResponse>
    {
        public CreateBookTypeDto BookTypeDto { get; set; }
    }
}

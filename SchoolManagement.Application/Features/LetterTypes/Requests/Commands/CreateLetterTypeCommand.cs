using MediatR;
using SchoolManagement.Application.DTOs.LetterTypes;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.LetterTypes.Requests.Commands
{
    public class CreateLetterTypeCommand : IRequest<BaseCommandResponse>
    {
        public CreateLetterTypeDto LetterTypeDto { get; set; }
    }
}

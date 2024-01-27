using MediatR;
using SchoolManagement.Application.DTOs.LetterTypes;

namespace SchoolManagement.Application.Features.LetterTypes.Requests.Commands
{
    public class UpdateLetterTypeCommand : IRequest<Unit>
    { 
        public LetterTypeDto LetterTypeDto { get; set; }
    }
}
 
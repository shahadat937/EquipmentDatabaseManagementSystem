using MediatR;

namespace SchoolManagement.Application.Features.LetterTypes.Requests.Commands
{
    public class DeleteLetterTypeCommand : IRequest
    {
        public int LetterTypeId { get; set; }
    }
} 

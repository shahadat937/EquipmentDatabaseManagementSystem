using MediatR;
using SchoolManagement.Application.DTOs.LetterTypes;

namespace SchoolManagement.Application.Features.LetterTypes.Requests.Queries
{
    public class GetLetterTypeDetailRequest : IRequest<LetterTypeDto>
    {
        public int LetterTypeId { get; set; }
    }
}

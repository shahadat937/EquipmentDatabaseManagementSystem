using MediatR;
using SchoolManagement.Application.DTOs.BookType;

namespace SchoolManagement.Application.Features.BookTypes.Requests.Queries
{
    public class GetBookTypeDetailRequest : IRequest<BookTypeDto>
    {
        public int BookTypeId { get; set; }
    }
}

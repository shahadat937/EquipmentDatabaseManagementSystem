using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.BookType;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.BookTypes.Requests.Queries
{
    public class GetBookTypeListRequest : IRequest<PagedResult<BookTypeDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}

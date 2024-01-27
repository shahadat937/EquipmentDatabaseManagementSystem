using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.LetterTypes;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.LetterTypes.Requests.Queries
{
    public class GetLetterTypeListRequest : IRequest<PagedResult<LetterTypeDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}

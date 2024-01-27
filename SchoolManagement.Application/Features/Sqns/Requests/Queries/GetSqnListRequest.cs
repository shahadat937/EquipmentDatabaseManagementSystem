using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Sqns;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.Sqns.Requests.Queries
{
    public class GetSqnListRequest : IRequest<PagedResult<SqnDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}

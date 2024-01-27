using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.QuarterlyReturnNoTypes;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.QuarterlyReturnNoTypes.Requests.Queries
{
    public class GetQuarterlyReturnNoTypeListRequest : IRequest<PagedResult<QuarterlyReturnNoTypeDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}

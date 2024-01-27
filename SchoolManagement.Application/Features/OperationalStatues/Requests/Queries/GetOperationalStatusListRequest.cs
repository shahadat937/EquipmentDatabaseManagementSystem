using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.OperationalStatuss;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.OperationalStatuss.Requests.Queries
{
    public class GetOperationalStatusListRequest : IRequest<PagedResult<OperationalStatusDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}

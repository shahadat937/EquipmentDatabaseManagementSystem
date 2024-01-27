using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.FcLc;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.FcLcs.Requests.Queries
{
    public class GetFcLcListRequest : IRequest<PagedResult<FcLcDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}

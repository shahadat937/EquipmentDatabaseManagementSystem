using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Controlled;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.Controlleds.Requests.Queries
{
    public class GetControlledListRequest : IRequest<PagedResult<ControlledDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}

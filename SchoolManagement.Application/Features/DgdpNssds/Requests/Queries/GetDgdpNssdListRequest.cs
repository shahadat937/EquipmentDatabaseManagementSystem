using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.DgdpNssd;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.DgdpNssds.Requests.Queries
{
    public class GetDgdpNssdListRequest : IRequest<PagedResult<DgdpNssdDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}

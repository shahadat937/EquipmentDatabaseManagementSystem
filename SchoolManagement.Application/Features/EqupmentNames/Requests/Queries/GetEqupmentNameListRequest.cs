using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.EqupmentNames;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.EqupmentNames.Requests.Queries
{
    public class GetEqupmentNameListRequest : IRequest<PagedResult<EqupmentNameDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}

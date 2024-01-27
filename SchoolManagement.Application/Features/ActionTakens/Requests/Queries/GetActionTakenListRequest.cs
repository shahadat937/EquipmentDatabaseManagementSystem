using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.ActionTaken;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.ActionTakens.Requests.Queries
{
    public class GetActionTakenListRequest : IRequest<PagedResult<ActionTakenDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}

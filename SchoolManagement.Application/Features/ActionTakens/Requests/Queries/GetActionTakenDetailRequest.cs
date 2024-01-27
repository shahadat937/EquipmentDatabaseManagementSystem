using MediatR;
using SchoolManagement.Application.DTOs.ActionTaken;

namespace SchoolManagement.Application.Features.ActionTakens.Requests.Queries
{
    public class GetActionTakenDetailRequest : IRequest<ActionTakenDto>
    {
        public int ActionTakenId { get; set; }
    }
}

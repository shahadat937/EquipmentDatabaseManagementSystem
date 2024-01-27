using MediatR;
using SchoolManagement.Application.DTOs.ActionTaken;

namespace SchoolManagement.Application.Features.ActionTakens.Requests.Commands
{
    public class UpdateActionTakenCommand : IRequest<Unit>
    { 
        public ActionTakenDto ActionTakenDto { get; set; }
    }
}
 
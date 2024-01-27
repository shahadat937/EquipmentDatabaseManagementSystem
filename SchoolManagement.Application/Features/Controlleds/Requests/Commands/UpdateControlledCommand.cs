using MediatR;
using SchoolManagement.Application.DTOs.Controlled;

namespace SchoolManagement.Application.Features.Controlleds.Requests.Commands
{
    public class UpdateControlledCommand : IRequest<Unit>
    { 
        public ControlledDto ControlledDto { get; set; }
    }
}
 
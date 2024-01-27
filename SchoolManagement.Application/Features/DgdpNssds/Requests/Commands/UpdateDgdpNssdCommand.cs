using MediatR;
using SchoolManagement.Application.DTOs.DgdpNssd;

namespace SchoolManagement.Application.Features.DgdpNssds.Requests.Commands
{
    public class UpdateDgdpNssdCommand : IRequest<Unit>
    { 
        public DgdpNssdDto DgdpNssdDto { get; set; }
    }
}
 
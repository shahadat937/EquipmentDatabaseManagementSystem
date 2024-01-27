using MediatR;
using SchoolManagement.Application.DTOs.DgdpNssd;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.DgdpNssds.Requests.Commands
{
    public class CreateDgdpNssdCommand : IRequest<BaseCommandResponse>
    {
        public CreateDgdpNssdDto DgdpNssdDto { get; set; }
    }
}

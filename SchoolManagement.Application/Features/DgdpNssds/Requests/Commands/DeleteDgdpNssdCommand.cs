using MediatR;

namespace SchoolManagement.Application.Features.DgdpNssds.Requests.Commands
{
    public class DeleteDgdpNssdCommand : IRequest
    {
        public int DgdpNssdId { get; set; }
    }
} 

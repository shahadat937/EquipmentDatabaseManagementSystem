using MediatR;
using SchoolManagement.Application.DTOs.DgdpNssd;

namespace SchoolManagement.Application.Features.DgdpNssds.Requests.Queries
{
    public class GetDgdpNssdDetailRequest : IRequest<DgdpNssdDto>
    {
        public int DgdpNssdId { get; set; }
    }
}

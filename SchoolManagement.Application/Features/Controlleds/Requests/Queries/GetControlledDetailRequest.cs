using MediatR;
using SchoolManagement.Application.DTOs.Controlled;

namespace SchoolManagement.Application.Features.Controlleds.Requests.Queries
{
    public class GetControlledDetailRequest : IRequest<ControlledDto>
    {
        public int ControlledId { get; set; }
    }
}

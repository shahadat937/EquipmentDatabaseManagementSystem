using MediatR;
using SchoolManagement.Application.DTOs.Priority;

namespace SchoolManagement.Application.Features.Prioritys.Requests.Queries
{
    public class GetPriorityDetailRequest : IRequest<PriorityDto>
    {
        public int PriorityId { get; set; }
    }
}

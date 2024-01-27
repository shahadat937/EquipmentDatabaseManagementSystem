using MediatR;
using SchoolManagement.Application.DTOs.DealingOfficer;

namespace SchoolManagement.Application.Features.DealingOfficers.Requests.Queries
{
    public class GetDealingOfficerDetailRequest : IRequest<DealingOfficerDto>
    {
        public int DealingOfficerId { get; set; }
    }
}

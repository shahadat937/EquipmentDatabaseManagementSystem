using MediatR;
using SchoolManagement.Application.DTOs.QuarterlyReturnNoTypes;

namespace SchoolManagement.Application.Features.QuarterlyReturnNoTypes.Requests.Queries
{
    public class GetQuarterlyReturnNoTypeDetailRequest : IRequest<QuarterlyReturnNoTypeDto>
    {
        public int QuarterlyReturnNoTypeId { get; set; }
    }
}

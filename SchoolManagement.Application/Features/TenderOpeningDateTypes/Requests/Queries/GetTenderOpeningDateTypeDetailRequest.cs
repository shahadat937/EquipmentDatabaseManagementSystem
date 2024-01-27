using MediatR;
using SchoolManagement.Application.DTOs.TenderOpeningDateTypes;

namespace SchoolManagement.Application.Features.TenderOpeningDateTypes.Requests.Queries
{
    public class GetTenderOpeningDateTypeDetailRequest : IRequest<TenderOpeningDateTypeDto>
    {
        public int TenderOpeningDateTypeId { get; set; }
    }
}

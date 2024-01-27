using MediatR;
using SchoolManagement.Application.DTOs.PaymentStatus;

namespace SchoolManagement.Application.Features.PaymentStatuses.Requests.Queries
{
    public class GetPaymentStatusDetailRequest : IRequest<PaymentStatusDto>
    {
        public int PaymentStatusId { get; set; }
    }
}

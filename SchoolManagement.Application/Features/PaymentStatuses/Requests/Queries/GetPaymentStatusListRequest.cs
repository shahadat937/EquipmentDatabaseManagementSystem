using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.PaymentStatus;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.PaymentStatuses.Requests.Queries
{
    public class GetPaymentStatusListRequest : IRequest<PagedResult<PaymentStatusDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}

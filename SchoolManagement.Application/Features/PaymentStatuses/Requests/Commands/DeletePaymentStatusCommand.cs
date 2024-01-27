using MediatR;

namespace SchoolManagement.Application.Features.PaymentStatuses.Requests.Commands
{
    public class DeletePaymentStatusCommand : IRequest
    {
        public int PaymentStatusId { get; set; }
    }
} 

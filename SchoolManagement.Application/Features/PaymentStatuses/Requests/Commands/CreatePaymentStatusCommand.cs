using MediatR;
using SchoolManagement.Application.DTOs.PaymentStatus;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.PaymentStatuses.Requests.Commands
{
    public class CreatePaymentStatusCommand : IRequest<BaseCommandResponse>
    {
        public CreatePaymentStatusDto PaymentStatusDto { get; set; }
    }
}

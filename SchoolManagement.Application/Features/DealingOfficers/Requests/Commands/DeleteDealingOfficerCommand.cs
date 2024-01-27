using MediatR;

namespace SchoolManagement.Application.Features.DealingOfficers.Requests.Commands
{
    public class DeleteDealingOfficerCommand : IRequest
    {
        public int DealingOfficerId { get; set; }
    }
} 

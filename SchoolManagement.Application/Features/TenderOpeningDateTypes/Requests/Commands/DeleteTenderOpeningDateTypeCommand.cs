using MediatR;

namespace SchoolManagement.Application.Features.TenderOpeningDateTypes.Requests.Commands
{
    public class DeleteTenderOpeningDateTypeCommand : IRequest
    {
        public int TenderOpeningDateTypeId { get; set; }
    }
} 

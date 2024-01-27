using MediatR;
using SchoolManagement.Application.DTOs.TenderOpeningDateTypes;

namespace SchoolManagement.Application.Features.TenderOpeningDateTypes.Requests.Commands
{
    public class UpdateTenderOpeningDateTypeCommand : IRequest<Unit>
    { 
        public TenderOpeningDateTypeDto TenderOpeningDateTypeDto { get; set; }
    }
}
 
using MediatR;
using SchoolManagement.Application.DTOs.TenderOpeningDateTypes;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.TenderOpeningDateTypes.Requests.Commands
{
    public class CreateTenderOpeningDateTypeCommand : IRequest<BaseCommandResponse>
    {
        public CreateTenderOpeningDateTypeDto TenderOpeningDateTypeDto { get; set; }
    }
}

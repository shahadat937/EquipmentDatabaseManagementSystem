using MediatR;
using SchoolManagement.Application.DTOs.QuarterlyReturnNoTypes;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.QuarterlyReturnNoTypes.Requests.Commands
{
    public class CreateQuarterlyReturnNoTypeCommand : IRequest<BaseCommandResponse>
    {
        public CreateQuarterlyReturnNoTypeDto QuarterlyReturnNoTypeDto { get; set; }
    }
}

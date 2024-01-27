using MediatR;
using SchoolManagement.Application.DTOs.OperationalStatuss;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.OperationalStatuss.Requests.Commands
{
    public class CreateOperationalStatusCommand : IRequest<BaseCommandResponse>
    {
        public CreateOperationalStatusDto OperationalStatusDto { get; set; }
    }
}

using MediatR;
using SchoolManagement.Application.DTOs.OperationalStates;
using SchoolManagement.Application.DTOs.ShipInformationTypes;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.OperationalStates.Requests.Commands
{
    public class CreateOperationalStateCommand : IRequest<BaseCommandResponse>
    {
        public CreateOperationalStateDto OperationalStateDto { get; set; }
    }
}

using MediatR;
using SchoolManagement.Application.DTOs.OperationalStates;
using SchoolManagement.Application.DTOs.ShipInformationTypes;

namespace SchoolManagement.Application.Features.OperationalStates.Requests.Commands
{
    public class UpdateOperationalStateCommand : IRequest<Unit>
    { 
        public OperationalStateDto OperationalStateDto { get; set; }
    }
}
 
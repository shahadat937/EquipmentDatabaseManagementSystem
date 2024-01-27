using MediatR;
using SchoolManagement.Application.DTOs.EqupmentNames;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.EqupmentNames.Requests.Commands
{
    public class CreateEqupmentNameCommand : IRequest<BaseCommandResponse>
    {
        public CreateEqupmentNameDto EqupmentNameDto { get; set; }
    }
}

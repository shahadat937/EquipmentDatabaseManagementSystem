using MediatR;
using SchoolManagement.Application.DTOs.ProcurementType;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.ProcurementTypes.Requests.Commands
{
    public class CreateProcurementTypeCommand : IRequest<BaseCommandResponse>
    {
        public CreateProcurementTypeDto ProcurementTypeDto { get; set; }
    }
}

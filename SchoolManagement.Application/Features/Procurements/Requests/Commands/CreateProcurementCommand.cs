using MediatR;
using SchoolManagement.Application.DTOs.Procurement;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.Procurements.Requests.Commands
{
    public class CreateProcurementCommand : IRequest<BaseCommandResponse>
    {
        public CreateProcurementDto ProcurementDto { get; set; }
    }
}

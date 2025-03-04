using MediatR;
using SchoolManagement.Application.DTOs.Procurement;

namespace SchoolManagement.Application.Features.Procurements.Requests.Commands
{
    public class UpdateProcurementCommand : IRequest<Unit>
    { 
        public CreateProcurementDto ProcurementDto { get; set; }
    }
}
 
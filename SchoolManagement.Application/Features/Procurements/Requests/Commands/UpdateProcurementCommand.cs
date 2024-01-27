using MediatR;
using SchoolManagement.Application.DTOs.Procurement;

namespace SchoolManagement.Application.Features.Procurements.Requests.Commands
{
    public class UpdateProcurementCommand : IRequest<Unit>
    { 
        public ProcurementDto ProcurementDto { get; set; }
    }
}
 
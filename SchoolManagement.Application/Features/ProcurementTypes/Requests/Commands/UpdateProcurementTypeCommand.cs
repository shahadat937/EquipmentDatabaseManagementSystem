using MediatR;
using SchoolManagement.Application.DTOs.ProcurementType;

namespace SchoolManagement.Application.Features.ProcurementTypes.Requests.Commands
{
    public class UpdateProcurementTypeCommand : IRequest<Unit>
    { 
        public ProcurementTypeDto ProcurementTypeDto { get; set; }
    }
}
 
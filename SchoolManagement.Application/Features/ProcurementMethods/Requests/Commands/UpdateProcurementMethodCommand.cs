using MediatR;
using SchoolManagement.Application.DTOs.ProcurementMethod;

namespace SchoolManagement.Application.Features.ProcurementMethods.Requests.Commands
{
    public class UpdateProcurementMethodCommand : IRequest<Unit>
    { 
        public ProcurementMethodDto ProcurementMethodDto { get; set; }
    }
}
 
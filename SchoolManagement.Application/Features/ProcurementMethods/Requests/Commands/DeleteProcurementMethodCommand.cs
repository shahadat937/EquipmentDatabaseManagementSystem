using MediatR;

namespace SchoolManagement.Application.Features.ProcurementMethods.Requests.Commands
{
    public class DeleteProcurementMethodCommand : IRequest
    {
        public int ProcurementMethodId { get; set; }
    }
} 

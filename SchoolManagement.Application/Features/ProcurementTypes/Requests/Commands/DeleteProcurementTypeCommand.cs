using MediatR;

namespace SchoolManagement.Application.Features.ProcurementTypes.Requests.Commands
{
    public class DeleteProcurementTypeCommand : IRequest
    {
        public int ProcurementTypeId { get; set; }
    }
} 

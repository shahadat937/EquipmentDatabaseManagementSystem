using MediatR;

namespace SchoolManagement.Application.Features.Procurements.Requests.Commands
{
    public class DeleteProcurementCommand : IRequest
    {
        public int ProcurementId { get; set; }
    }
} 

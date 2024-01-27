using MediatR;
using SchoolManagement.Application.DTOs.OperationalStatuss;

namespace SchoolManagement.Application.Features.OperationalStatuss.Requests.Commands
{
    public class UpdateOperationalStatusCommand : IRequest<Unit>
    { 
        public OperationalStatusDto OperationalStatusDto { get; set; }
    }
}
 
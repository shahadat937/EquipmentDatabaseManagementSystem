using MediatR;
using SchoolManagement.Application.DTOs.EqupmentNames;

namespace SchoolManagement.Application.Features.EqupmentNames.Requests.Commands
{
    public class UpdateEqupmentNameCommand : IRequest<Unit>
    { 
        public EqupmentNameDto EqupmentNameDto { get; set; }
    }
}
 
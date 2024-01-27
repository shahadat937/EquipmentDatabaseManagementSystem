using MediatR;

namespace SchoolManagement.Application.Features.EqupmentNames.Requests.Commands
{
    public class DeleteEqupmentNameCommand : IRequest
    {
        public int EqupmentNameId { get; set; }
    }
} 

using MediatR;
using SchoolManagement.Application.DTOs.ReturnTypes;

namespace SchoolManagement.Application.Features.ReturnTypes.Requests.Commands
{
    public class UpdateReturnTypeCommand : IRequest<Unit>
    { 
        public ReturnTypeDto ReturnTypeDto { get; set; }
    }
}
 
using MediatR;
using SchoolManagement.Application.DTOs.ReturnTypes;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.ReturnTypes.Requests.Commands
{
    public class CreateReturnTypeCommand : IRequest<BaseCommandResponse>
    {
        public CreateReturnTypeDto ReturnTypeDto { get; set; }
    }
}

using MediatR;

namespace SchoolManagement.Application.Features.ReturnTypes.Requests.Commands
{
    public class DeleteReturnTypeCommand : IRequest
    {
        public int ReturnTypeId { get; set; }
    }
} 

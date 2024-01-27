using MediatR;
using SchoolManagement.Application.DTOs.Sqns;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.Sqns.Requests.Commands
{
    public class CreateSqnCommand : IRequest<BaseCommandResponse>
    {
        public CreateSqnDto SqnDto { get; set; }
    }
}

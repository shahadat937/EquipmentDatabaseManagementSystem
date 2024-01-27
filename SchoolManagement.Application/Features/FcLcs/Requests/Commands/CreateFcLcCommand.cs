using MediatR;
using SchoolManagement.Application.DTOs.FcLc;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.FcLcs.Requests.Commands
{
    public class CreateFcLcCommand : IRequest<BaseCommandResponse>
    {
        public CreateFcLcDto FcLcDto { get; set; }
    }
}

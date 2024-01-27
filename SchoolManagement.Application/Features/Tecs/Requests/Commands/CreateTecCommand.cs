using MediatR;
using SchoolManagement.Application.DTOs.Tec;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.Tecs.Requests.Commands
{
    public class CreateTecCommand : IRequest<BaseCommandResponse>
    {
        public CreateTecDto TecDto { get; set; }
    }
}

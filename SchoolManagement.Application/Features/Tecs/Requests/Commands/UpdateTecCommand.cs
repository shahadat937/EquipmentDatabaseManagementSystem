using MediatR;
using SchoolManagement.Application.DTOs.Tec;

namespace SchoolManagement.Application.Features.Tecs.Requests.Commands
{
    public class UpdateTecCommand : IRequest<Unit>
    { 
        public TecDto TecDto { get; set; }
    }
}
 
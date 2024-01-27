using MediatR;
using SchoolManagement.Application.DTOs.FcLc;

namespace SchoolManagement.Application.Features.FcLcs.Requests.Commands
{
    public class UpdateFcLcCommand : IRequest<Unit>
    { 
        public FcLcDto FcLcDto { get; set; }
    }
}
 
using MediatR;
using SchoolManagement.Application.DTOs.Sqns;

namespace SchoolManagement.Application.Features.Sqns.Requests.Commands
{
    public class UpdateSqnCommand : IRequest<Unit>
    { 
        public SqnDto SqnDto { get; set; }
    }
}
 
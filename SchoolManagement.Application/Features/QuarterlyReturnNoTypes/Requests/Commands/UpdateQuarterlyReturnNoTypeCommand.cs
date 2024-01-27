using MediatR;
using SchoolManagement.Application.DTOs.QuarterlyReturnNoTypes;

namespace SchoolManagement.Application.Features.QuarterlyReturnNoTypes.Requests.Commands
{
    public class UpdateQuarterlyReturnNoTypeCommand : IRequest<Unit>
    { 
        public QuarterlyReturnNoTypeDto QuarterlyReturnNoTypeDto { get; set; }
    }
}
 
using MediatR;
using SchoolManagement.Application.DTOs.CodeValues;

namespace SchoolManagement.Application.Features.CodeValues.Requests.Commands
{
    public class UpdateCodeValueCommand : IRequest<Unit>  
    { 
        public CodeValueDto CodeValueDto { get; set; }     
    }
}

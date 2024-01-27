using MediatR;
using SchoolManagement.Application.Responses;
using SchoolManagement.Application.DTOs.CodeValues;

namespace SchoolManagement.Application.Features.CodeValues.Requests.Commands
{
    public class CreateCodeValueCommand : IRequest<BaseCommandResponse> 
    {
        public CreateCodeValueDto CodeValueDto { get; set; }      

    }
}

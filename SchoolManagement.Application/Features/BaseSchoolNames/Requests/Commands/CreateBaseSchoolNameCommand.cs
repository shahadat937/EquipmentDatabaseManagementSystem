using MediatR;
using SchoolManagement.Application.Responses;
using SchoolManagement.Application.DTOs.BaseSchoolNames;

namespace SchoolManagement.Application.Features.BaseSchoolNames.Requests.Commands 
{
    public class CreateBaseSchoolNameCommand : IRequest<BaseCommandResponse> 
    {
        public CreateBaseSchoolNameDto BaseSchoolNameDto { get; set; }      

    }
}

using MediatR;
using SchoolManagement.Application.DTOs.BaseSchoolNames;

namespace SchoolManagement.Application.Features.BaseSchoolNames.Requests.Commands
{
    public class UpdateBaseSchoolNameCommand : IRequest<Unit>  
    { 
        //public BaseSchoolNameDto BaseSchoolNameDto { get; set; }     
        public CreateBaseSchoolNameDto CreateBaseSchoolNameDto { get; set; }
    }
}

using MediatR;
using SchoolManagement.Application.DTOs.BaseSchoolNames;
 
namespace SchoolManagement.Application.Features.BaseSchoolNames.Requests.Queries
{
    public class GetBaseSchoolNameDetailRequest : IRequest<BaseSchoolNameDto> 
    {
        public int Id { get; set; } 
    }
}

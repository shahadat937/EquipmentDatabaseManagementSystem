using MediatR;
using SchoolManagement.Application.DTOs.CodeValues;

namespace SchoolManagement.Application.Features.CodeValues.Requests.Queries
{
    public class GetCodeValueDetailRequest : IRequest<CodeValueDto>  
    {
        public int Id { get; set; } 
    }
}

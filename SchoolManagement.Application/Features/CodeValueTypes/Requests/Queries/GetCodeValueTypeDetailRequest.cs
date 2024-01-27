using MediatR;
using SchoolManagement.Application.DTOs.CodeValueType;

namespace SchoolManagement.Application.Features.CodeValueTypes.Requests.Queries
{
    public class GetCodeValueTypeDetailRequest : IRequest<CodeValueTypeDto> 
    {
        public int Id { get; set; } 
    }
}

using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.CodeValueType;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.CodeValueTypes.Requests.Queries
{
    public class GetCodeValueTypeListRequest : IRequest<PagedResult<CodeValueTypeDto>>
    {
        public QueryParams QueryParams { get; set; }  
    }
}

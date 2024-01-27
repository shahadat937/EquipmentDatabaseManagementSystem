using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.CodeValues;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.CodeValues.Requests.Queries
{
    public class GetCodeValueListRequest : IRequest<PagedResult<CodeValueDto>>
    { 
        public QueryParams QueryParams { get; set; }  
    }
}

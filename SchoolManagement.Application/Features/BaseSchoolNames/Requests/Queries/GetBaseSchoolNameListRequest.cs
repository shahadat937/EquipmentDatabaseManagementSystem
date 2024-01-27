using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.BaseSchoolNames;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.BaseSchoolNames.Requests.Queries 
{ 
    public class GetBaseSchoolNameListRequest : IRequest<PagedResult<BaseSchoolNameDto>>
    { 
        public QueryParams QueryParams { get; set; }  
    }
}

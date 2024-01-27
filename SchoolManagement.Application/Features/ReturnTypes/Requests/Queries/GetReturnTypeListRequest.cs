using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.ReturnTypes;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.ReturnTypes.Requests.Queries
{
    public class GetReturnTypeListRequest : IRequest<PagedResult<ReturnTypeDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}

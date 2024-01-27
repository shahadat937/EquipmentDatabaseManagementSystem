using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.TenderOpeningDateTypes;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.TenderOpeningDateTypes.Requests.Queries
{
    public class GetTenderOpeningDateTypeListRequest : IRequest<PagedResult<TenderOpeningDateTypeDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}

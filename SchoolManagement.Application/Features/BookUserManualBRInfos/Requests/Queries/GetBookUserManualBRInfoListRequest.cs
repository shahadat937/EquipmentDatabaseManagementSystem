using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.BookUserManualBRInfo;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.BookUserManualBRInfos.Requests.Queries
{
    public class GetBookUserManualBRInfoListRequest : IRequest<PagedResult<BookUserManualBRInfoDto>>
    {
        public QueryParams QueryParams { get; set; }
        public int? ShipId { get; set; }
    }
}

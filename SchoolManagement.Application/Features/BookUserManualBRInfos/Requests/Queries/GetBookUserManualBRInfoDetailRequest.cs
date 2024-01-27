using MediatR;
using SchoolManagement.Application.DTOs.BookUserManualBRInfo;

namespace SchoolManagement.Application.Features.BookUserManualBRInfos.Requests.Queries
{
    public class GetBookUserManualBRInfoDetailRequest : IRequest<BookUserManualBRInfoDto>
    {
        public int BookUserManualBRInfoId { get; set; }
    }
}

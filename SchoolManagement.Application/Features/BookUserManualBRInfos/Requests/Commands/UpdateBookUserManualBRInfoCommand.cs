using MediatR;
using SchoolManagement.Application.DTOs.BookUserManualBRInfo;

namespace SchoolManagement.Application.Features.BookUserManualBRInfos.Requests.Commands
{
    public class UpdateBookUserManualBRInfoCommand : IRequest<Unit>
    { 
        public BookUserManualBRInfoDto BookUserManualBRInfoDto { get; set; }
    }
}
 
using MediatR;
using SchoolManagement.Application.DTOs.BookUserManualBRInfo;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.BookUserManualBRInfos.Requests.Commands
{
    public class CreateBookUserManualBRInfoCommand : IRequest<BaseCommandResponse>
    {
        public CreateBookUserManualBRInfoDto BookUserManualBRInfoDto { get; set; }
    }
}

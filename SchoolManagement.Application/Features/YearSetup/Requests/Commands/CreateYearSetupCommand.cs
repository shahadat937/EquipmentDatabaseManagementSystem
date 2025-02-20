using MediatR;
using SchoolManagement.Application.DTOs.YearSetup;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.YearSetups.Requests.Commands
{
    public class CreateYearSetupCommand : IRequest<BaseCommandResponse>
    {
        public CreateYearSetupDto YearSetupDto { get; set; }
    }
}

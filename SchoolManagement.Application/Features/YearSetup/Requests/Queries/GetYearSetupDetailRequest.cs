using MediatR;
using SchoolManagement.Application.DTOs.YearSetup;

namespace SchoolManagement.Application.Features.YearSetups.Requests.Queries
{
    public class GetYearSetupDetailRequest : IRequest<YearSetupDto>
    {
        public int YearSetupId { get; set; }
    }
}

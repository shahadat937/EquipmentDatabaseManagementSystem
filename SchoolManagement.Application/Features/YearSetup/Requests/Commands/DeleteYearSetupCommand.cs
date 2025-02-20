using MediatR;

namespace SchoolManagement.Application.Features.YearSetups.Requests.Commands
{
    public class DeleteYearSetupCommand : IRequest
    {
        public int YearSetupId { get; set; }
    }
} 

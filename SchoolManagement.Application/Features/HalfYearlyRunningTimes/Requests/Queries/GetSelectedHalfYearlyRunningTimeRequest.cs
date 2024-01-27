using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.HalfYearlyRunningTimes.Requests.Queries
{
    public class GetSelectedHalfYearlyRunningTimeRequest : IRequest<List<SelectedModel>>
    {
    }
} 
